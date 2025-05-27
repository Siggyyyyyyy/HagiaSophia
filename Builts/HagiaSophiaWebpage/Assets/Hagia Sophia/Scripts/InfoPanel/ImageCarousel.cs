using UnityEngine;
using UnityEngine.UI;

public class ImageCarousel : MonoBehaviour
{
    public Image displayImage;           // Das UI-Image, in dem das aktuelle Bild angezeigt wird
    public Sprite[] imageOptions;        // Die Bildliste, durch die rotiert werden soll
    

    private int currentIndex = 0;

    private void Start()
    {
        if (imageOptions.Length == 0 || displayImage == null)
        {
            Debug.LogError("ImageCarousel: Bildoptionen oder Display-Image nicht zugewiesen.");
            return;
        }



        UpdateImage();
    }

    public void PreviousImage()
    {
        currentIndex = (currentIndex - 1 + imageOptions.Length) % imageOptions.Length;
        UpdateImage();
    }

    public void NextImage()
    {
        currentIndex = (currentIndex + 1) % imageOptions.Length;
        UpdateImage();
    }

    private void UpdateImage()
    {
        displayImage.sprite = imageOptions[currentIndex];
    }
}
