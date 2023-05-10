using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SkillChoiceOverlayUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] skillName;
    [SerializeField] private TextMeshProUGUI[] skillEffect;
    [SerializeField] private TextMeshProUGUI[] skillDescription;
    [SerializeField] private Image[] skillImage;

    private BaseSkill[] skills;

    private void Awake() 
    {
        gameObject.SetActive(false);
        LevelSystem.Instance.OnSkillsOverlay += LevelSystem_OnSkillsOverlay;
    }

    private void LevelSystem_OnSkillsOverlay (object sender, BaseSkill[] skills)
    {
        gameObject.SetActive(true);
        this.skills = skills;
        for (int i = 0; i < skills.Length; i++)
        {
            skillName[i].text = skills[i].Name;
            skillEffect[i].text = skills[i].Effect;
            skillDescription[i].text = skills[i].Description;
            skillImage[i].sprite = skills[i].NewSprite;
        }
    }

    public void OnSkillButton1Clicked()
    {        
        LevelSystem.Instance.ChoosedSkill(skills[0]);
        gameObject.SetActive(false);
    }
    
    public void OnSkillButton2Clicked()
    {
        LevelSystem.Instance.ChoosedSkill(skills[1]);
        gameObject.SetActive(false);
    }

}
