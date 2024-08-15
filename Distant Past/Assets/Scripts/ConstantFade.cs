using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ConstantFade : MonoBehaviour
{
    Image imageToFade;
    [SerializeField] float fadeDuration = 2.0f;
    [SerializeField] float initialAlpha;

    void Start()
    {
        imageToFade = GetComponent<Image>();
        // Get the initial alpha value of the image
    }

    void Update()
    {
        // Check if the alpha is greater than 0
        if (imageToFade.color.a > 0)
        {
            // Calculate the amount to fade per second
            float alphaDecreaseRate = initialAlpha / fadeDuration;

            // Decrease the alpha value based on the rate and the time elapsed since the last frame
            Color color = imageToFade.color;
            color.a -= alphaDecreaseRate * Time.deltaTime;

            // Ensure alpha doesn't go below 0
            if (color.a < 0)
            {
                color.a = 0;
            }

            // Apply the new color to the image
            imageToFade.color = color;
        }
    }
}
