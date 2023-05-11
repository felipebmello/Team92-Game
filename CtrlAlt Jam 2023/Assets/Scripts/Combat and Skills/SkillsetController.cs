using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillsetController : MonoBehaviour
{
    public enum SkillState
    {
        Evil,
        Corrupt,
        Innocent,
        Good,
        Enlightned
    }
    
    [SerializeField] protected SkillState currentState = SkillState.Innocent;
    [SerializeField] protected BaseSkill currentSkill;
    [SerializeField] protected List<SkillState> allStates = new List<SkillState>();
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
        //CheckCurrentState(skill);
        currentState = skill.State;
        currentSkill = skill;
        healthSystem.ApplyHealthModifier(skill.HealthModifier);
        allStates.Add(currentState);
    }

    private void CheckCurrentState(BaseSkill skill)
    {
        switch (currentState)
        {
            case SkillState.Evil:
                switch (skill.State)
                {
                    case SkillState.Evil:
                        currentState = SkillState.Corrupt;
                        return;
                    case SkillState.Good:
                        currentState = SkillState.Innocent;
                        return;
                }
                return;
            case SkillState.Innocent:
                currentState = skill.State;
                return;
            case SkillState.Good:
                switch (skill.State)
                {
                    case SkillState.Evil:
                        currentState = SkillState.Innocent;
                        return;
                    case SkillState.Good:
                        currentState = SkillState.Enlightned;
                        return;
                }
                return;
        }
    }
}
