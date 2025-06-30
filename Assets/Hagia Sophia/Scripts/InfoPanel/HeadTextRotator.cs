using UnityEngine;
using TMPro; 

public class HeadTextRotator : MonoBehaviour
{
    public TMP_Text infoText;     
    public string[] texts;         
    private int currentIndex = 0;  
    private void Start()
    {
        if (texts.Length == 0)
        {
            Debug.LogError("Fehler: Es wurden keine Texte zugewiesen!");
            return;
        }

        UpdateText(); 
    }

    public void NextText()
    {
        currentIndex = (currentIndex + 1) % texts.Length;
        UpdateText();
    }

    public void PreviousText()
    {
        currentIndex = (currentIndex - 1 + texts.Length) % texts.Length; 
        UpdateText();
    }

    private void UpdateText()
    {
        infoText.text = texts[currentIndex];
    }
}
