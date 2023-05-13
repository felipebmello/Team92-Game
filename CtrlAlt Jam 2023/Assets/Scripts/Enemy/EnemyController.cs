using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : SkillsetController
{
    [SerializeField] private bool isAffectedByPlayerChoice = false;
    [SerializeField] private bool isStrongVariant = false;
    private EnemyMovement enemyMovement;
    private EnemyShooting enemyShooting;

    protected override void Start() 
    {
        enemyMovement = this.gameObject.GetComponent<EnemyMovement>();
        enemyShooting = this.gameObject.GetComponent<EnemyShooting>();
        if (currentState == KarmaScriptableObject.KarmaState.TooInnocent && !isAffectedByPlayerChoice) enemyShooting = null;
        else ToggleEnemyBehaviour(true);
        base.Start();
        LevelSystem.Instance.OnNotChosenSkill += LevelSystem_OnNotChosenSkill;
    }
    protected override void OnDisable() 
    {
        base.OnDisable();
        LevelSystem.Instance.OnNotChosenSkill -= LevelSystem_OnNotChosenSkill;
    }
    protected override void LevelSystem_OnSkillsOverlay (object sender, SkillScriptableObject[] skills)
    {
        ToggleEnemyBehaviour(false);
    }
    protected void LevelSystem_OnNotChosenSkill (object sender, SkillScriptableObject skill)
    {
        ToggleEnemyBehaviour(true);
        if (isAffectedByPlayerChoice) 
        {
            CheckCurrentKarmaState(skill);
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

    public override void LearnNewSkill (SkillScriptableObject skill)
    {
        base.LearnNewSkill(skill);
        float strongVariantModifier = 1;
        if (isStrongVariant) 
        {
            strongVariantModifier = 3;
        }
        healthSystem.ApplyHealthModifier(strongVariantModifier);
        enemyMovement.SetMovementSpeed(skill.NewMovementSpeed / 3);
        enemyMovement.SetSprite(currentKarmaScrObj.NewSprite);
        if (enemyShooting != null)
        {
            enemyShooting.SetBulletPrefab(currentKarmaScrObj.NewBulletPrefab);
            enemyShooting.SetFireRate(skill.NewFireRate / strongVariantModifier);
        }
        else 
        {
            enemyMovement.SetDamageAmount(currentKarmaScrObj.NewBulletPrefab.GetComponent<Bullet>().GetDamageAmount());
        }
    }

    public HealthSystem GetHealthSystem()
    {
        return healthSystem;
    }

}
