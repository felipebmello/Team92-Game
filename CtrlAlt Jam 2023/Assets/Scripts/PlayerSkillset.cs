using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillset : MonoBehaviour
{

    public enum PlayerState
    {
        Evil,
        Corrupt,
        Innocent,
        Good,
        Enlightned
    }
    [SerializeField] private PlayerState playerState = PlayerState.Innocent;
    [SerializeField] private BaseSkill currentSkill;
    private PlayerMovement playerMovement;
    private PlayerShooting playerShooting;

    private void Start() 
    {
        playerMovement = this.gameObject.GetComponent<PlayerMovement>();
        playerShooting = this.gameObject.GetComponent<PlayerShooting>();
        LevelSystem.Instance.OnSkillsOverlay += LevelSystem_OnSkillsOverlay;
        LevelSystem.Instance.OnChoosedSkill += LevelSystem_OnChoosedSkill;
    }

    private void LevelSystem_OnSkillsOverlay (object sender, BaseSkill[] skills)
    {
        TogglePlayerBehaviour(false);
    }

    private void LevelSystem_OnChoosedSkill(object sender, BaseSkill skill)
    {
        LearnNewSkill(skill);
    }



    private void TogglePlayerBehaviour (bool toggle)
    {
        playerMovement.enabled = toggle;
        playerShooting.enabled = toggle;
        CameraController.Instance.enabled = toggle;
    }

    public void LearnNewSkill (BaseSkill skill)
    {
        TogglePlayerBehaviour(true);
        currentSkill = skill;
        playerState = skill.State;
        playerMovement.SetMovementSpeed(skill.NewMovementSpeed);
        playerMovement.SetSprite(skill.NewSprite);
        playerShooting.SetBulletPrefab(skill.NewBulletPrefab);
        playerShooting.SetHoldToShoot(skill.NewHoldToShoot);
        playerShooting.SetFireRate(skill.NewFireRate);
    }
}