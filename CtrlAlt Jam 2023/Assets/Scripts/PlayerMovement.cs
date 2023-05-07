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
    
    private Vector2 movementInput;
    private Vector2 smoothedMovementInput;
    private Vector2 movementInputCurrentVelocity;
    private Rigidbody2D myRigidbody;

    private void Awake() 
    {
        //Inicializando os componentes
        myRigidbody = GetComponent<Rigidbody2D>();
    }
    
    public void OnMovement(InputValue inputValue) 
    {
        movementInput = inputValue.Get<Vector2>();
    }

    private void FixedUpdate()
    {
        LookAtMouseWorldPosition();
        MovePlayer();
    }

    private void LookAtMouseWorldPosition()
    {
        /*Vector3 position = MouseWorld.;
        if (this.transform.position.x < Input.mousePosition.x)
        {

        }*/
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
