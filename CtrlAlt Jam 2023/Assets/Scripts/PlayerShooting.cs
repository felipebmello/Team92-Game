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
        Debug.Log("Fire called"+value.GetType()+value.isPressed);
        buttonPressed = value.isPressed;
    }

    protected override void Update() 
    {
        base.Update();
        if (buttonPressed) 
        {
            FireBullet();
            if (!holdToShoot) buttonPressed = false;
        }
    }

    protected override void FireBullet()
    {
        targetDirection = (MouseWorld.GetPosition() - this.transform.position).normalized;
        base.FireBullet();
    }
}
