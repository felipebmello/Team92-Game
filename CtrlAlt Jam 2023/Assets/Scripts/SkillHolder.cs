using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillHolder : BaseCollectable
{
    [SerializeField] protected BaseSkill skill;
    public override string GetName()
    {
        return skill.name;
    }

    protected override void PickUp(GameObject player)
    {
        player.gameObject.GetComponent<PlayerSkillset>().LearnNewSkill(skill);
    }
}
