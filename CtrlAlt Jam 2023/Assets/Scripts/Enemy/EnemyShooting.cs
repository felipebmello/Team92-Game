using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : ShootProjectile
{
    private EnemyFOV enemyFOV;
    private Transform target;
    void Awake()
    {
        enemyFOV = GetComponent<EnemyFOV>();
    }
    void OnEnable()
    {
        enemyFOV.OnSeeingTarget += EnemyFOV_OnSeeingTarget;
        enemyFOV.OnLosingTarget += EnemyFOV_OnLosingTarget;
    }

    private void OnDisable() 
    {
        enemyFOV.OnSeeingTarget -= EnemyFOV_OnSeeingTarget;
        enemyFOV.OnLosingTarget -= EnemyFOV_OnLosingTarget;
    }

    protected override void Update() 
    {
        base.Update();
        if (target != null) 
        {
            targetDirection = (target.position - this.transform.position).normalized;
            base.FireBullet();
        }
    }
    private void EnemyFOV_OnSeeingTarget(object sender, Transform target)
    {
        fireTimer = fireRate;
        this.target = target;
    }
    
    private void EnemyFOV_OnLosingTarget(object sender, EventArgs e)
    {
        
        target = null;
    }

}
