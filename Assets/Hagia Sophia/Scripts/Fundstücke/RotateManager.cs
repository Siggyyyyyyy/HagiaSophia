using UnityEngine;
using System.Collections;
using System.Linq;

public class RotateManager : MonoBehaviour
{
    public float activationDelay = 1f;

    public void ActivateRotation()
    {
        StartCoroutine(EnableWithDelay());
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

    private IEnumerator EnableWithDelay()
{
    yield return new WaitForSeconds(activationDelay);

    foreach (var rotator in Resources.FindObjectsOfTypeAll<ObjectDragRotator>())
    {
        if (rotator.gameObject.scene.IsValid())
        {
            rotator.enabled = true;
            rotator.ResetRotationInput(); 
        }
    }
}


}
