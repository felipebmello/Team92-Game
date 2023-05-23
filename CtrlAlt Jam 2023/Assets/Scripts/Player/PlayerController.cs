using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : SkillsetController
{
    private PlayerMovement playerMovement;
    private PlayerShooting playerShooting;
    [SerializeField] private Animator skillUpAnimator;
    [SerializeField] private float skillUpAnimationTimer;
    [SerializeField] protected AudioClip playerSkillUpSFX;
    [SerializeField] protected AudioClip playerDeathSFX;
    
    protected override void Start() 
    {
        playerMovement = this.gameObject.GetComponent<PlayerMovement>();
        playerShooting = this.gameObject.GetComponent<PlayerShooting>();
        base.Start();
        playerMovement.SetSprite(currentKarmaScrObj.NewSprite);
        TogglePlayerBehaviour(true);
        LevelSystem.Instance.OnChoosedSkill += LevelSystem_OnChoosedSkill;
        LevelSystem.Instance.OnEnemyDeath += LevelSystem_OnEnemyDeath;
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

    protected void LevelSystem_OnEnemyDeath (object sender, EventArgs e)
    {
        //Incluir lógica da skill "Babão".
        if (playerShooting.GetHealingShot())
        {
            int chance = UnityEngine.Random.Range(1, 5);
            if (chance == 1)
            {
                HealPlayer(25f);
            }
        }
    }

    protected override void HealthSystem_OnDead (object sender, EventArgs e)
    {
        LevelSystem.Instance.PlayerDamaged(0);
        //Executar comportamento ao morrer, mover player para trás
        TogglePlayerBehaviour(false);
        LevelSystem.Instance.PlayerKilled();
        AudioSource.PlayClipAtPoint(playerDeathSFX, AudioManager.Instance.GetAudioListener().transform.position, controllerSFXVolume);
        Destroy(gameObject, 1f);
    }

    protected override void HealthSystem_OnDamaged(object sender, Transform other)
    {
        LevelSystem.Instance.PlayerDamaged(healthSystem.GetHealth());
        base.HealthSystem_OnDamaged(sender, other);
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
        if (currentSkill.BackShot) playerShooting.SetBackShot(true);
        if (currentSkill.TripleShot) playerShooting.SetTripleShot(true);
        if (currentSkill.HealingShot) playerShooting.SetHealingShot(true);
    }
    
    protected override void SaveSkillsetData()
    {
        base.SaveSkillsetData();
        savedData.MovementSpeed = playerMovement.GetMovementSpeed();
        savedData.BulletPrefab = playerShooting.GetBulletPrefab();
        savedData.HoldToShoot = playerShooting.GetHoldToShoot();
        savedData.FireRate = playerShooting.GetFireRate();
        savedData.BackShot = playerShooting.GetBackShot();
        savedData.TripleShot = playerShooting.GetTripleShot();
        savedData.HealingShot = playerShooting.GetHealingShot();
        savedData.HealthMax = healthSystem.GetHealthMax() / healthSystem.GetLastHealthModifier();
        savedData.Health = healthSystem.GetHealth();
    }

    protected override void LoadSkillsetData()
    {
        base.LoadSkillsetData();
        playerMovement.SetMovementSpeed(savedData.CurrentSkill.NewMovementSpeed);
        playerShooting.SetBulletPrefab(savedData.CurrentKarmaScrObj.NewBulletPrefab);
        playerShooting.SetHoldToShoot(savedData.CurrentSkill.NewHoldToShoot);
        playerShooting.SetFireRate(savedData.CurrentSkill.NewFireRate);
        playerShooting.SetBackShot(savedData.BackShot);
        playerShooting.SetTripleShot(savedData.TripleShot);
        playerShooting.SetHealingShot(savedData.HealingShot);
        healthSystem.SetHealthMax(savedData.HealthMax);
        healthSystem.SetHealth(savedData.Health);
    }

    protected override void ModifyHealthSystem(float healthModifier)
    {
        base.ModifyHealthSystem(healthModifier);
        LevelSystem.Instance.PlayerHealthChanged(healthSystem);
        LevelSystem.Instance.PlayerDamaged(healthSystem.GetHealth());
    }

    protected override void ReplenishHealthSystem()
    {
        base.ReplenishHealthSystem();
        LevelSystem.Instance.PlayerHealed(healthSystem.GetHealth());
    }

    public void HealPlayer (float healAmount)
    {
        healthSystem.Heal(healAmount);
        LevelSystem.Instance.PlayerHealed(healthSystem.GetHealth());
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
