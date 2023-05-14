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
        LevelSystem.Instance.OnPlayerDeath += LevelSystem_OnPlayerDeath;
    }

    public void LevelSystem_OnPlayerMaxHealthChanged (object sender, float newTotalNumberOfHearts)
    {
        for (int i = 0; i < totalNumberOfHearts; i++)
        {
            heartArray[i].GetComponent<Image>().sprite = fullHeart;
            Debug.Log("Replenished Heart "+i);

        }
        if (totalNumberOfHearts > newTotalNumberOfHearts) return;
        Debug.Log(newTotalNumberOfHearts +" should Instantiate" + ", from current "+ totalNumberOfHearts);
        for (int i = totalNumberOfHearts; i < newTotalNumberOfHearts; i++)
        {
            heartArray.Add(Instantiate(heartPrefab, imageContainer));
            heartArray[i].GetComponent<Image>().sprite = fullHeart;
            Debug.Log("Created heart "+i);
        }
        totalNumberOfHearts = (int) newTotalNumberOfHearts;
    }
    public void LevelSystem_OnPlayerDamaged  (object sender, float newNumberOfFullHearts)
    {
        for (int i = totalNumberOfHearts-1; i >= newNumberOfFullHearts; i--)
        {
            heartArray[i].GetComponent<Image>().sprite = emptyHeart;
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
