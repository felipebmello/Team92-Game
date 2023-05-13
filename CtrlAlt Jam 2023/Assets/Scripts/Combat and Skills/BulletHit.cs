using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHit : MonoBehaviour
{
    private KarmaScriptableObject.KarmaState bulletState;
    private Animator myAnimator;

    private void Awake() {
        myAnimator = GetComponent<Animator>();
    }

    public void SetBulletState(KarmaScriptableObject.KarmaState karmaState)
    {
        int karmaStateValue = (int) karmaState;
        /*Debug.Log(karmaState + karmaStateValue);
        Debug.Log(myAnimator);*/
        myAnimator.SetInteger("State", karmaStateValue);
        Destroy(gameObject, 0.025f);

    }
}
