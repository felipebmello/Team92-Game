using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : SkillsetController
{
    [SerializeField] private bool isAffectedByPlayerChoice = false;
    private EnemyMovement enemyMovement;
    private EnemyShooting enemyShooting;

    protected override void Start() 
    {
        enemyMovement = this.gameObject.GetComponent<EnemyMovement>();
        enemyShooting = this.gameObject.GetComponent<EnemyShooting>();
        if (currentState == SkillState.Innocent && !isAffectedByPlayerChoice) enemyShooting = null;
        else ToggleEnemyBehaviour(true);
        base.Start();
        LevelSystem.Instance.OnNotChosenSkill += LevelSystem_OnNotChosenSkill;
    }
    protected override void OnDisable() 
    {
        base.OnDisable();
        LevelSystem.Instance.OnNotChosenSkill -= LevelSystem_OnNotChosenSkill;
    }
    protected override void LevelSystem_OnSkillsOverlay (object sender, BaseSkill[] skills)
    {
        ToggleEnemyBehaviour(false);
    }
    protected void LevelSystem_OnNotChosenSkill (object sender, BaseSkill skill)
    {
        ToggleEnemyBehaviour(true);
        if (isAffectedByPlayerChoice) 
        {
            LearnNewSkill(skill);
        }
    }
        

    protected override void HealthSystem_OnDead(object sender, EventArgs e)
    {
        Debug.Log(gameObject+" is dead!");
        //Executar comportamento ao morrer, mover player para trás
        ToggleEnemyBehaviour(false);
        Destroy(this.gameObject, 1f);
    }
    private void ToggleEnemyBehaviour (bool toggle)
    {
        enemyMovement.enabled = toggle;
        if (enemyShooting != null) enemyShooting.enabled = toggle;
    }
    public override void LearnNewSkill (BaseSkill skill)
    {
        base.LearnNewSkill(skill);
        enemyMovement.SetMovementSpeed(skill.NewMovementSpeed/3);
        enemyMovement.SetSprite(skill.NewSprite);
        if (enemyShooting != null)
        {
            enemyShooting.SetBulletPrefab(skill.NewBulletPrefab);
            enemyShooting.SetFireRate(skill.NewFireRate);
        }
        else 
        {
            enemyMovement.SetDamageAmount(skill.NewBulletPrefab.GetComponent<Bullet>().GetDamageAmount());
        }
    }

}
