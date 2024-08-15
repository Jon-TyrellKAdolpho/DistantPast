using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AILightBall : MonoBehaviour
{
    [SerializeField] Vector2 speed = new Vector2(.4f, .5f);
    [SerializeField] Vector2 height = new Vector2(.25f, .3f);
    float trueSpeed;
    float trueHeight;

    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
        trueSpeed = Random.Range(speed.x, speed.y);
        trueHeight = Random.Range(height.x, height.y);
    }

    void Update()
    {
        // Calculate the new Y position
        float newY = initialPosition.y + Mathf.Sin(Time.time * trueSpeed) * trueHeight;
        // Set the new position
        transform.position = new Vector3(initialPosition.x, newY, initialPosition.z);
    }
}
