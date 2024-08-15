using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldMovement : MonoBehaviour
{
    public float speed = 5f; // Speed of the movement
    public float moveHeight = 10f; // How high the object should move before resetting

    private Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        // Remember the start position of the object
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Move the object upwards by the speed factor
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        // Check if the object has moved up by the moveHeight amount
        if (transform.position.y >= startPosition.y + moveHeight)
        {
            // Reset the object's position to the start position
            transform.position = startPosition;
        }
    }
}