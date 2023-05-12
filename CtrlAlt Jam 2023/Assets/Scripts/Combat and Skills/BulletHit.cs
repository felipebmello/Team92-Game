using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHit : MonoBehaviour
{
    private SkillsetController.KarmaState bulletState;
    private Animator myAnimator;

    private void Start() {
        myAnimator = GetComponent<Animator>();
    }

    public void SetBulletState(SkillsetController.KarmaState skillState)
    {
        myAnimator.SetInteger("State", (int) skillState);
        Destroy(gameObject, 0.0175f);

    }
}
