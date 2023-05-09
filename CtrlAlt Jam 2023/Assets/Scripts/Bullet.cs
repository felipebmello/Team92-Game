using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Settings")]
    [Range(1f, 15f)]
    [SerializeField] private float speed;
    [SerializeField] private float damageAmount = 100f;
    [Range(1f, 10f)]
    [SerializeField] private float lifetime;

    private Vector3 direction;
    private Rigidbody2D myRigidbody;
    public void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifetime);
    }

    private void FixedUpdate() 
    {
        myRigidbody.velocity = direction * speed;
    }

    public void SetTarget(Vector3 targetDirection)
    {
        this.direction = targetDirection;
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.tag != this.tag)
        {
            Debug.Log(this.tag+", "+gameObject.name+", "+gameObject.layer+" + "+other.gameObject.tag+", "+other.gameObject.name+", "+other.gameObject.layer);
            if (other.gameObject.TryGetComponent<HealthSystem>(out HealthSystem healthSystem))
            {
                healthSystem.Damage(damageAmount);
            }
            Destroy(gameObject);
        }
    }
}
