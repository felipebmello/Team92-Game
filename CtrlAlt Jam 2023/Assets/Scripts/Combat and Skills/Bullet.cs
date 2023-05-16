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
    [SerializeField] private Transform bulletHitPrefab;
    [SerializeField] KarmaScriptableObject.KarmaState bulletState;
    private CircleCollider2D bulletCollider;

    private Vector2 direction;
    private Rigidbody2D myRigidbody;
    public void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        bulletCollider = GetComponent<CircleCollider2D>();
        Destroy(gameObject, lifetime);
    }

    private void FixedUpdate() 
    {
        myRigidbody.velocity = direction * speed;
    }

    public void SetTarget(Vector2 targetDirection)
    {
        this.direction = targetDirection;
    }

    public CircleCollider2D GetBulletCollider()
    {
        return bulletCollider;
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.TryGetComponent<Bullet>(out Bullet bullet))
        {
           Physics2D.IgnoreCollision(this.bulletCollider, bullet.GetBulletCollider());
           return;
        }
        if (other.gameObject.tag == this.tag)
        {
           Physics2D.IgnoreCollision(this.bulletCollider, other.gameObject.GetComponent<Collider2D>());

        }
        if (other.gameObject.tag != this.tag)
        {
            if (other.gameObject.TryGetComponent<HealthSystem>(out HealthSystem healthSystem))
            {
                healthSystem.Damage(damageAmount, this.transform);
            }
            Instantiate(bulletHitPrefab, this.transform.position, this.transform.rotation).GetComponent<BulletHit>().SetBulletState(bulletState);
            Destroy(gameObject);
        }
    }

    public float GetDamageAmount()
    {
        return damageAmount;
    }
}
