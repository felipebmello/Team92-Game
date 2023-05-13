using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthPowerUp : BasePowerUp
{
    
    public event EventHandler<float> OnPoweUpPickUp;
    public override string GetName()
    {
        return "Health";
    }

    protected override void PickUp(GameObject player)
    {
        var healthSystem = player.gameObject.GetComponent<HealthSystem>();
        if(healthSystem.health < healthSystem.healthMax)
        {
            healthSystem.health += 50;
        }
    }
}
