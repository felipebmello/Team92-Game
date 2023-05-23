using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private SpriteRenderer mySpriteRenderer;
    [Header("Movement Settings")]
    [Range(0f, 5f)]
    [SerializeField] float stoppingDistance = 2;
    private float movementSpeed = 2;
    [SerializeField] private float damageAmount = 0;
    private EnemyFOV enemyFOV;
    private Transform target;
    private Rigidbody2D myRigidbody;
    private bool facingRight = true;
    void Awake()
    {
        enemyFOV = GetComponent<EnemyFOV>();
        myRigidbody = GetComponent<Rigidbody2D>();
    }
    private void OnEnable() 
    {
        enemyFOV.OnSeeingTarget += EnemyFOV_OnSeeingTarget;
        enemyFOV.OnLosingTarget += EnemyFOV_OnLosingTarget;
    }

    private void OnDisable() 
    {
        enemyFOV.OnSeeingTarget -= EnemyFOV_OnSeeingTarget;
        enemyFOV.OnLosingTarget -= EnemyFOV_OnLosingTarget;
        myRigidbody.velocity = Vector3.zero;
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
                    mySpriteRenderer.gameObject.transform.Rotate(new Vector3 (0, 180, 0));
                    facingRight = true;
                }
                else if (this.transform.position.x > targetPosition.x && facingRight)
                {
                    //Olhar para a esquerda
                    mySpriteRenderer.gameObject.transform.Rotate(new Vector3 (0, 180, 0));
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
    public void SetMovementSpeed(float movementSpeed)
    {
        this.movementSpeed = movementSpeed;
    }
    public void SetDamageAmount(float damageAmount)
    {
        this.damageAmount = damageAmount;
    }
    public void SetSprite(Sprite sprite)
    {
        this.mySpriteRenderer.sprite = sprite;
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            if (other.gameObject.TryGetComponent<HealthSystem>(out HealthSystem healthSystem))
            {
                healthSystem.Damage(damageAmount, this.transform);
                StartCoroutine(StopAfterDamaging());
            }
        }
    }
    public float GetMovementSpeed()
    {
        return this.movementSpeed;
    }

    IEnumerator StopAfterDamaging() {
        this.enabled = false;
        yield return new WaitForSeconds(2f);
        this.enabled = true;
    }

}
