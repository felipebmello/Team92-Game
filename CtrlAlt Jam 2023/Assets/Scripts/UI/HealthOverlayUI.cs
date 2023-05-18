using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthOverlayUI : MonoBehaviour
{
    [SerializeField] private int totalNumberOfHearts = 0;
    [SerializeField] private Transform heartPrefab;
    [SerializeField] private List<Transform> heartArray = new List<Transform>();
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;
    [SerializeField] private Transform imageContainer;


    private void Awake() 
    {
        LevelSystem.Instance.OnPlayerMaxHealthChanged += LevelSystem_OnPlayerMaxHealthChanged;
        LevelSystem.Instance.OnPlayerDamaged += LevelSystem_OnPlayerDamaged;
        LevelSystem.Instance.OnPlayerHealed += LevelSystem_OnPlayerHealed;
        LevelSystem.Instance.OnPlayerDeath += LevelSystem_OnPlayerDeath;
    }

    public void LevelSystem_OnPlayerMaxHealthChanged (object sender, float newTotalNumberOfHearts)
    {
        Debug.Log("Received "+ newTotalNumberOfHearts +" as total health of player.");
        for (int i = 0; i < totalNumberOfHearts; i++)
        {
            Debug.Log("Replenished Heart "+(i+1)+" from "+newTotalNumberOfHearts+" total");
            heartArray[i].GetComponent<Image>().sprite = fullHeart;

        }
        if (totalNumberOfHearts > newTotalNumberOfHearts) 
        {
            Debug.Log("Life is already full.");
            return;
        }
        for (int i = totalNumberOfHearts; i < newTotalNumberOfHearts; i++)
        {
            Debug.Log("Added "+(i+1)+" to "+newTotalNumberOfHearts+" total");
            heartArray.Add(Instantiate(heartPrefab, imageContainer));
            heartArray[i].GetComponent<Image>().sprite = fullHeart;
            //Debug.Log("Created heart "+i);
        }
        totalNumberOfHearts = (int) newTotalNumberOfHearts;
    }
    public void LevelSystem_OnPlayerDamaged  (object sender, float newNumberOfFullHearts)
    {
        for (int i = totalNumberOfHearts-1; i >= newNumberOfFullHearts; i--)
        {
            Debug.Log("Reduced Heart "+(i+1)+" from "+totalNumberOfHearts+" as player took damage.");
            heartArray[i].GetComponent<Image>().sprite = emptyHeart;
        }
    }

    public void LevelSystem_OnPlayerHealed  (object sender, float newNumberOfFullHearts)
    {
        for (int i = 0; i < newNumberOfFullHearts; i++)
        {
            Debug.Log("Replenished Heart "+(i+1)+" from "+totalNumberOfHearts+" as player healed to a total of "+ newNumberOfFullHearts +".");
            heartArray[i].GetComponent<Image>().sprite = fullHeart;
        }
    }

    public void LevelSystem_OnPlayerDeath  (object sender, EventArgs e)
    {
        for (int i = 0; i < totalNumberOfHearts; i++)
        {
            heartArray[i].GetComponent<Image>().sprite = emptyHeart;
        }
    }
}
