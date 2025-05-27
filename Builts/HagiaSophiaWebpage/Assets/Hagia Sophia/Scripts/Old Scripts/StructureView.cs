using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeTeleport : MonoBehaviour
{
    public KeyCode toggleKey = KeyCode.R; // Taste zum Auslösen der Bewegung (z. B. 'R')
    private Vector3 targetPosition = new Vector3(-7.44f, 4.06f, -17.5f); // Zielposition
    private Quaternion targetRotation = Quaternion.Euler(0f, 0f, 0f); // Zielrotation
    private Vector3 initialPosition; // Startposition des Cubes
    private Quaternion initialRotation; // Startrotation des Cubes
    private bool isAtTarget = false; // Ob der Cube bereits an der Zielposition ist
    private bool isMoving = false; // Kontrolliert, ob der Cube sich gerade bewegt

    public float moveSpeed = 20f; // Erhöhte Geschwindigkeit der Bewegung
    
    public Orbitview orbitviewScript; // Referenz auf das Orbitview-Skript
    public CircleMovement circleMovementScript; // Referenz auf das CircleMovement-Skript der Kamera
    public Spin spinScript; // Referenz auf das Spin-Skript des Cubes

    void Start()
    {
        // Speichern der ursprünglichen Position und Rotation
        initialPosition = transform.position;
        initialRotation = transform.rotation;

        // Sicherstellen, dass das Orbitview-Skript existiert
        if (orbitviewScript == null)
        {
            orbitviewScript = GetComponent<Orbitview>();
        }

        // Sicherstellen, dass das CircleMovement-Skript existiert
        if (circleMovementScript == null)
        {
            circleMovementScript = Camera.main.GetComponent<CircleMovement>(); // Suche das Skript an der Kamera
        }

        // Sicherstellen, dass das Spin-Skript existiert
        if (spinScript == null)
        {
            spinScript = GetComponent<Spin>(); // Suche das Spin-Skript auf dem Cube
        }
    }

    void Update()
    {
        // Überprüfen, ob die festgelegte Taste gedrückt wurde und der Cube sich nicht schon bewegt
        if (Input.GetKeyDown(toggleKey) && !isMoving)
        {
            isMoving = true;
            StartCoroutine(MoveCube()); // Startet die Coroutine, die den Cube bewegt
        }
    }

    // Coroutine für das sanfte Bewegen des Cubes
    IEnumerator MoveCube()
    {
        Vector3 startPosition = transform.position;
        Quaternion startRotation = transform.rotation;
        Vector3 endPosition = isAtTarget ? initialPosition : targetPosition;
        Quaternion endRotation = isAtTarget ? initialRotation : targetRotation;

        // Setze eine konstante Geschwindigkeit der Bewegung
        float journeyLength = Vector3.Distance(startPosition, endPosition);
        float startTime = Time.time;

        // Umschalten des Status
        isAtTarget = !isAtTarget;

        // Orbitview-Skript deaktivieren oder aktivieren
        if (orbitviewScript != null)
        {
            orbitviewScript.enabled = !orbitviewScript.enabled; // Wechselt den Status
        }
/*
        // CircleMovement-Skript deaktivieren oder aktivieren
        if (circleMovementScript != null)
        {
            // Wenn der Cube gerade teleportiert wurde, deaktivieren wir CircleMovement nach der Teleportation
            if (isAtTarget)
            {
                circleMovementScript.enabled = false; // Deaktiviert das CircleMovement-Skript
            }
            else
            {
                circleMovementScript.enabled = true; // Aktiviert das CircleMovement-Skript
            }
        }
*/
        // Spin-Skript deaktivieren oder aktivieren
        if (spinScript != null)
        {
            // Wenn der Cube an die Zielposition teleportiert wurde, deaktivieren wir das Spin-Skript
            if (isAtTarget)
            {
                spinScript.enabled = false; // Deaktiviert das Spin-Skript
            }
            else
            {
                spinScript.enabled = true; // Aktiviert das Spin-Skript
            }
        }

        // Bewegung des Cubes zu der neuen Position mit einer schnelleren Geschwindigkeit
        while (Vector3.Distance(transform.position, endPosition) > 0.1f)
        {
            float distanceCovered = (Time.time - startTime) * moveSpeed; // Berechnet die zurückgelegte Strecke basierend auf der Zeit und Geschwindigkeit
            float fractionOfJourney = distanceCovered / journeyLength; // Ermittelt, wie weit der Cube im Verhältnis zum gesamten Weg gekommen ist

            transform.position = Vector3.Lerp(startPosition, endPosition, fractionOfJourney);
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, fractionOfJourney);

            yield return null; // Warten bis zum nächsten Frame
        }

        // Sicherstellen, dass der Cube exakt die Zielposition und -rotation erreicht
        transform.position = endPosition;
        transform.rotation = endRotation;

        isMoving = false; // Beende die Bewegung
    }
}
