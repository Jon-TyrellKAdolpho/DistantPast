using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    public float speed = 1f;

    void Update()
    {
        // Rotate the object gradually over time
        transform.Rotate(Vector3.up, speed * 10 * Time.deltaTime);
    }

}
