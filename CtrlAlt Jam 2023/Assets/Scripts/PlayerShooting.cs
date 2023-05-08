using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : ShootProjectile
{
    [SerializeField] private bool holdToShoot = false;
    private bool buttonPressed = false;

    protected void OnFire(InputValue value)
    {   
        if (!holdToShoot && value.isPressed) FireProjectile();
        else
        {
            buttonPressed = value.isPressed;
        }
    }

    protected override void Update() 
    {
        base.Update();
        if (holdToShoot && buttonPressed) FireProjectile();
    }

    protected override void FireProjectile()
    {
        targetDirection = (MouseWorld.GetPosition() - this.transform.position).normalized;
        base.FireProjectile();
    }
}
