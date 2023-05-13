using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DamagePowerUp : BasePowerUp
{
    
    public event EventHandler<float> OnPoweUpPickUp;
    [SerializeField] private float speedModifier = 1.25f;
    public override string GetName()
    {
        return "Damage";
    }

    protected override void PickUp(GameObject player)
    {
        player.gameObject.GetComponent<PlayerMovement>().SetSpeedModifier(speedModifier);
    }
}
