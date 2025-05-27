using UnityEngine;
using TMPro; // Wichtig für TextMeshPro

public class InfoTextRotator : MonoBehaviour
{
    public TMP_Text infoText;      // TextMeshPro-Textobjekt
    public string[] texts;         // Liste mit allen möglichen Texten
    private int currentIndex = 0;  // Aktueller Index des Textes

    private void Start()
    {
        if (texts.Length == 0)
        {
            Debug.LogError("Fehler: Es wurden keine Texte zugewiesen!");
            return;
        }

        UpdateText(); // Starttext setzen
    }

    public void NextText()
    {
        currentIndex = (currentIndex + 1) % texts.Length; // Vorwärts rotieren
        UpdateText();
    }

    public void PreviousText()
    {
        currentIndex = (currentIndex - 1 + texts.Length) % texts.Length; // Rückwärts rotieren
        UpdateText();
    }

    private void UpdateText()
    {
        infoText.text = texts[currentIndex];
    }
}
