using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private SpriteRenderer mySpriteRenderer;
    [Header("Movement Settings")]
    [Range(5f, 20f)]
    [SerializeField] private float movementSpeed;
    [Range(0.05f, 0.2f)]
    [SerializeField] private float smoothTime = 0.1f;


    /*[Header("Looking Settings")]
    [SerializeField] private float lookSpeed = 5f;*/
    
    private Vector2 movementInput;
    private Vector2 smoothedMovementInput;
    private Vector2 movementInputCurrentVelocity;
    private Rigidbody2D myRigidbody;
    private bool facingRight = true;
    [SerializeField] private float speedModifier = 1f;

    private void Awake() 
    {
        //Inicializando os componentes
        myRigidbody = GetComponent<Rigidbody2D>();
    }
    private void OnDisable() 
    {
        myRigidbody.velocity = Vector3.zero;
    }

    
    public void OnMovement(InputValue value) 
    {
        movementInput = value.Get<Vector2>();
    }

    private void FixedUpdate()
    {
        FlipToMouseWorldPosition();
        MovePlayer();
    }

    private void FlipToMouseWorldPosition()
    {
        Vector3 targetPosition = MouseWorld.GetPosition();
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
        //Vector3 lookDir = (targetPosition - this.transform.position).normalized;
        
    }

    private void MovePlayer()
    {
        smoothedMovementInput = Vector2.SmoothDamp(
            smoothedMovementInput,
            movementInput,
            ref movementInputCurrentVelocity,
            smoothTime);
        myRigidbody.velocity = smoothedMovementInput * movementSpeed * speedModifier;
    }

    public void SetSpeedModifier(float speedPowerUpValue)
    {
        this.speedModifier = speedPowerUpValue;
    }
    public void SetMovementSpeed(float movementSpeed)
    {
        this.movementSpeed = movementSpeed;
    }
    public void SetSprite(Sprite sprite)
    {
        this.mySpriteRenderer.sprite = sprite;
    }
}
