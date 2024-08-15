using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    [SerializeField] bool player;
    [SerializeField] Transform lookAt;
    [SerializeField] bool leftRight;

    // Start is called before the first frame update
    void Start()
    {
        if (player)
        {
            if (lookAt == null)
            {
                if (FindObjectOfType<FirstPersonMovement>() != null)
                {
                    lookAt = FindObjectOfType<FirstPersonMovement>(true).GetComponentInChildren<Camera>().transform;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            if (lookAt == null)
            {
                if (FindObjectOfType<FirstPersonMovement>() != null)
                {
                    lookAt = FindObjectOfType<FirstPersonMovement>(true).GetComponentInChildren<Camera>().transform;
                }
            }
        }
        if (lookAt != null)
        {
            if (leftRight)
            {
                // Get the target position on the same Y level as this object
                Vector3 targetPosition = new Vector3(lookAt.position.x, transform.position.y, lookAt.position.z);
                transform.LookAt(targetPosition);
            }
            else
            {
                transform.LookAt(lookAt.position);
            }
        }
    }
}
