using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : ShootProjectile
{
    [SerializeField] private bool holdToShoot = false;
    private bool buttonPressed = false;

    //Bug detectado, 
    protected void OnFire(InputValue value)
    {   
        if (!holdToShoot && value.isPressed) FireBullet();
        else
        {
            buttonPressed = value.isPressed;
        }
    }

    protected override void Update() 
    {
        base.Update();
        if (holdToShoot && buttonPressed) FireBullet();
    }

    protected override void FireBullet()
    {
        targetDirection = (MouseWorld.GetPosition() - this.transform.position).normalized;
        base.FireBullet();
    }
}
