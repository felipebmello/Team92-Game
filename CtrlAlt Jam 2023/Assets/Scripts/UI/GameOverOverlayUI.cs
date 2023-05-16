using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverOverlayUI : MonoBehaviour
{
    
    private void Awake() 
    {
        gameObject.SetActive(false);
        LevelSystem.Instance.OnPlayerDeath += LevelSystem_OnPlayerDeath;
    }
    
    private void LevelSystem_OnPlayerDeath (object sender, EventArgs e)
    {
        gameObject.SetActive(true);
        Cursor.visible = true;
    }

    public void OnRestartButtonClick()
    {        
        LevelSystem.Instance.RestartScene();
        Cursor.visible = false;
    }
}
