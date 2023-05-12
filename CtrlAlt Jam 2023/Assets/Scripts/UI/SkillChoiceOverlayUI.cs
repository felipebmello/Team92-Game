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
    [SerializeField] private Outline[] skillOutline;

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
        int numberOfSkillStates = System.Enum.GetValues(typeof(SkillsetController.KarmaState)).Length / 2;
        for (int i = 0; i < skills.Length; i++)
        {
            skillName[i].text = skills[i].Name;
            skillEffect[i].text = skills[i].Effect;
            skillDescription[i].text = skills[i].Description;
            skillImage[i].sprite = skills[i].NewSprite;
            if ((int) skills[i].State >=  numberOfSkillStates) skillOutline[i].effectColor = Color.blue;
            else skillOutline[i].effectColor = Color.red;
        }
    }

    public void OnSkillButton1Clicked()
    {        
        gameObject.SetActive(false);
        LevelSystem.Instance.ChoosedSkill(skills[0]);
        LevelSystem.Instance.SkillNotChosen(skills[1]);
    }
    
    public void OnSkillButton2Clicked()
    {
        gameObject.SetActive(false);
        LevelSystem.Instance.SkillNotChosen(skills[0]);
        LevelSystem.Instance.ChoosedSkill(skills[1]);
    }

}
