using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public KeyCode teleportKey = KeyCode.R; // Die Taste, die das Teleportieren auslöst (z. B. 'R')
    public Vector3 targetPosition = new Vector3(-2.155063f, 7.55f, 10.60972f); // Zielposition für die Kamera
    private Vector3 initialPosition; // Startposition der Kamera
    private bool isMoving = false; // Kontrolliert, ob die Kamera sich gerade bewegt

    public float moveSpeed = 5f; // Geschwindigkeit der Bewegung

    void Start()
    {
        initialPosition = transform.position; // Speichern der Startposition
    }

    void Update()
    {
        /*/ Überprüfen, ob die Taste gedrückt wurde und ob die Kamera sich noch nicht bewegt
        if (Input.GetKeyDown(teleportKey) && !isMoving)
        {
            isMoving = true;
            StartCoroutine(MoveCameraToTarget()); // Startet die Coroutine, die die Kamera bewegt
        }*/
    }

    // Coroutine für das sanfte Bewegen der Kamera
    IEnumerator MoveCameraToTarget()
    {
        Vector3 startPosition = transform.position;
        float journeyLength = Vector3.Distance(startPosition, targetPosition);
        float startTime = Time.time;

        // Bewegung der Kamera zum Ziel mit Lerp
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            float distanceCovered = (Time.time - startTime) * moveSpeed; // Berechnung der zurückgelegten Strecke
            float fractionOfJourney = distanceCovered / journeyLength; // Berechnung des Fortschritts

            transform.position = Vector3.Lerp(startPosition, targetPosition, fractionOfJourney); // Bewegt die Kamera
            yield return null; // Warten bis zum nächsten Frame
        }

        // Sicherstellen, dass die Kamera exakt an der Zielposition angekommen ist
        transform.position = targetPosition;

        isMoving = false; // Beende die Bewegung
    }
}