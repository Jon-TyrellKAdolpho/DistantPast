using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpingObject : MonoBehaviour
{
    [SerializeField] Transform target;
    public Transform targetPosition;
    public float lerpSpeed = 2f; // Set the speed of the lerp
    public float arrivalThreshold = 0.1f; // Set the threshold for considering the object as arrived

    private void Update()
    {
        // Lerping the object towards the target position
        target.position = Vector3.Lerp(transform.position, targetPosition.position, lerpSpeed * Time.deltaTime);

        // Check if the object is close enough to the target position
        if (Vector3.Distance(target.position, targetPosition.position) < arrivalThreshold)
        {
            OnArrival(); // Call the function when the object has arrived
        }
    }

    private void OnArrival()
    {
        Destroy(gameObject);
    }
}