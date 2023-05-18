using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BasePowerUp : BaseCollectable
{
    [SerializeField] protected Transform popUpPrefab;
    [SerializeField] protected Canvas powerUpCanvas;
    protected Color powerUpColor;
    protected override void Start() 
    {
        base.Start();
        collectableCollider = GetComponent<CircleCollider2D>();
        powerUpColor = GetComponentInChildren<SpriteRenderer>().color;
    }
    
    protected override void PickUp(GameObject player)
    {
        powerUpCanvas.transform.position = this.transform.position;
        Instantiate(popUpPrefab, powerUpCanvas.transform.position, Quaternion.identity, powerUpCanvas.transform).gameObject.GetComponent<PowerUpPopUp>().ShowPowerUp(GetName(), powerUpColor);
    }

}
