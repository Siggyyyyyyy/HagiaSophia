using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DelayedButtonActivator : MonoBehaviour
{
    public Button targetButton;           
    public float delayInSeconds = 0.5f;    

    private void OnEnable()
    {
        if (targetButton != null)
        {
            targetButton.interactable = false; // sofort deaktivieren
            StartCoroutine(EnableButtonAfterDelay());
        }
    }

    private IEnumerator EnableButtonAfterDelay()
    {
        yield return new WaitForSeconds(delayInSeconds);
        targetButton.interactable = true;
    }
}
