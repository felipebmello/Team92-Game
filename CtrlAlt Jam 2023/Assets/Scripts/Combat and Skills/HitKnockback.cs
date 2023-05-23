using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitKnockback : MonoBehaviour
{
     public float thrust;

    private Rigidbody2D myRigidbody;

    private void Awake() {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    public void ObjectHit(Transform other)
    {
        StartCoroutine(KnockbackCo(0.5f, thrust, other));

    }

    public IEnumerator KnockbackCo (float knockbackDuration, float knockbackPower, Transform obj) {
        float timer = 0;
        while (knockbackDuration > timer) {
            timer += Time.deltaTime;
            Vector2 direction = (obj.transform.position - transform.position).normalized;
            myRigidbody.AddForce(-direction * knockbackPower);
        }
        
        yield return 0;
    }
}
