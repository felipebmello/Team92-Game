using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerUp : BasePowerUp
{
    private int speedModifier;
    public override string GetName()
    {
        return "SpeedPowerUp";
    }

    protected override void OnPickUp(Transform playerTransform)
    {
        playerTransform.GetComponent<PlayerMovement>().SetSpeedModifier(speedModifier);
    }
}
