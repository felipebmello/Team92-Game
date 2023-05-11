using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootProjectile : MonoBehaviour
{
    [Header("Shooting Settings")]
    [SerializeField] protected Transform bulletSpawnPoint;
    [SerializeField] protected Transform bulletPrefab;
    protected float fireRate;
    protected Vector3 targetDirection;
    protected float fireTimer = 0f;
    
    [Header("Shooting SFX Settings")]
    [SerializeField] protected AudioClip shootingSFX;
    [SerializeField] float shootingSFXVolume = 0.75f;

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
            Debug.Log(bulletPrefab);
            
            AudioSource.PlayClipAtPoint(shootingSFX, AudioManager.Instance.GetAudioListener().transform.position, shootingSFXVolume);

            Transform bulletTransform = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
            bulletTransform.tag = this.tag;
            bulletTransform.gameObject.layer = this.gameObject.layer;
            bulletTransform.Rotate(0, 0, angleInDegrees);
            Bullet bullet = bulletTransform.GetComponent<Bullet>();
            bullet.SetTarget(targetDirection);
            fireTimer = fireRate;
        }
    }

    public void SetBulletPrefab (Transform bulletPrefab)
    {
        this.bulletPrefab = bulletPrefab;
    }
    
    public void SetFireRate (float fireRate)
    {
        this.fireRate = fireRate;
    }
}
