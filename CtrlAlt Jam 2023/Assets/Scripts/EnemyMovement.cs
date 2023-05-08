using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 2;
    [SerializeField] float stoppingDistance = 2;
    private EnemyFOV enemyFOV;
    private Transform target;
    private Rigidbody2D myRigidbody;
    void Start()
    {
        enemyFOV = GetComponent<EnemyFOV>();
        myRigidbody = GetComponent<Rigidbody2D>();
        enemyFOV.OnSeeingTarget += EnemyFOV_OnSeeingTarget;
        enemyFOV.OnLosingTarget += EnemyFOV_OnLosingTarget;
    }

    private void OnDisable() 
    {
        enemyFOV.OnSeeingTarget -= EnemyFOV_OnSeeingTarget;
        enemyFOV.OnLosingTarget -= EnemyFOV_OnLosingTarget;
    }

    private void EnemyFOV_OnSeeingTarget(object sender, Transform target)
    {
        this.target = target;
    }
    
    private void EnemyFOV_OnLosingTarget(object sender, EventArgs e)
    {
        target = null;
    }

    private void FixedUpdate()
    {
        MoveEnemy();
    }

    private void MoveEnemy()
    {
        if (target != null && (Vector2.Distance(this.transform.position, target.position) > stoppingDistance))
        {
            Vector3 direction = (target.position - transform.position).normalized;
            myRigidbody.velocity = direction * movementSpeed;
        }
        else
        {
            myRigidbody.velocity = Vector3.zero;
        }
    }
}
