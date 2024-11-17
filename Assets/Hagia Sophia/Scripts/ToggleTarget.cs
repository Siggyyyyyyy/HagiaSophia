using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ToggleTarget : MonoBehaviour
{
    public KeyCode toggleKey = KeyCode.R; // Taste zum Umschalten
    public CircleMovement circleMovementScript; // Referenz zum CircleMovement-Skript
    public LookAt lookAtScript; // Referenz zum LookAt-Skript

    public Transform structureMiddle; // Zielobjekt "Structuremiddle"
    public Transform cube; // Zielobjekt "Cube"

    private bool isTargetStructure = false; // Zustand für Umschaltung


void Start()
{
    if (circleMovementScript == null)
    {
        circleMovementScript = GetComponent<CircleMovement>();
    }
    if (lookAtScript == null)
    {
        lookAtScript = GetComponent<LookAt>();
    }
}

    void Update()
    {
        // Überprüfe, ob die Umschalttaste gedrückt wurde
        if (Input.GetKeyDown(toggleKey))
        {
            if (isTargetStructure)
            {
                // Zurück zum Cube
                circleMovementScript.target = cube;
                circleMovementScript.radius = 10;
                lookAtScript.target = cube;
            }
            else
            {
                // Ziel auf Structuremiddle setzen
                circleMovementScript.target = structureMiddle;
                circleMovementScript.radius = 17;
                lookAtScript.target = structureMiddle;
            }

            // Zustand umschalten
            isTargetStructure = !isTargetStructure;
        }
    }
}
