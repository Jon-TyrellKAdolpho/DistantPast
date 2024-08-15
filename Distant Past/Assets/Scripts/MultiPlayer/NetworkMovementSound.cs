using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class NetworkMovementSound : NetworkBehaviour
{
    Vector3 lastPosition;
    [SerializeField] AudioSource footStepSound;
    [SerializeField] AudioSource footStepSound2;
    int which;
    [SerializeField] NetworkPlayerMovement movement;
    // Start is called before the first frame update
    void Start()
    {
        if (!IsOwner) return;
        lastPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsOwner) return;
        if (Vector3.Distance(lastPosition, transform.position) > 1.5f && movement.grounded == true)
        {
            if (which == 0)
            {
                footStepSound.Play();
                lastPosition = transform.position;
                which = 1;
            }
            else
            {
                footStepSound2.Play();
                lastPosition = transform.position;
                which = 0;
            }

        }
    }
}
