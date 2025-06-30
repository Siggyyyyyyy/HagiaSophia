using UnityEngine;
using System.Collections;

public class BuildingPlacementMover : MonoBehaviour
{
    [Header("Zielposition im Gebäude")]
    public Transform buildingTarget;

    [Header("Animationseinstellungen")]
    public float moveDuration = 1.2f;

    private Vector3 originalPosition;
    private Quaternion originalRotation;

    private bool isMoved = false;
    private Coroutine moveCoroutine;

   
    public void SetCurrentAsOriginal()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    public void MoveToBuilding()
    {
        if (isMoved) return;

        if (moveCoroutine != null)
            StopCoroutine(moveCoroutine);

        moveCoroutine = StartCoroutine(MoveOverTime(transform.position, buildingTarget.position,
                                                    transform.rotation, buildingTarget.rotation));
        isMoved = true;
    }

    public void MoveBackToOriginal()
    {
        if (!isMoved) return;

        if (moveCoroutine != null)
            StopCoroutine(moveCoroutine);

        moveCoroutine = StartCoroutine(MoveOverTime(transform.position, originalPosition,
                                                    transform.rotation, originalRotation));
        isMoved = false;
    }

    private IEnumerator MoveOverTime(Vector3 startPos, Vector3 endPos, Quaternion startRot, Quaternion endRot)
    {
        float elapsed = 0f;

        while (elapsed < moveDuration)
        {
            float t = elapsed / moveDuration;
            transform.position = Vector3.Lerp(startPos, endPos, t);
            transform.rotation = Quaternion.Slerp(startRot, endRot, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = endPos;
        transform.rotation = endRot;
    }
}
