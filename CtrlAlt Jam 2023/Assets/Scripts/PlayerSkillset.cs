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
        //LearnNewSkill();
    }

    public void LearnNewSkill (BaseSkill skill)
    {
        currentSkill = skill;
        playerState = skill.State;
        playerMovement.SetMovementSpeed(skill.NewMovementSpeed);
        playerMovement.SetSprite(skill.NewSprite);
        playerShooting.SetBulletPrefab(skill.NewBulletPrefab);
        playerShooting.SetHoldToShoot(skill.NewHoldToShoot);
        playerShooting.SetFireRate(skill.NewFireRate);
    }
}
