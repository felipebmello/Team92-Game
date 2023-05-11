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
        base.Start();
        playerMovement.SetSprite(currentSkill.NewSprite);
        TogglePlayerBehaviour(true);
        LevelSystem.Instance.OnChoosedSkill += LevelSystem_OnChoosedSkill;
    }
    protected override void OnDisable() 
    {
        base.OnDisable();
        LevelSystem.Instance.OnChoosedSkill -= LevelSystem_OnChoosedSkill;
    }

    protected override void LevelSystem_OnSkillsOverlay (object sender, BaseSkill[] skills)
    {
        TogglePlayerBehaviour(false);
    }
    protected void LevelSystem_OnChoosedSkill (object sender, BaseSkill skill)
    {
        LearnNewSkill(skill);
        
        StartCoroutine(SkillUpEffects(skill));
    }


    protected override void HealthSystem_OnDead(object sender, EventArgs e)
    {
        //Executar comportamento ao morrer, mover player para trás
        Debug.Log(gameObject+" is dead!");
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


    public override void LearnNewSkill (BaseSkill skill)
    {
        base.LearnNewSkill(skill);
        playerMovement.SetMovementSpeed(skill.NewMovementSpeed);
        playerShooting.SetBulletPrefab(skill.NewBulletPrefab);
        playerShooting.SetHoldToShoot(skill.NewHoldToShoot);
        playerShooting.SetFireRate(skill.NewFireRate);
    }
    
    private void OnQuit() {
        Application.Quit();
    }

    public IEnumerator SkillUpEffects(BaseSkill skill)
    {
        int numberOfSkillStates = System.Enum.GetValues(typeof(SkillsetController.SkillState)).Length / 2;
        if ((int) skill.State >= numberOfSkillStates) skillUpAnimator.SetTrigger("GoodTrigger");
        else skillUpAnimator.SetTrigger("EvilTrigger");
        AudioSource.PlayClipAtPoint(playerSkillUpSFX, AudioManager.Instance.GetAudioListener().transform.position, controllerSFXVolume);
        yield return new WaitForSeconds(1f);
        playerMovement.SetSprite(skill.NewSprite);
        TogglePlayerBehaviour(true);

    }
}
