using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCollectable : MonoBehaviour
{
    protected CircleCollider2D collectableCollider;

    public abstract string GetName();

    protected virtual void Start() 
    {
        collectableCollider = GetComponent<CircleCollider2D>();
    }
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Player") && !other.TryGetComponent<Bullet>(out Bullet bullet))
        {
            Debug.Log(other.name);
            PickUp(other.gameObject);
            Destroy(gameObject);
        }
    }

    protected abstract void PickUp(GameObject player);
}
