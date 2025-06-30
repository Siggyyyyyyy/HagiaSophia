using UnityEngine;
using System.Collections;


public class ObjectDragRotator : MonoBehaviour
{
    public float rotationSpeed = 0.2f;
    public float inertiaDamping = 2f;

    private bool isDragging = false;
    private Vector3 lastMousePosition;
    private Vector2 currentVelocity;

    private bool readyForInput = false;

    void OnEnable()
    {
        ResetRotationInput(); // Jedes Mal beim Aktivieren zurücksetzen
    }

    public void ResetRotationInput()
    {
        isDragging = false;
        currentVelocity = Vector2.zero;
        readyForInput = false;
        StartCoroutine(EnableInputNextFrame());
    }

    private IEnumerator EnableInputNextFrame()
    {
        yield return null; // 1 Frame warten
        readyForInput = true;
    }

    void Update()
    {
        if (!readyForInput)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            lastMousePosition = Input.mousePosition;
            currentVelocity = Vector2.zero;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        if (isDragging)
        {
            Vector3 delta = Input.mousePosition - lastMousePosition;
            currentVelocity = new Vector2(-delta.x, delta.y);
            ApplyRotation(currentVelocity);
            lastMousePosition = Input.mousePosition;
        }
        else if (currentVelocity.magnitude > 0.01f)
        {
            ApplyRotation(currentVelocity);
            currentVelocity = Vector2.Lerp(currentVelocity, Vector2.zero, Time.deltaTime * inertiaDamping);
        }
    }

    void ApplyRotation(Vector2 velocity)
    {
        transform.Rotate(Camera.main.transform.up, velocity.x * rotationSpeed, Space.World);
        transform.Rotate(Camera.main.transform.right, velocity.y * rotationSpeed, Space.World);
    }
}
