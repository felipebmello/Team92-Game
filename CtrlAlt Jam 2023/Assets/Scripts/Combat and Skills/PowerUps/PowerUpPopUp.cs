using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PowerUpPopUp : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshPro;
    private bool showing = false;

    private void Start()
    {
        // Hide the pop-up initially
        gameObject.SetActive(true);
    }
    private void Update()
    {
        /*textMeshPro.fontSize -= textMeshPro.fontSize*Time.deltaTime;
        this.transform.position += new Vector3 (0f, this.transform.position.y*(Time.deltaTime/2f), 0f);*/
    }

    public void ShowPowerUp(string powerUpName, Color powerUpColor)
    {
        // Set the text to display the power-up name
        textMeshPro.text = "+ " + powerUpName;
        textMeshPro.color = powerUpColor;

        // Show the pop-up
        gameObject.SetActive(true);
        showing = true;

        // Start a coroutine to hide the pop-up after a delay
        StartCoroutine(HidePopUp());
    }

    private IEnumerator HidePopUp()
    {
        // Wait for a certain duration
        yield return new WaitForSeconds(1f); // Adjust the duration as desired

        Destroy(gameObject);
    }
}
