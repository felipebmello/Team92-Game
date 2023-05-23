using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DamagePowerUp : BasePowerUp
{
    
    public event EventHandler<float> OnPoweUpPickUp;
    [SerializeField] private float damageModifier = 1.25f;
    public override string GetName()
    {
        return "Damage";
    }

    protected override void PickUp(GameObject player)
    {
        base.PickUp(player);
        player.gameObject.GetComponent<PlayerShooting>().SetDamageModifier(damageModifier);
    }
}
