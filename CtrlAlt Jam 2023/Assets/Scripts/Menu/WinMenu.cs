using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class WinMenu : MonoBehaviour
{

    [SerializeField] private Transform overlayUI;

    private void Awake() {
        overlayUI.gameObject.SetActive(false);
    }
    public void ShowOverlay()
    {
        overlayUI.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
