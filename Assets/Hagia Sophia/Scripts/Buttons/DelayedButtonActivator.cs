using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DelayedButtonActivator : MonoBehaviour
{
    public Button targetButton;            // Der Button, der verzögert aktiv sein soll
    public float delayInSeconds = 0.5f;    // Verzögerung, z. B. 0.5 Sekunden

    private void OnEnable()
    {
        if (targetButton != null)
        {
            targetButton.interactable = false; // Sofort deaktivieren
            StartCoroutine(EnableButtonAfterDelay());
        }
    }

    private IEnumerator EnableButtonAfterDelay()
    {
        yield return new WaitForSeconds(delayInSeconds);
        targetButton.interactable = true;
    }
}
