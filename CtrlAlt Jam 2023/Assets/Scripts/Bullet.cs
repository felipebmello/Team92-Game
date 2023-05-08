using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Settings")]
    [Range(1f, 15f)]
    [SerializeField] private float speed;
    [Range(1f, 10f)]
    [SerializeField] private float lifetime;

    private Vector3 direction;
    private Rigidbody2D myRigidbody;
    public void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        direction = (MouseWorld.GetPosition()-this.transform.position).normalized;
        Destroy(gameObject, lifetime);
    }

    private void FixedUpdate() 
    {
        myRigidbody.velocity = direction * speed;
    }
}
