using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    public static LevelSystem Instance { get; private set; }
    public event EventHandler<BaseSkill[]> OnSkillsOverlay;
    public event EventHandler<BaseSkill> OnChoosedSkill;
    private void Awake() 
    {
        if (Instance != null)
        {
            Debug.LogError("There's more than one LevelSystem! "+ transform + " - " + Instance);
            Destroy(gameObject);
        }
        Instance = this;
    }

    public void SkillHolderActivated(BaseSkill[] skills)
    {
        if (skills == null) Debug.Log("No skills received.");
        OnSkillsOverlay?.Invoke(this, skills);
    } 

    public void ChoosedSkill(BaseSkill skill)
    {
        OnChoosedSkill?.Invoke(this, skill);
    }
}
