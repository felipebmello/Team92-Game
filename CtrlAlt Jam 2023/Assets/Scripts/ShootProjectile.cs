using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootProjectile : MonoBehaviour
{
    [Header("Shooting Settings")]
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private Transform bulletPrefab;
    [Range(0.1f, 1f)]
    [SerializeField] protected float fireRate = 0.5f;
    protected Vector3 targetDirection;
    protected float fireTimer = 0f;

    protected virtual void Update() 
    {
        fireTimer -= Time.deltaTime;
    }
    protected virtual void FireProjectile()
    {
        if (fireTimer <= 0f)
        {
            Transform bulletTransform = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            bulletTransform.tag = this.tag;
            Bullet bullet = bulletTransform.GetComponent<Bullet>();
            bullet.SetTarget(targetDirection);
            fireTimer = fireRate;
        }
    }
}
