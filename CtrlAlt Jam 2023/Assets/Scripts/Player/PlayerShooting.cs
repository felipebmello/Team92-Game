using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : ShootProjectile
{
    [SerializeField] private bool holdToShoot = false;
    private bool buttonPressed = false;

    private void OnDisable() 
    {
        buttonPressed = false;
    }

    //Bug detectado, 
    protected void OnFire(InputValue value)
    {
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
        Vector2 playerPosition = this.transform.position;
        targetDirection = MouseWorld.GetPosition() - playerPosition;
        targetDirection.Normalize();
        base.FireBullet();
    }
    public void SetHoldToShoot (bool holdToShoot)
    {
        this.holdToShoot = holdToShoot;
    }
    
    public bool GetHoldToShoot ()
    {
        return this.holdToShoot;
    }
}
