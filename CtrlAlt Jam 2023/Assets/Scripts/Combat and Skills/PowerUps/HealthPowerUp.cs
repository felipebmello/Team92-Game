using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthPowerUp : BasePowerUp
{
    public event EventHandler<float> OnPoweUpPickUp;
    [SerializeField] private float healValue = 25f;
    public override string GetName()
    {
        return "Health";
    }

    protected override void PickUp(GameObject player)
    {
        base.PickUp(player);
        player.gameObject.GetComponent<PlayerController>().HealPlayer(healValue);
    }
}
