using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class NetworkCrossHair : NetworkBehaviour
{
    Transform cam;
    ImageModifier imageModifier;
    [SerializeField] float range = 50;
    [SerializeField] LayerMask hitMask;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponentInParent<NetworkPlayerMovement>().GetComponentInChildren<Camera>(true).transform;
        imageModifier = GetComponent<ImageModifier>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner) return;
        RaycastHit hit;
        if (Physics.Raycast(cam.position, cam.forward, out hit, range, hitMask))
        {
            Health health = hit.collider.GetComponent<Health>();
            if (health != null)
            {
                imageModifier.ChangeIcon(1);
            }
            else
            {
                imageModifier.ChangeIcon(0);
            }
        }
        else
        {
            imageModifier.ChangeIcon(0);
        }
    }

    public void SetRange(float value)
    {
        range = value;
    }
}
