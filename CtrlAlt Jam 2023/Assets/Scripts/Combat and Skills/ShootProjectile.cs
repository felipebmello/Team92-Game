using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootProjectile : MonoBehaviour
{
    [Header("Shooting Settings")]
    [SerializeField] protected Transform bulletSpawnPoint;
    [SerializeField] protected Transform bulletPrefab;
    [SerializeField] protected float fireRate = 1f;

    [SerializeField] protected float fireRateModifier = 1f;
    [SerializeField] protected Vector2 targetDirection;
    protected float fireTimer = 0f;
    
    [Header("Shooting SFX Settings")]
    [SerializeField] protected AudioClip shootingSFX;
    [SerializeField] protected float shootingSFXVolume = 0.75f;
    [SerializeField] private float bulletDamage;
    [SerializeField] private bool backShot = false;
    [SerializeField] private bool tripleShot = false;
    [SerializeField] protected float tripleShotAngle = 15f;
    [SerializeField] private bool healingShot = false;
    [SerializeField] protected Animator myAnimator;
    

    protected virtual void Update() 
    {
        bulletDamage = bulletPrefab.GetComponent<Bullet>().GetDamageAmount();
        fireTimer -= Time.deltaTime;
    }
    protected virtual void FireBullet()
    {
        if (fireTimer <= 0f)
        {
            float angleInRadians = Mathf.Atan2(targetDirection.y, targetDirection.x);
            float angleInDegrees = angleInRadians * Mathf.Rad2Deg;
            //Debug.Log(bulletPrefab);
            myAnimator.SetTrigger("Attack");

            AudioSource.PlayClipAtPoint(shootingSFX, AudioManager.Instance.GetAudioListener().transform.position, shootingSFXVolume);
            CreateBulletWithDirection(bulletSpawnPoint.position, angleInDegrees, targetDirection);
            if (backShot) 
            {
                Vector2 newPosition = new Vector2 (transform.position.x-bulletSpawnPoint.localPosition.x, bulletSpawnPoint.position.y);
                CreateBulletWithDirection(newPosition, angleInDegrees+180, -targetDirection);
            }
            if (tripleShot) 
            {
                //Melhorar a lógica abaixo
                CreateBulletWithDirection(bulletSpawnPoint.position, angleInDegrees+tripleShotAngle, Quaternion.Euler(0, 0, tripleShotAngle) * targetDirection);
                CreateBulletWithDirection(bulletSpawnPoint.position, angleInDegrees-tripleShotAngle, Quaternion.Euler(0, 0, -tripleShotAngle) * targetDirection);
            }
            fireTimer = (fireRate / fireRateModifier);
        }
    }

    private void CreateBulletWithDirection(Vector2 position, float angleInDegrees, Vector2 targetDirection)
    {
        Transform bulletTransform = Instantiate(bulletPrefab, position, Quaternion.identity);
        bulletTransform.tag = this.tag;
        bulletTransform.gameObject.layer = this.gameObject.layer;
        bulletTransform.Rotate(0, 0, angleInDegrees);
        Bullet bullet = bulletTransform.GetComponent<Bullet>();
        bullet.SetTarget(targetDirection);
    }

    public void ApplyFireRateModifier (float modifier)
    {
        fireRateModifier = modifier;
    }

    public void SetBulletPrefab (Transform bulletPrefab)
    {
        this.bulletPrefab = bulletPrefab;
    }
    
    public void SetFireRate (float fireRate)
    {
        this.fireRate = fireRate;
    }
    
    public Transform GetBulletPrefab ()
    {
        return this.bulletPrefab;
    }
    
    public float GetFireRate ()
    {
        return this.fireRate;
    }
    
    public bool GetBackShot()
    {
        return this.backShot;
    }

    public void SetBackShot (bool hasBackShot)
    {
        this.backShot = hasBackShot;
    }
    
    public bool GetTripleShot()
    {
        return this.tripleShot;
    }

    public void SetTripleShot (bool hasTripleShot)
    {
        this.tripleShot = hasTripleShot;
    }
    
    public bool GetHealingShot()
    {
        return this.healingShot;
    }
    public void SetHealingShot (bool hasHealingShot)
    {
        this.healingShot = hasHealingShot;
    }
    
}
