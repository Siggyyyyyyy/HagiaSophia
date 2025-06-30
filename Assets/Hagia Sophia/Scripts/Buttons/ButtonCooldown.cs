using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Button))]
public class ButtonCooldown : MonoBehaviour
{
    public float cooldownTime = 1.0f;

    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClickWithCooldown);
    }

    private void OnClickWithCooldown()
    {
        if (!button.interactable) return;

        StartCoroutine(DisableTemporarily());
    }

    private IEnumerator DisableTemporarily()
    {
        button.interactable = false;
        yield return new WaitForSeconds(cooldownTime);
        button.interactable = true;
    }
}
