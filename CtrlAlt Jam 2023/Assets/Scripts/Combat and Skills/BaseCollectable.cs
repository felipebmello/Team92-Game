using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCollectable : MonoBehaviour
{
    protected CircleCollider2D collectableCollider;
    protected PowerUpPopUp popUp;

    public abstract string GetName();

    protected void Awake()
    {
        popUp = GameObject.Find("PowerUpPopUp").GetComponent<PowerUpPopUp>();
    }
    protected virtual void Start() 
    {
        collectableCollider = GetComponent<CircleCollider2D>();
    }
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Player") && !other.TryGetComponent<Bullet>(out Bullet bullet))
        {
            //finds popup
            string powerUpName = GetName();
            PickUp(other.gameObject);
            popUp.ShowPowerUp(powerUpName);
            Destroy(gameObject);
        }
    }

    protected abstract void PickUp(GameObject player);
}
