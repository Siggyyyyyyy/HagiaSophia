using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BackButtonDelay : MonoBehaviour
{
    public Button targetButton;         
    public float cooldownTime = 0.5f;  

    void OnEnable()
    {
        if (targetButton != null)
            StartCoroutine(DisableTemporarily());
    }

    private IEnumerator DisableTemporarily()
    {
        targetButton.interactable = false; 
        yield return new WaitForSeconds(cooldownTime);
        targetButton.interactable = true;   
    }
}
