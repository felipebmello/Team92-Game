using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
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

    private void Awake() 
    {
        //Inicializando os componentes
        myRigidbody = GetComponent<Rigidbody2D>();
    }
    
    public void OnMovement(InputValue value) 
    {
        movementInput = value.Get<Vector2>();
    }

    private void FixedUpdate()
    {
        LookAtMouseWorldPosition();
        MovePlayer();
    }

    private void LookAtMouseWorldPosition()
    {
        Vector3 targetPosition = MouseWorld.GetPosition();
        if (this.transform.position.x <= Input.mousePosition.x)
        {
            //Olhar para a direita

        }
        else
        {
            //Olhar para a esquerda
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
        myRigidbody.velocity = smoothedMovementInput * movementSpeed;
    }
}
