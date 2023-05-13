using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public event EventHandler OnDead;
    public event EventHandler<Transform> OnDamaged;
    public float health = 100;
    public float healthMax;
    
    private void Awake() 
    {
        healthMax = health;
    }

    public void ApplyHealthModifier(float healthModifier)
    {
        healthMax *= healthModifier;
        health = healthMax;
    }

    public void Damage (float damageAmount, Transform other)
    {
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
    
    private void Die() 
    {
        OnDead?.Invoke(this, EventArgs.Empty);
    }
}
