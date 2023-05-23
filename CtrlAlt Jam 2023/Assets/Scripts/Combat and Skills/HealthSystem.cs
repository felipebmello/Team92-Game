using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public event EventHandler OnDead;
    public event EventHandler<Transform> OnDamaged;
    [SerializeField] private float health = 100;
    [SerializeField] private float healthMax;

    [SerializeField] private float lastHealthModifier = 1f;
    private float lastDamageSuffered;
    
    private void Awake() 
    {
        healthMax = health;
    }

    public void ApplyHealthModifier(float healthModifier)
    {
        healthMax *= healthModifier;
        if (health > healthMax) health = healthMax;
    }

    public void ReplenishFullHealth()
    {
        health = healthMax;
    }

    public void Damage (float damageAmount, Transform other)
    {
        lastDamageSuffered = damageAmount;
        health -= damageAmount;
        if (health < 0)
        {
            health = 0;
        }

        if (health == 0)
        {
            Die();
        }
        OnDamaged?.Invoke(this, other);
    }
    
    public void Heal (float healAmount)
    {
        if (health + healAmount <= healthMax) health += healAmount;
        else health = healthMax;
    }
    
    private void Die() 
    {
        OnDead?.Invoke(this, EventArgs.Empty);
    }

    public float GetHealthMax()
    {
        return healthMax;
    }
    
    public float GetHealth()
    {
        return health;
    }
    public void SetHealthMax(float healthMax)
    {
        this.healthMax = healthMax;
    }
    
    public void SetHealth(float health)
    {
        this.health = health;
    }
    public float GetLastDamageSuffered()
    {
        return lastDamageSuffered;
    }

    public float GetLastHealthModifier()
    {
        return lastHealthModifier;
    }
    public void SetLastHealthModifier(float healthModifier)
    {
        this.lastHealthModifier = healthModifier;
    }
}
