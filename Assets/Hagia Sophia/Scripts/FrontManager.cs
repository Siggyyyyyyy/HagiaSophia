using UnityEngine;

public class FrontManager : MonoBehaviour
{
     public GameObject[] previewVariants;        // Die Vorschau im Hintergrund
    public GameObject[] buildingFrontVariants;  // Die echten Varianten im Zielobjekt
    private int currentIndex = 0;

    private void Start()
    {
        UpdatePreview();
    }

    public void ShowNext()
    {
        currentIndex = (currentIndex + 1) % previewVariants.Length;
        UpdatePreview();
    }

    public void ShowPrevious()
    {
        currentIndex = (currentIndex - 1 + previewVariants.Length) % previewVariants.Length;
        UpdatePreview();
    }

    private void UpdatePreview()
    {
        for (int i = 0; i < previewVariants.Length; i++)
        {
            previewVariants[i].SetActive(i == currentIndex);
        }
    }

    public void ConfirmSelection()
    {
        for (int i = 0; i < buildingFrontVariants.Length; i++)
        {
            buildingFrontVariants[i].SetActive(i == currentIndex);
        }
    }
}