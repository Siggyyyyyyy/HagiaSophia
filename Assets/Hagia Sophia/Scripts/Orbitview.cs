using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbitview : MonoBehaviour
{
    public Transform cameraTransform; // Referenz zur Kamera
    public KeyCode alignKey = KeyCode.Space; // Taste, die gedrückt werden soll (z. B. Leertaste)
    private bool spacePressed = false;
    private Spin spinScript; // Referenz zum Spin-Skript auf dem Cube
    private CircleMovement circleMovementScript; // Referenz zum CircleMovement-Skript auf der Kamera

    public float moveSpeed = 2f; // Geschwindigkeit, mit der der Cube sich bewegt
    public float rotationSpeed = 500f; // Erhöhte Geschwindigkeit für sensiblere Drehung

    private Vector3 initialPosition; // Startposition des Cubes
    private Quaternion initialRotation; // Startrotation des Cubes
    private float lastMouseX, lastMouseY; // Speichert die Mauspositionen vom letzten Frame

    void Start()
    {
        // Hole das Spin-Skript vom Cube
        spinScript = GetComponent<Spin>();

        // Hole das CircleMovement-Skript von der Kamera
        if (cameraTransform != null)
        {
            circleMovementScript = cameraTransform.GetComponent<CircleMovement>();
        }

        // Speichere die ursprüngliche Position und Rotation des Cubes
        initialPosition = transform.position;
        initialRotation = transform.rotation;

        // Stelle sicher, dass beide Skripte initial deaktiviert sind
        if (spinScript != null)
        {
            spinScript.enabled = false;
        }

        if (circleMovementScript != null)
        {
            circleMovementScript.enabled = true; // Standardmäßig eingeschaltet
        }
    }

    void Update()
    {
        // Überprüfe, ob die festgelegte Taste gedrückt wurde
        if (Input.GetKeyDown(alignKey))
        {
            spacePressed = !spacePressed; // Umschalten des spacePressed-Status

            // Aktiviere/Deaktiviere das Spin-Skript
            if (spinScript != null)
            {
                spinScript.enabled = spacePressed;
            }

            // De-/Aktiviere das CircleMovement-Skript auf der Kamera
            if (circleMovementScript != null)
            {
                circleMovementScript.enabled = !spacePressed;
            }
        }

        // Wenn die Leertaste gedrückt wurde und der Cube sich bewegen soll
        if (spacePressed)
        {
            // Interpoliere die Y-Position des Cubes von der aktuellen Position zur Y-Position der Kamera
            float newYPosition = Mathf.Lerp(transform.position.y, cameraTransform.position.y, Time.deltaTime * moveSpeed);
            transform.position = new Vector3(transform.position.x, newYPosition, transform.position.z);

            // Mausbewegung zum Drehen des Cubes um alle Achsen (X, Y, Z)
            if (Input.GetMouseButton(0)) // Wenn die linke Maustaste gedrückt wird
            {
                float mouseDeltaX = Input.mousePosition.x - lastMouseX; // Mausbewegung in der X-Achse
                float mouseDeltaY = Input.mousePosition.y - lastMouseY; // Mausbewegung in der Y-Achse

                // Berechne Rotationen basierend auf der Mausbewegung
                float rotationAmountX = mouseDeltaY * rotationSpeed * Time.deltaTime;
                float rotationAmountY = -mouseDeltaX * rotationSpeed * Time.deltaTime; // Vorzeichen umkehren

                // Drehe den Cube um alle Achsen basierend auf der Mausbewegung
                transform.Rotate(Vector3.right, -rotationAmountX, Space.World); // Rotation um X-Achse
                transform.Rotate(Vector3.up, rotationAmountY, Space.World);    // Rotation um Y-Achse
            }

            // Speichere die aktuelle Mausposition für den nächsten Frame
            lastMouseX = Input.mousePosition.x;
            lastMouseY = Input.mousePosition.y;
        }
        else
        {
            // Interpoliere die Y-Position des Cubes zurück zur ursprünglichen Position
            float newYPosition = Mathf.Lerp(transform.position.y, initialPosition.y, Time.deltaTime * moveSpeed);
            transform.position = new Vector3(transform.position.x, newYPosition, transform.position.z);

            // Stelle den Cube in seine ursprüngliche Rotation zurück
            transform.rotation = Quaternion.Lerp(transform.rotation, initialRotation, Time.deltaTime * moveSpeed);
        }
    }
}
