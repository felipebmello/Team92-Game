using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillsetController : MonoBehaviour
{

    [SerializeField] protected DataScriptableObject savedData;
    [SerializeField] protected KarmaScriptableObject.KarmaState currentState = KarmaScriptableObject.KarmaState.TooInnocent;
    [SerializeField] protected List<KarmaScriptableObject> karmaList;
    [SerializeField] protected KarmaScriptableObject currentKarmaScrObj;
    [SerializeField] protected SkillScriptableObject currentSkill;
    [SerializeField] protected List<SkillScriptableObject> allSkills;
    [SerializeField] protected List<KarmaScriptableObject.KarmaState> allStates = new List<KarmaScriptableObject.KarmaState>();
    [SerializeField] protected AudioClip objectHitSFX;
    [SerializeField] protected float controllerSFXVolume = 0.75f;
    [SerializeField] protected Animator myAnimator;
    protected HealthSystem healthSystem;
    protected HitKnockback hitKnockback;
    protected HitFlash hitFlash;
    protected bool isPlayerDead = false;
    protected bool hasPlayerWon = false;

    protected virtual void OnDestroy() {
        if (savedData != null && !isPlayerDead && !hasPlayerWon) 
        {
            //Debug.Log(gameObject + " is saving skillset to " + savedData + ".");
            SaveSkillsetData();
        }
    }

    protected virtual void Start() 
    {
        
        //Debug.Log(gameObject + " is starting.");
        healthSystem = this.gameObject.GetComponent<HealthSystem>();
        hitKnockback = this.gameObject.GetComponent<HitKnockback>();
        hitFlash = this.gameObject.GetComponentInChildren<HitFlash>();
        if (savedData != null) 
        {
            //Player e inimigos variantes sempre carregam seu skillset dos dados base salvos no PlayerSaveData Scriptable Object.
            //Debug.Log(gameObject + " is loading skillset from " + savedData + ".");
            LoadSkillsetData();
            savedData.SetLastData();
            ModifyHealthSystem(currentSkill.HealthModifier);
            GetKarmaObject();
            SetAnimationOverride();
        }    
        else 
        {
            //Inimigos não variantes não carregam seu skillset, apenas "aprendem" sua skill única/atual.
            LearnNewSkill(currentSkill);
        }
        healthSystem.OnDamaged += HealthSystem_OnDamaged;
        healthSystem.OnDead += HealthSystem_OnDead;
        LevelSystem.Instance.OnSkillsOverlay += LevelSystem_OnSkillsOverlay;
        LevelSystem.Instance.OnPlayerDeath += LevelSystem_OnPlayerDeath;
        LevelSystem.Instance.OnPlayerVictory += LevelSystem_OnPlayerVictory;

    }

    protected virtual void OnDisable() 
    {
        healthSystem.OnDamaged -= HealthSystem_OnDamaged;
        healthSystem.OnDead -= HealthSystem_OnDead;
        LevelSystem.Instance.OnSkillsOverlay -= LevelSystem_OnSkillsOverlay;
    }

    protected abstract void LevelSystem_OnSkillsOverlay (object sender, SkillScriptableObject[] skills);
    
    protected void LevelSystem_OnPlayerDeath(object sender, EventArgs e)
    {
        isPlayerDead = true;
        if (savedData != null)
        {
            savedData.BaseData();
            savedData.ResetData();
        }
    }

    protected void LevelSystem_OnPlayerVictory(object sender, EventArgs e)
    {
        hasPlayerWon = true;
        if (savedData != null)
        {
            savedData.BaseData();
            savedData.ResetData();
            Debug.Log("Cleaned data");
        }
    }
    protected virtual void HealthSystem_OnDamaged(object sender, Transform other)
    {
        AudioSource.PlayClipAtPoint(objectHitSFX, AudioManager.Instance.GetAudioListener().transform.position, controllerSFXVolume);
        hitFlash.Flash();
        hitKnockback.ObjectHit(other);
    }

    protected abstract void HealthSystem_OnDead (object sender, EventArgs e);

    public virtual void LearnNewSkill (SkillScriptableObject skill)
    {
        //Debug.Log(gameObject + " is learning new skill " + skill + ".");
        currentSkill = skill;
        ModifyHealthSystem(skill.HealthModifier);
        ReplenishHealthSystem();
        allStates.Add(currentState);
        allSkills.Add(currentSkill);
        GetKarmaObject();
        SetAnimationOverride();
    }

    protected virtual void ModifyHealthSystem(float healthModifier)
    {
        healthSystem.ApplyHealthModifier(healthModifier);
        healthSystem.SetLastHealthModifier(healthModifier);
    }
    protected virtual void ReplenishHealthSystem()
    {
        healthSystem.ReplenishFullHealth();
    }

    protected void GetKarmaObject()
    {
        foreach (KarmaScriptableObject karmaObject in karmaList) 
        {
            if (karmaObject.State == currentState) currentKarmaScrObj = karmaObject;
        }
    }

    protected void SetAnimationOverride()
    {
        myAnimator.runtimeAnimatorController = currentKarmaScrObj.NewAOC;
    }

    protected void CheckCurrentKarmaState(SkillScriptableObject skill)
    {
        switch (currentState)
        {
            case KarmaScriptableObject.KarmaState.Corrupt:
                switch (skill.State)
                {
                    case SkillScriptableObject.SkillState.BadKarma:
                        currentState = KarmaScriptableObject.KarmaState.Evil;
                        return;
                    case SkillScriptableObject.SkillState.GoodKarma:
                        currentState = KarmaScriptableObject.KarmaState.Enlightned;
                        return;
                }
                return;
            case KarmaScriptableObject.KarmaState.Malicious:
                switch (skill.State)
                {
                    case SkillScriptableObject.SkillState.BadKarma:
                        currentState = KarmaScriptableObject.KarmaState.Corrupt;
                        return;
                    case SkillScriptableObject.SkillState.GoodKarma:
                        currentState = KarmaScriptableObject.KarmaState.Innocent;
                        return;
                }
                return;
            case KarmaScriptableObject.KarmaState.TooInnocent:
                switch (skill.State)
                {
                    case SkillScriptableObject.SkillState.BadKarma:
                        currentState = KarmaScriptableObject.KarmaState.Malicious;
                        return;
                    case SkillScriptableObject.SkillState.GoodKarma:
                        currentState = KarmaScriptableObject.KarmaState.Virtuous;
                        return;
                }
                return;
            case KarmaScriptableObject.KarmaState.Innocent:
                switch (skill.State)
                {
                    case SkillScriptableObject.SkillState.BadKarma:
                        currentState = KarmaScriptableObject.KarmaState.Enlightned;
                        return;
                    case SkillScriptableObject.SkillState.Neutral:
                        currentState = KarmaScriptableObject.KarmaState.Enlightned;
                        return;
                    case SkillScriptableObject.SkillState.GoodKarma:
                        currentState = KarmaScriptableObject.KarmaState.Enlightned;
                        return;
                }
                return;
            case KarmaScriptableObject.KarmaState.Virtuous:
                switch (skill.State)
                {
                    case SkillScriptableObject.SkillState.BadKarma:
                        currentState = KarmaScriptableObject.KarmaState.Innocent;
                        return;
                    case SkillScriptableObject.SkillState.GoodKarma:
                        currentState = KarmaScriptableObject.KarmaState.Good;
                        return;
                }
                return;
            case KarmaScriptableObject.KarmaState.Good:
                switch (skill.State)
                {
                    case SkillScriptableObject.SkillState.BadKarma:
                        currentState = KarmaScriptableObject.KarmaState.Enlightned;
                        return;
                    case SkillScriptableObject.SkillState.GoodKarma:
                        currentState = KarmaScriptableObject.KarmaState.Angel;
                        return;
                }
                return;
        }
    }

    protected virtual void SaveSkillsetData()
    {
        savedData.CurrentState = currentState;
        savedData.CurrentKarmaScrObj = currentKarmaScrObj;
        savedData.CurrentSkill = currentSkill;
        savedData.AllStates = new List<KarmaScriptableObject.KarmaState>();
        savedData.AllSkills = new List<SkillScriptableObject>();
        savedData.AllStates = allStates;
        savedData.AllSkills = allSkills;
        savedData.numberOfSaves++;
    }
    protected virtual void LoadSkillsetData()
    {
        currentState = savedData.CurrentState;
        currentSkill = savedData.CurrentSkill;
        allStates = savedData.AllStates;
        allSkills = savedData.AllSkills;
    }

}
