using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSystem : MonoBehaviour
{
    public static LevelSystem Instance { get; private set; }
    public event EventHandler<SkillScriptableObject[]> OnSkillsOverlay;
    public event EventHandler<SkillScriptableObject> OnChoosedSkill;
    public event EventHandler<SkillScriptableObject> OnNotChosenSkill;
    public event EventHandler<float> OnPlayerMaxHealthChanged;
    public event EventHandler<float> OnPlayerDamaged;
    public event EventHandler OnPlayerDeath;
    private void Awake() 
    {
        if (Instance != null)
        {
            Debug.LogError("There's more than one LevelSystem! "+ transform + " - " + Instance);
            Destroy(gameObject);
        }
        Instance = this;

    }

    public void SkillHolderActivated(SkillScriptableObject[] skills)
    {
        if (skills == null) Debug.Log("No skills received.");
        OnSkillsOverlay?.Invoke(this, skills);
    } 

    public void ChoosedSkill(SkillScriptableObject skill)
    {
        OnChoosedSkill?.Invoke(this, skill);
    }
    
    public void SkillNotChosen(SkillScriptableObject skill)
    {
        OnNotChosenSkill?.Invoke(this, skill);
    }
    
    public void PlayerHealthChanged(HealthSystem healthSystem)
    {
        float totalNumberOfHearts = (healthSystem.GetHealthMax()) / 25;
        OnPlayerMaxHealthChanged?.Invoke(this, totalNumberOfHearts);
    }
    public void PlayerDamaged(float currentHealth)
    {
        float numberOfFullHearts = currentHealth / 25;
        OnPlayerDamaged?.Invoke(this, numberOfFullHearts);
    }
    public void PlayerKilled()
    {
        OnPlayerDeath?.Invoke(this, EventArgs.Empty);
    }

    
    public void EnterNextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        else SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
