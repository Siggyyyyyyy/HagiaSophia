using UnityEngine;
using System.Linq;

public class RotateManager : MonoBehaviour
{
    public void ActivateRotation()
    {
        foreach (var rotator in Resources.FindObjectsOfTypeAll<ObjectDragRotator>())
        {
            // Nur Objekte in der Szene, nicht in Prefabs im Projektordner
            if (rotator.gameObject.scene.IsValid())
            {
                rotator.enabled = true;
            }
        }
    }

    public void DeactivateRotation()
    {
        foreach (var rotator in Resources.FindObjectsOfTypeAll<ObjectDragRotator>())
        {
            if (rotator.gameObject.scene.IsValid())
            {
                rotator.enabled = false;
            }
        }
    }
}
