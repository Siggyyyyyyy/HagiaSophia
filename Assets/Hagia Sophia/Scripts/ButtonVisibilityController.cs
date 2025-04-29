using UnityEngine;

public class ButtonVisibilityController : MonoBehaviour
{
    public GameObject leftArrowButton;
    public GameObject rightArrowButton;
    public GameObject selectButton;
    public GameObject backButton;

    public GameObject infoButton;
    public GameObject fundstelleButton;
    public GameObject gebäudeButton;
    public GameObject detailButton;

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
        infoButton.SetActive(false);
        fundstelleButton.SetActive(false);
        gebäudeButton.SetActive(false);
        detailButton.SetActive(false);

    }

    private void ShowDetailMode()
    {
        isInDetailMode = true;

        leftArrowButton.SetActive(false);
        rightArrowButton.SetActive(false);
        selectButton.SetActive(false);

        backButton.SetActive(true);
        infoButton.SetActive(true);
        fundstelleButton.SetActive(true);
        gebäudeButton.SetActive(true);
        detailButton.SetActive(true);

    }
}
