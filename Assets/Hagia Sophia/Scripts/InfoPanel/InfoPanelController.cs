using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InfoPanelController : MonoBehaviour
{
    public RectTransform infoPanel;  
    public Button infoButton;        
    public float slideDuration = 0.8f; 

    private bool isPanelOpen = false;
    private Vector2 hiddenPosition;
    private Vector2 visiblePosition;

    private void Start()
    {
        
        visiblePosition = infoPanel.anchoredPosition;

        
        hiddenPosition = visiblePosition + new Vector2(infoPanel.rect.width, 0);
        infoPanel.anchoredPosition = hiddenPosition;

        
        infoButton.onClick.AddListener(TogglePanel);
    }

    private void TogglePanel()
    {
        StopAllCoroutines();
        StartCoroutine(SlidePanel(isPanelOpen ? hiddenPosition : visiblePosition));
        isPanelOpen = !isPanelOpen;
    }

    public void ClosePanel()
    {
        if (isPanelOpen)
        {
            StopAllCoroutines();
            StartCoroutine(SlidePanel(hiddenPosition));
            isPanelOpen = false;
        }
    }

    private IEnumerator SlidePanel(Vector2 targetPosition)
    {
        float elapsed = 0f;
        Vector2 startPosition = infoPanel.anchoredPosition;

        while (elapsed < slideDuration)
        {
            infoPanel.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, elapsed / slideDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        infoPanel.anchoredPosition = targetPosition;
    }
}
