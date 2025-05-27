using UnityEngine;
using System.Collections;

public class BuildingPlacementMover : MonoBehaviour
{
    public Transform buildingTarget; // Ziel im Gebäude (im Inspector setzen)
    private Vector3 originalPosition;
    private Quaternion originalRotation;

    public float moveDuration = 1.2f;

    private bool isMoved = false;
    private Coroutine moveCoroutine;

    void Start()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    public void MoveToBuilding()
    {
        if (!isMoved)
        {
            if (moveCoroutine != null) StopCoroutine(moveCoroutine);
            moveCoroutine = StartCoroutine(MoveOverTime(transform.position, buildingTarget.position,
                                                         transform.rotation, buildingTarget.rotation));
            isMoved = true;
        }
    }

    public void MoveBackToOriginal()
    {
        if (isMoved)
        {
            if (moveCoroutine != null) StopCoroutine(moveCoroutine);
            moveCoroutine = StartCoroutine(MoveOverTime(transform.position, originalPosition,
                                                         transform.rotation, originalRotation));
            isMoved = false;
        }
    }

    private IEnumerator MoveOverTime(Vector3 startPos, Vector3 endPos, Quaternion startRot, Quaternion endRot)
    {
        float elapsed = 0f;

        while (elapsed < moveDuration)
        {
            transform.position = Vector3.Lerp(startPos, endPos, elapsed / moveDuration);
            transform.rotation = Quaternion.Slerp(startRot, endRot, elapsed / moveDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = endPos;
        transform.rotation = endRot;
    }
}
