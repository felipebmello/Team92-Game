using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillsetController : MonoBehaviour
{

    [SerializeField] protected KarmaScriptableObject.KarmaState currentState = KarmaScriptableObject.KarmaState.TooInnocent;
    [SerializeField] protected List<KarmaScriptableObject> karmaList;
    [SerializeField] protected KarmaScriptableObject currentKarmaScrObj;
    [SerializeField] protected SkillScriptableObject currentSkill;
    [SerializeField] protected List<KarmaScriptableObject.KarmaState> allStates = new List<KarmaScriptableObject.KarmaState>();
    [SerializeField] protected AudioClip objectHitSFX;
    [SerializeField] protected float controllerSFXVolume = 0.75f;
    protected HealthSystem healthSystem;
    protected HitKnockback hitKnockback;

    protected virtual void Start() 
    {
        healthSystem = this.gameObject.GetComponent<HealthSystem>();
        hitKnockback = this.gameObject.GetComponent<HitKnockback>();
        LearnNewSkill(currentSkill);
        healthSystem.OnDamaged += HealthSystem_OnDamaged;
        healthSystem.OnDead += HealthSystem_OnDead;
        LevelSystem.Instance.OnSkillsOverlay += LevelSystem_OnSkillsOverlay;
    }
    
    protected virtual void OnDisable() 
    {
        healthSystem.OnDamaged += HealthSystem_OnDamaged;
        healthSystem.OnDead += HealthSystem_OnDead;
        LevelSystem.Instance.OnSkillsOverlay -= LevelSystem_OnSkillsOverlay;
    }

    protected abstract void LevelSystem_OnSkillsOverlay (object sender, SkillScriptableObject[] skills);
    
    protected void HealthSystem_OnDamaged(object sender, Transform other)
    {
        AudioSource.PlayClipAtPoint(objectHitSFX, AudioManager.Instance.GetAudioListener().transform.position, controllerSFXVolume);
        hitKnockback.ObjectHit(other);
    }

    protected abstract void HealthSystem_OnDead (object sender, EventArgs e);

    public virtual void LearnNewSkill (SkillScriptableObject skill)
    {
        currentSkill = skill;
        healthSystem.ApplyHealthModifier(skill.HealthModifier);
        allStates.Add(currentState);
        GetKarmaObject();
    }

    protected void GetKarmaObject()
    {
        foreach (KarmaScriptableObject karmaObject in karmaList) 
        {
            if (karmaObject.State == currentState) currentKarmaScrObj = karmaObject;
        }
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
                        currentState = KarmaScriptableObject.KarmaState.Innocent;
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
                        Debug.Log("Just checked for good karma");
                        currentState = KarmaScriptableObject.KarmaState.Virtuous;
                        return;
                }
                return;
            case KarmaScriptableObject.KarmaState.Innocent:
                switch (skill.State)
                {
                    case SkillScriptableObject.SkillState.BadKarma:
                        currentState = KarmaScriptableObject.KarmaState.Corrupt;
                        return;
                    case SkillScriptableObject.SkillState.Neutral:
                        currentState = KarmaScriptableObject.KarmaState.Enlightned;
                        return;
                    case SkillScriptableObject.SkillState.GoodKarma:
                        currentState = KarmaScriptableObject.KarmaState.Good;
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
                        currentState = KarmaScriptableObject.KarmaState.Innocent;
                        return;
                    case SkillScriptableObject.SkillState.GoodKarma:
                        currentState = KarmaScriptableObject.KarmaState.Angel;
                        return;
                }
                return;
        }
    }
}
