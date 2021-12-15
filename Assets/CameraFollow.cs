// Smooth towards the target

using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.2F;
    private Vector3 velocity = Vector3.zero;
    private bool ignoreY=true;
    Vector3 pointVector = new Vector3(0, 1, -10);
    private void Awake()
    {
        transform.position = target.transform.position+Vector3.up;
    }
    public void ActivateHeightMovement()
    {
        ignoreY = false;
    }
    public void PlayerDied()
    {
        pointVector= new Vector3(0, 0.2f, -10);
        smoothTime = 0.5F;
        ActivateHeightMovement();
    }
    public void DeactivateHeightMovement()
    {
        ignoreY = true;
    }
    void FixedUpdate()
    {
        // Define a target position above and behind the target transform
        Vector3 targetPosition = target.TransformPoint(pointVector);
        if(ignoreY)
            targetPosition = new Vector3(targetPosition.x, transform.position.y, targetPosition.z);
        else
            targetPosition = new Vector3(targetPosition.x, targetPosition.y, targetPosition.z);
        // Smoothly move the camera towards that target position
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}