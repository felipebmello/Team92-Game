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
        //Executar comportamento ao morrer, mover player para trás
        ToggleEnemyBehaviour(false);
        Destroy(this.gameObject, 0.25f);
    }
    private void ToggleEnemyBehaviour (bool toggle)
    {
        enemyMovement.enabled = toggle;
        if (enemyShooting != null) enemyShooting.enabled = toggle;
    }

    public override void LearnNewSkill (SkillScriptableObject skill)
    {
        base.LearnNewSkill(skill);
        float strongVariantModifier = 1f;
        if (isStrongVariant) 
        {
            strongVariantModifier = 1.5f;
        }
        if (currentState == KarmaScriptableObject.KarmaState.TooInnocent) strongVariantModifier = 0.5f;
        healthSystem.ApplyHealthModifier(strongVariantModifier);
        enemyMovement.SetMovementSpeed(skill.NewMovementSpeed / 2);
        enemyMovement.SetSprite(currentKarmaScrObj.NewSprite);
        if (enemyShooting != null)
        {
            enemyShooting.SetBulletPrefab(currentKarmaScrObj.NewBulletPrefab);
            enemyShooting.SetFireRate(skill.NewFireRate);
            enemyShooting.ApplyFireRateModifier(strongVariantModifier);
            if (currentSkill.BackShot) enemyShooting.SetBackShot(true);
        }
        else 
        {
            enemyMovement.SetDamageAmount(currentKarmaScrObj.NewBulletPrefab.GetComponent<Bullet>().GetDamageAmount());
        }
    }
    
    protected override void SaveSkillsetData()
    {
        Debug.Log(savedData);
        base.SaveSkillsetData();
        savedData.MovementSpeed = enemyMovement.GetMovementSpeed();
        savedData.BulletPrefab = enemyShooting.GetBulletPrefab();
        savedData.FireRate = enemyShooting.GetFireRate();
        savedData.BackShot = enemyShooting.GetBackShot();
        Debug.Log(enemyShooting.GetFireRate());
    }

    protected override void LoadSkillsetData()
    {
        float strongVariantModifier = 1f;
        if (isStrongVariant) 
        {
            strongVariantModifier = 1.5f;
        }
        base.LoadSkillsetData();
        enemyMovement.SetMovementSpeed(savedData.CurrentSkill.NewMovementSpeed / 2);
        enemyShooting.SetBulletPrefab(savedData.CurrentKarmaScrObj.NewBulletPrefab);
        enemyShooting.SetFireRate(savedData.CurrentSkill.NewFireRate);
        enemyShooting.SetBackShot(savedData.BackShot);
        enemyShooting.ApplyFireRateModifier(strongVariantModifier);
    }

    public HealthSystem GetHealthSystem()
    {
        return healthSystem;
    }

}
