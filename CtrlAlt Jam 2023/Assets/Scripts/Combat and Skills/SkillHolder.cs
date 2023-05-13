using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillHolder : BaseCollectable
{
    [SerializeField] Animator myAnimator;
    [SerializeField] protected SkillScriptableObject[] skills;
    public bool isActive = true;
    public override string GetName()
    {
        return "Skill List";
    }
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (isActive)
        {
            if (other.tag.Equals("Player") && !other.TryGetComponent<Bullet>(out Bullet bullet))
            {
                StartCoroutine(ActivateSkillAltar(other));
            }

        }
    }

    protected override void PickUp(GameObject player)
    {
        LevelSystem.Instance.SkillHolderActivated(skills);
        //player.gameObject.GetComponent<PlayerSkillset>().LearnNewSkill(skill);
    }

    public IEnumerator ActivateSkillAltar(Collider2D other)
    {
        myAnimator.SetTrigger("Interacted");
        yield return new WaitForSeconds(1f);
        isActive = false;
        PickUp(other.gameObject);
    }
    
}
