using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FireGun : MonoBehaviour
{
    [Header("Shooting Settings")]
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private Transform bulletPrefab;
    [Range(0.1f, 1f)]
    [SerializeField] private float fireRate = 0.5f;
    [SerializeField] private bool holdToShoot = false;
    private bool buttonPressed = false;
    private float fireTimer = 0f;
    
    public void OnFire(InputValue value)
    {   
        if (!holdToShoot && value.isPressed) FireBullet();
        else
        {
            buttonPressed = value.isPressed;
        }
    }

    private void FireBullet()
    {
        if (fireTimer <= 0f)
        {
            Transform bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            bullet.tag = this.tag;
            fireTimer = fireRate;
        }
    }

    private void Update() 
    {
        fireTimer -= Time.deltaTime;
        if (holdToShoot && buttonPressed) FireBullet();
    }
}
