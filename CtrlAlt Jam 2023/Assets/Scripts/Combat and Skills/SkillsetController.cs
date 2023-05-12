using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillsetController : MonoBehaviour
{
    public enum KarmaState
    {
        Evil,           //0
        Corrupt,        //1
        Malicious,      //2
        TooInnocent,    //3
        Innocent,       //4
        Enlightned,     //5
        Virtuous,       //6
        Good,           //7
        Angel         //8
    }
    
    [SerializeField] protected KarmaState currentState = KarmaState.Innocent;
    [SerializeField] protected BaseSkill currentSkill;
    [SerializeField] protected List<KarmaState> allStates = new List<KarmaState>();
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

    protected abstract void LevelSystem_OnSkillsOverlay (object sender, BaseSkill[] skills);
    
    protected void HealthSystem_OnDamaged(object sender, Transform other)
    {
        AudioSource.PlayClipAtPoint(objectHitSFX, AudioManager.Instance.GetAudioListener().transform.position, controllerSFXVolume);
        hitKnockback.ObjectHit(other);
    }

    protected abstract void HealthSystem_OnDead (object sender, EventArgs e);

    public virtual void LearnNewSkill (BaseSkill skill)
    {
        CheckCurrentKarmaState(skill);
        currentSkill = skill;
        healthSystem.ApplyHealthModifier(skill.HealthModifier);
        allStates.Add(currentState);
    }

    private void CheckCurrentKarmaState(BaseSkill skill)
    {
        switch (currentState)
        {
            case KarmaState.Corrupt:
                switch (skill.State)
                {
                    case BaseSkill.SkillState.BadKarma:
                        currentState = KarmaState.Evil;
                        return;
                    case BaseSkill.SkillState.GoodKarma:
                        currentState = KarmaState.Innocent;
                        return;
                }
                return;
            case KarmaState.Malicious:
                switch (skill.State)
                {
                    case BaseSkill.SkillState.BadKarma:
                        currentState = KarmaState.Corrupt;
                        return;
                    case BaseSkill.SkillState.GoodKarma:
                        currentState = KarmaState.Innocent;
                        return;
                }
                return;
            case KarmaState.TooInnocent:
                switch (skill.State)
                {
                    case BaseSkill.SkillState.BadKarma:
                        currentState = KarmaState.Malicious;
                        return;
                    case BaseSkill.SkillState.GoodKarma:
                        currentState = KarmaState.Virtuous;
                        return;
                }
                return;
            case KarmaState.Innocent:
                switch (skill.State)
                {
                    case BaseSkill.SkillState.BadKarma:
                        currentState = KarmaState.Corrupt;
                        return;
                    case BaseSkill.SkillState.GoodKarma:
                        currentState = KarmaState.Good;
                        return;
                }
                return;
            case KarmaState.Virtuous:
                switch (skill.State)
                {
                    case BaseSkill.SkillState.BadKarma:
                        currentState = KarmaState.Corrupt;
                        return;
                    case BaseSkill.SkillState.GoodKarma:
                        currentState = KarmaState.Good;
                        return;
                }
                return;
            case KarmaState.Good:
                switch (skill.State)
                {
                    case BaseSkill.SkillState.BadKarma:
                        currentState = KarmaState.Innocent;
                        return;
                    case BaseSkill.SkillState.GoodKarma:
                        currentState = KarmaState.Angel;
                        return;
                }
                return;
        }
    }
}
