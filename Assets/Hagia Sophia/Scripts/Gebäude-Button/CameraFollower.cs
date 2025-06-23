using UnityEngine;
using System.Collections;

public class CameraFollower : MonoBehaviour
{
    public Vector3 offset = new Vector3(0, 1, -3);
    public float followSpeed = 2f;

    private Transform currentTarget;
    private Vector3 originalPosition;
    private Quaternion originalRotation;

    private bool isFollowing = false;

    void Start()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    void Update()
    {
        if (isFollowing && currentTarget != null)
        {
            Vector3 desiredPosition = currentTarget.position + offset;
            transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * followSpeed);
        }
    }

    public void StartFollowing(Transform newTarget)
    {
        currentTarget = newTarget;
        isFollowing = true;
    }

    public void StopFollowing()
    {
        isFollowing = false;
        StartCoroutine(MoveBackToOriginal());
    }

    private IEnumerator MoveBackToOriginal()
    {
        float elapsed = 0f;
        float duration = 1f;

        Vector3 startPos = transform.position;
        Quaternion startRot = transform.rotation;

        while (elapsed < duration)
        {
            transform.position = Vector3.Lerp(startPos, originalPosition, elapsed / duration);
            transform.rotation = Quaternion.Lerp(startRot, originalRotation, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = originalPosition;
        transform.rotation = originalRotation;
    }
}
