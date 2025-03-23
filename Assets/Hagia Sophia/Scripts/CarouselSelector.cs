using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CarouselSelector : MonoBehaviour
{
    public GameObject[] carouselObjects; 
    public Button leftButton;
    public Button rightButton;
    public Button selectButton;
    public Button backButton;

    private int currentIndex = 0;
    private bool isObjectSelected = false;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private float animationDuration = 0.5f;

    private void Start()
    {
        if (carouselObjects.Length == 0)
        {
            Debug.LogError("Fehler: Keine Objekte im Karussell definiert!");
            return;
        }

        leftButton.onClick.AddListener(RotateLeft);
        rightButton.onClick.AddListener(RotateRight);
        selectButton.onClick.AddListener(SelectObject);
        backButton.onClick.AddListener(BackToCarousel);

        backButton.gameObject.SetActive(false);

        UpdateSelection();
    }

    private void RotateLeft()
    {
        if (isObjectSelected) return;
        currentIndex = (currentIndex + 1) % carouselObjects.Length;
        UpdateSelection();
    }

    private void RotateRight()
    {
        if (isObjectSelected) return;
        currentIndex = (currentIndex - 1 + carouselObjects.Length) % carouselObjects.Length;
        UpdateSelection();
    }

    private void UpdateSelection()
    {
        foreach (GameObject obj in carouselObjects)
        {
            Renderer objRenderer = obj.GetComponent<Renderer>();
            if (objRenderer != null)
            {
                objRenderer.material.color = Color.white;
                obj.SetActive(true);
            }
        }

        Renderer selectedRenderer = carouselObjects[currentIndex].GetComponent<Renderer>();
        if (selectedRenderer != null)
        {
            selectedRenderer.material.color = Color.green;
        }
    }

    private void SelectObject()
    {
        if (isObjectSelected) return;

        isObjectSelected = true;
        GameObject selectedObject = carouselObjects[currentIndex];

        originalPosition = selectedObject.transform.position;
        originalRotation = selectedObject.transform.rotation;

        DisableSpin(selectedObject);

        StartCoroutine(MoveAndRotateObject(selectedObject));

        leftButton.gameObject.SetActive(false);
        rightButton.gameObject.SetActive(false);
        selectButton.gameObject.SetActive(false);
        backButton.gameObject.SetActive(true);

        foreach (GameObject obj in carouselObjects)
        {
            if (obj != selectedObject)
            {
                obj.SetActive(false);
            }
        }
    }

    private IEnumerator MoveAndRotateObject(GameObject obj)
    {
        float elapsed = 0f;
        Vector3 startPos = obj.transform.position;
        Vector3 targetPos = startPos + Vector3.up * 2f;
        Quaternion startRot = obj.transform.rotation;
        Quaternion targetRot = Quaternion.Euler(0, 90, 0);

        while (elapsed < animationDuration)
        {
            obj.transform.position = Vector3.Lerp(startPos, targetPos, elapsed / animationDuration);
            obj.transform.rotation = Quaternion.Slerp(startRot, targetRot, elapsed / animationDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        obj.transform.position = targetPos;
        obj.transform.rotation = targetRot;
    }

    private void BackToCarousel()
    {
        if (!isObjectSelected) return;

        isObjectSelected = false;
        GameObject selectedObject = carouselObjects[currentIndex];

        StartCoroutine(MoveAndResetRotation(selectedObject));

        leftButton.gameObject.SetActive(true);
        rightButton.gameObject.SetActive(true);
        selectButton.gameObject.SetActive(true);
        backButton.gameObject.SetActive(false);

        foreach (GameObject obj in carouselObjects)
        {
            obj.SetActive(true);
        }
    }

    private IEnumerator MoveAndResetRotation(GameObject obj)
    {
        float elapsed = 0f;
        Vector3 startPos = obj.transform.position;
        Vector3 targetPos = originalPosition;
        Quaternion startRot = obj.transform.rotation;
        Quaternion targetRot = originalRotation;

        while (elapsed < animationDuration)
        {
            obj.transform.position = Vector3.Lerp(startPos, targetPos, elapsed / animationDuration);
            obj.transform.rotation = Quaternion.Slerp(startRot, targetRot, elapsed / animationDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        obj.transform.position = targetPos;
        obj.transform.rotation = targetRot;

        // Warten auf Abschluss der Rücksetz-Animation, bevor das Spin-Skript wieder aktiviert wird
        yield return new WaitForSeconds(0.1f); 

        EnableSpin(obj);
    }

    private void DisableSpin(GameObject obj)
    {
        MonoBehaviour spinScript = obj.GetComponent<MonoBehaviour>();
        if (spinScript != null)
        {
            spinScript.enabled = false;
        }
    }

    private void EnableSpin(GameObject obj)
    {
        MonoBehaviour spinScript = obj.GetComponent<MonoBehaviour>();
        if (spinScript != null)
        {
            spinScript.enabled = true;
        }
    }
}
