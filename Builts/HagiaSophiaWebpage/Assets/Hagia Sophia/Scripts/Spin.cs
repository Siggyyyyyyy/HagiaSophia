using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    public float rotationSpeed = 50.0f; // Rotationsgeschwindigkeit in Grad pro Sekunde

    void Update()
    {
        // Drehe das Objekt um die Y-Achse
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }   
}
