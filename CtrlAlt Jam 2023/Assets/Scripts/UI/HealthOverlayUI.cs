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
    [SerializeField] private Sprite halfHeart;
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

        //Recebe o novo total de corações
        //Debug.Log("Received "+ newTotalNumberOfHearts +" as total health of player.");

        //Atualiza os corações do Player de 0 ao total anterior como cheios
        for (int i = 0; i < totalNumberOfHearts; i++)
        {
            //Debug.Log("Replenished Heart "+(i+1)+" from "+newTotalNumberOfHearts+" total");
            heartArray[i].GetComponent<Image>().sprite = fullHeart;

        }
        if (totalNumberOfHearts > newTotalNumberOfHearts) 
        {
            //Debug.Log("Life is already full.");
            return;
        }

        //Cria novos corações
        for (int i = totalNumberOfHearts; i < Mathf.FloorToInt(newTotalNumberOfHearts); i++)
        {
            //Debug.Log("Added "+(i+1)+" to "+newTotalNumberOfHearts+" total");
            heartArray.Add(Instantiate(heartPrefab, imageContainer));
            heartArray[i].GetComponent<Image>().sprite = fullHeart;
            //Debug.Log("Created heart "+i);
        }

        //Verifica a existência de corações meio cheios
        if (newTotalNumberOfHearts % 1 != 0)
        {
            //Debug.Log("Added "+(totalNumberOfHearts+1)+" to "+newTotalNumberOfHearts+" total");
            heartArray.Add(Instantiate(heartPrefab, imageContainer));
            heartArray[totalNumberOfHearts+1].GetComponent<Image>().sprite = halfHeart;
            //Debug.Log("Created half heart "+newTotalNumberOfHearts);
        }

        //Atualiza o total de corações
        totalNumberOfHearts = Mathf.CeilToInt(newTotalNumberOfHearts);
    }
    public void LevelSystem_OnPlayerDamaged  (object sender, float newNumberOfFullHearts)
    {
        //Apaga os corações reduzidos
        for (int i = totalNumberOfHearts-1; i >= newNumberOfFullHearts; i--)
        {
            //Debug.Log("Reduced Full Heart "+(i+1)+" from "+totalNumberOfHearts+" as player took damage. "+newNumberOfFullHearts);
            heartArray[i].GetComponent<Image>().sprite = emptyHeart;
        }
        //Verifica a existência de corações meio cheios
        if (newNumberOfFullHearts % 1 != 0)
        {
            //Debug.Log("Reduced Half Heart "+Mathf.FloorToInt(newNumberOfFullHearts)+" from "+totalNumberOfHearts+" as player took damage. "+newNumberOfFullHearts);
            heartArray[Mathf.FloorToInt(newNumberOfFullHearts)].GetComponent<Image>().sprite = halfHeart;
        }

    }

    public void LevelSystem_OnPlayerHealed  (object sender, float newNumberOfFullHearts)
    {
        //Recupera os corações regenerados
        for (int i = 0; i < newNumberOfFullHearts; i++)
        {
            //Debug.Log("Replenished Full Heart "+(i+1)+" from "+totalNumberOfHearts+" as player healed to a total of "+ newNumberOfFullHearts +".");
            heartArray[i].GetComponent<Image>().sprite = fullHeart;
        }
        //Verifica a existência de corações meio cheios
        if (newNumberOfFullHearts % 1 != 0)
        {
            //Debug.Log("Replenished Half Heart "+Mathf.FloorToInt(newNumberOfFullHearts)+" from "+totalNumberOfHearts+" as player healed to a total of "+ newNumberOfFullHearts +".");
            heartArray[Mathf.FloorToInt(newNumberOfFullHearts)].GetComponent<Image>().sprite = halfHeart;
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
