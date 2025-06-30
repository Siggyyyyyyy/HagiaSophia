using UnityEngine;

public class ButtonVisibilityController : MonoBehaviour
{
    public GameObject leftArrowButton;
    public GameObject rightArrowButton;
    public GameObject selectButton;
    public GameObject backButton;

    // public GameObject infoButton; // vorübergehend deaktiviert
    public GameObject gebäudeButton;

    private bool isInDetailMode = false;

    private void Start()
    {
        ShowNavigationMode();
    }

    public void OnSelectButtonPressed()
    {
        ShowDetailMode();
    }

    public void OnBackButtonPressed()
    {
        ShowNavigationMode();
    }

    private void ShowNavigationMode()
    {
        isInDetailMode = false;

        leftArrowButton.SetActive(true);
        rightArrowButton.SetActive(true);
        selectButton.SetActive(true);

        backButton.SetActive(false);
        // infoButton.SetActive(false); // vorübergehend deaktiviert
        gebäudeButton.SetActive(false);
    }

    private void ShowDetailMode()
    {
        isInDetailMode = true;

        leftArrowButton.SetActive(false);
        rightArrowButton.SetActive(false);
        selectButton.SetActive(false);

        backButton.SetActive(true);
        // infoButton.SetActive(true); // vorübergehend deaktiviert
        gebäudeButton.SetActive(true);
    }
}
