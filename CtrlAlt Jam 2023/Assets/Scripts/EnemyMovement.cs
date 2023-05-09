using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sprite;
    [Header("Movement Settings")]
    [Range(2f, 5f)]
    [SerializeField] float movementSpeed = 2;
    [Range(0f, 5f)]
    [SerializeField] float stoppingDistance = 2;
    private EnemyFOV enemyFOV;
    private Transform target;
    private Rigidbody2D myRigidbody;
    private bool facingRight = true;
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
        FlipToTargetPosition();
        MoveEnemy();
    }

    private void FlipToTargetPosition()
    {
        if(target != null)
        {
            Vector3 targetPosition = target.position;
            {
                if (this.transform.position.x <= targetPosition.x && !facingRight)
                {
                    //Olhar para a direita
                    sprite.gameObject.transform.Rotate(new Vector3 (0, 180, 0));
                    facingRight = true;
                }
                else if (this.transform.position.x > targetPosition.x && facingRight)
                {
                    //Olhar para a esquerda
                    sprite.gameObject.transform.Rotate(new Vector3 (0, 180, 0));
                    facingRight = false;
                }
            }
        }
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
