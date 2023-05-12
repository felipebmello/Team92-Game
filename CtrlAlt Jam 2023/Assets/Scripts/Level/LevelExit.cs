using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExit : BaseCollectable
{
    public override string GetName()
    {
        return "Skill List";
    }

    protected override void PickUp(GameObject player)
    {
        LevelSystem.Instance.EnterNextLevel();
    }
}
