using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : SkillsetController
{
    private PlayerMovement playerMovement;
    private PlayerShooting playerShooting;
    private Animator skillUpAnimator;
    [SerializeField] private float skillUpAnimationTimer;
    [SerializeField] protected AudioClip playerSkillUpSFX;
    [SerializeField] protected AudioClip playerDeathSFX;
    
    protected override void Start() 
    {
        skillUpAnimator = this.gameObject.GetComponentInChildren<Animator>();
        playerMovement = this.gameObject.GetComponent<PlayerMovement>();
        playerShooting = this.gameObject.GetComponent<PlayerShooting>();
        Debug.Log("Player starting, current skill: "+currentSkill);
        base.Start();
        if (currentState == KarmaScriptableObject.KarmaState.TooInnocent) healthSystem.ApplyHealthModifier(2);
        playerMovement.SetSprite(currentKarmaScrObj.NewSprite);
        TogglePlayerBehaviour(true);
        LevelSystem.Instance.OnChoosedSkill += LevelSystem_OnChoosedSkill;
    }
    protected override void OnDisable() 
    {
        base.OnDisable();
        LevelSystem.Instance.OnChoosedSkill -= LevelSystem_OnChoosedSkill;
    }

    protected override void LevelSystem_OnSkillsOverlay (object sender, SkillScriptableObject[] skills)
    {
        TogglePlayerBehaviour(false);
        CameraController.Instance.ClearPlayerOffset();
    }
    protected void LevelSystem_OnChoosedSkill (object sender, SkillScriptableObject skill)
    {
        LearnNewSkill(skill);
        
        StartCoroutine(SkillUpEffects(skill));
    }


    protected override void HealthSystem_OnDead(object sender, EventArgs e)
    {
        //Executar comportamento ao morrer, mover player para trás
        TogglePlayerBehaviour(false);
        LevelSystem.Instance.PlayerKilled();
        AudioSource.PlayClipAtPoint(playerDeathSFX, AudioManager.Instance.GetAudioListener().transform.position, controllerSFXVolume);
        Destroy(gameObject, 1f);
    }


    private void TogglePlayerBehaviour (bool toggle)
    {
        playerMovement.enabled = toggle;
        playerShooting.enabled = toggle;
        CameraController.Instance.enabled = toggle;
    }


    public override void LearnNewSkill (SkillScriptableObject skill)
    {
        CheckCurrentKarmaState(skill);
        base.LearnNewSkill(skill);
        playerMovement.SetMovementSpeed(skill.NewMovementSpeed);
        playerShooting.SetBulletPrefab(currentKarmaScrObj.NewBulletPrefab);
        playerShooting.SetHoldToShoot(skill.NewHoldToShoot);
        playerShooting.SetFireRate(skill.NewFireRate);
    }
    
    protected override void SaveSkillsetData()
    {
        base.SaveSkillsetData();
        savedData.MovementSpeed = playerMovement.GetMovementSpeed();
        savedData.BulletPrefab = playerShooting.GetBulletPrefab();
        savedData.HoldToShoot = playerShooting.GetHoldToShoot();
        savedData.FireRate = playerShooting.GetFireRate();
    }

    protected override void LoadSkillsetData()
    {
        base.LoadSkillsetData();
        playerMovement.SetMovementSpeed(savedData.CurrentSkill.NewMovementSpeed);
        playerShooting.SetBulletPrefab(savedData.CurrentKarmaScrObj.NewBulletPrefab);
        playerShooting.SetHoldToShoot(savedData.CurrentSkill.NewHoldToShoot);
        playerShooting.SetFireRate(savedData.CurrentSkill.NewFireRate);
    }

    private void OnQuit() {
        Application.Quit();
    }

    public IEnumerator SkillUpEffects(SkillScriptableObject skill)
    {
        if (skill.State == SkillScriptableObject.SkillState.GoodKarma) skillUpAnimator.SetTrigger("GoodTrigger");
        else skillUpAnimator.SetTrigger("EvilTrigger");
        AudioSource.PlayClipAtPoint(playerSkillUpSFX, AudioManager.Instance.GetAudioListener().transform.position, controllerSFXVolume);
        yield return new WaitForSeconds(1f);
        playerMovement.SetSprite(currentKarmaScrObj.NewSprite);
        TogglePlayerBehaviour(true);

    }
}
