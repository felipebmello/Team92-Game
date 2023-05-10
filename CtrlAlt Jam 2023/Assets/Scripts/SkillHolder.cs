using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillHolder : BaseCollectable
{
    [SerializeField] protected BaseSkill[] skills;
    public override string GetName()
    {
        return "Skill List";
    }

    protected override void PickUp(GameObject player)
    {
        LevelSystem.Instance.SkillHolderActivated(skills);
        //player.gameObject.GetComponent<PlayerSkillset>().LearnNewSkill(skill);
    }
}
