using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpeedPowerUp : BasePowerUp
{
    
    public event EventHandler<float> OnPoweUpPickUp;
    [SerializeField] private float speedModifier = 1.25f;
    public override string GetName()
    {
        return "Speed";
    }

    protected override void PickUp(GameObject player)
    {
        base.PickUp(player);
        player.gameObject.GetComponent<PlayerMovement>().SetSpeedModifier(speedModifier);
    }
}
