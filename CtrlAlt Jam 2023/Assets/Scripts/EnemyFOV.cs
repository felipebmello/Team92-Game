using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFOV : MonoBehaviour
{
    [SerializeField] private float sightRange;
    private CircleCollider2D sightCollider;
    public event EventHandler<Transform> OnSeeingTarget;
    public event EventHandler OnLosingTarget;

    
    
    private void Start() {
        sightCollider = GetComponent<CircleCollider2D>();
        sightCollider.radius = sightRange;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag.Equals("Player"))
        {
            OnSeeingTarget?.Invoke(this, other.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        OnLosingTarget?.Invoke(this, EventArgs.Empty);
    }
}
