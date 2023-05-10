using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : SkillsetController
{
    private PlayerMovement playerMovement;
    private PlayerShooting playerShooting;

    protected override void Start() 
    {
        playerMovement = this.gameObject.GetComponent<PlayerMovement>();
        playerShooting = this.gameObject.GetComponent<PlayerShooting>();
        base.Start();
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
    }


    protected override void HealthSystem_OnDead(object sender, EventArgs e)
    {
        //Executar comportamento ao morrer, mover player para trás
        Debug.Log(gameObject+" is dead!");
        TogglePlayerBehaviour(false);
        LevelSystem.Instance.PlayerKilled();
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
        TogglePlayerBehaviour(true);

        base.LearnNewSkill(skill);

        playerMovement.SetMovementSpeed(skill.NewMovementSpeed);
        playerMovement.SetSprite(skill.NewSprite);
        playerShooting.SetBulletPrefab(skill.NewBulletPrefab);
        playerShooting.SetHoldToShoot(skill.NewHoldToShoot);
        playerShooting.SetFireRate(skill.NewFireRate);
    }
    
    private void OnQuit() {
        Application.Quit();
    }
}
