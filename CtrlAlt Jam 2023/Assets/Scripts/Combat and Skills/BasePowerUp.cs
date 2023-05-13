using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePowerUp : BaseCollectable
{
    private float yOffset = 0; // Vertical offset for the bounce
    private float bounceHeight = 0.2f; // Maximum height of the bounce
    private float bounceSpeed = 3; // Speed of the bouncing motion

    private Vector3 originalPosition;

    private void Start()
    {
        originalPosition = transform.position;
    }

    private void Update()
    {
        // Calculate the vertical offset based on a sine wave
        float offsetY = Mathf.Sin(Time.time * bounceSpeed) * bounceHeight;

        // Set the position of the object using the original position and the offset
        transform.position = originalPosition + new Vector3(0f, yOffset + offsetY, 0f);
    }
}
