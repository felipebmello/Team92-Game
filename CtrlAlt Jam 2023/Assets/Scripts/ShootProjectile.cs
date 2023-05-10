using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootProjectile : MonoBehaviour
{
    [Header("Shooting Settings")]
    [SerializeField] protected Transform bulletSpawnPoint;
    [SerializeField] protected Transform bulletPrefab;
    [Range(0.1f, 2f)]
    [SerializeField] protected float fireRate = 0.5f;
    protected Vector3 targetDirection;
    protected float fireTimer = 0f;

    protected virtual void Update() 
    {
        fireTimer -= Time.deltaTime;
    }
    protected virtual void FireBullet()
    {
        if (fireTimer <= 0f)
        {
            
            float angleInRadians = Mathf.Atan2(targetDirection.y, targetDirection.x);
            float angleInDegrees = angleInRadians * Mathf.Rad2Deg;
            Debug.Log(angleInDegrees);
            Debug.Log("Instantiate Bullet from "+gameObject.name);
            Transform bulletTransform = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
            bulletTransform.tag = this.tag;
            bulletTransform.Rotate(0, 0, angleInDegrees);
            Bullet bullet = bulletTransform.GetComponent<Bullet>();
            bullet.SetTarget(targetDirection);
            fireTimer = fireRate;
        }
    }
}
