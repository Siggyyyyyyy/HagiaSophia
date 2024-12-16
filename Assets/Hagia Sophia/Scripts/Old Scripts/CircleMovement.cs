using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleMovement : MonoBehaviour
{
    public Transform cube; // Standardziel
    public Transform structureMiddle; // Alternatives Zielobjekt
    public float speed = 2f;
    private float angle = 0f;
    private bool isTargetStructure = false; // Zustand für Umschaltung

    public float cubeRadius = 10f; // Radius für Cube
    public float structureRadius = 17f; // Radius für Structuremiddle

    public Transform target;
    public float radius;

    void Start()
    {
        // Initiales Ziel und Radius setzen
        target = cube;
        radius = cubeRadius;
    }

    void Update()
    {
        // Position basierend auf dem Ziel und Radius berechnen
        float x = target.position.x + Mathf.Cos(angle) * radius;
        float z = target.position.z + Mathf.Sin(angle) * radius;
        transform.position = new Vector3(x, transform.position.y, z);

        // Winkel für die Rotation anpassen
        angle += speed * Time.deltaTime;

        // Umschalten bei Drücken der Taste R
        if (Input.GetKeyDown(KeyCode.R))
        {
            ToggleTarget();
        }
    }

    void ToggleTarget()
    {
        if (isTargetStructure)
        {
            // Zurück auf Cube und Radius auf Standard setzen
            target = cube;
            radius = cubeRadius;
        }
        else
        {
            // Ziel auf Structuremiddle setzen und Radius erhöhen
            target = structureMiddle;
            radius = structureRadius;
        }

        // Zustand umschalten
        isTargetStructure = !isTargetStructure;
    }
}
