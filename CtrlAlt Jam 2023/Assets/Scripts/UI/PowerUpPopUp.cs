using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PowerUpPopUp : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    private GameObject player;
    [SerializeField] private float yOffset;
    [SerializeField] private float xOffset;
    private bool showing = false;
    private Vector3 popUpPosition;

    private void Start()
    {
        // Hide the pop-up initially
        gameObject.SetActive(false);
        player = GameObject.Find("Player");
    }
    private void Update()
    {
        if (showing)
        {
            transform.position = player.transform.position + new Vector3(xOffset, yOffset, 0f); ;
        }
    }

    public void ShowPowerUp(string powerUpName)
    {
        // Set the text to display the power-up name
        textMeshPro.text = powerUpName + " Up!";

        // Calculate the position to show the pop-up above the player
        popUpPosition = player.transform.position + new Vector3(0f, yOffset, 0f);
        transform.position = popUpPosition;

        // Show the pop-up
        gameObject.SetActive(true);
        showing = true;

        // Start a coroutine to hide the pop-up after a delay
        StartCoroutine(HidePopUp());
    }

    private IEnumerator HidePopUp()
    {
        // Wait for a certain duration
        yield return new WaitForSeconds(2f); // Adjust the duration as desired

        showing=false;

        // Hide the pop-up
        gameObject.SetActive(false);
    }
}
