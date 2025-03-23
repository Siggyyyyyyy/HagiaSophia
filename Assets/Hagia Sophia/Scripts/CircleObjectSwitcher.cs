using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleObjectSwitcher : MonoBehaviour
{
    public GameObject[] objects; // Die Objekte, die im Kreis angeordnet werden
    public float radius = 5f; // Der Radius des Kreises
    public float rotationDuration = 0.5f; // Dauer der Drehung zwischen den Objekten

    private bool isRotating = false; // Ob eine Animation gerade läuft
    private float angleStep; // Schrittweite des Winkels zwischen Objekten

    void Start()
    {
        angleStep = 360f / objects.Length;
        ArrangeObjectsInCircle();
    }

    void ArrangeObjectsInCircle()
    {
        for (int i = 0; i < objects.Length; i++)
        {
            float angle = Mathf.Deg2Rad * (i * angleStep);
            Vector3 position = new Vector3(Mathf.Cos(angle) * radius, 7, Mathf.Sin(angle) * radius); // Höhe auf 7 gesetzt
            objects[i].transform.position = position;
        }
    }

    public void RotateLeft()
    {
        if (!isRotating)
        {
            StartCoroutine(RotateObjects(-angleStep));
        }
    }

    public void RotateRight()
    {
        if (!isRotating)
        {
            StartCoroutine(RotateObjects(angleStep));
        }
    }

    IEnumerator RotateObjects(float rotationAngle)
    {
        isRotating = true;

        float elapsedTime = 0f;
        float currentAngle = 0f;

        while (elapsedTime < rotationDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.SmoothStep(0, 1, elapsedTime / rotationDuration); // EaseInOut-Bewegung
            float stepAngle = Mathf.Lerp(0, rotationAngle, t) - currentAngle;
            currentAngle += stepAngle;

            foreach (GameObject obj in objects)
            {
                obj.transform.RotateAround(Vector3.zero, Vector3.up, stepAngle);
            }

            yield return null;
        }

        isRotating = false;
    }
}
