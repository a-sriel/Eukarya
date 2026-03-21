using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI; // Required for accessing UI Image components

public class EvolutionUIManager : MonoBehaviour
{
    // List to hold the 5 Image components on your UI
    public List<Image> uiImages;

    // Drag your replacement sprites here in the Inspector
    public Sprite filledSprite;
    public Sprite consumedSprite;

    private int currentImageIndex = 0;

    // Call this method from your player consumption script
    public void ConsumeItem()
    {
        if (currentImageIndex < uiImages.Count)
        {
            // Swap the image to the consumed sprite
            uiImages[currentImageIndex].sprite = consumedSprite;

            currentImageIndex++; // Increment to the next image
        }
    }

    // a function to reset the images to default
    public void ResetImages()
    {
        
    }
}
