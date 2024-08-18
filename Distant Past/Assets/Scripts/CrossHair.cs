using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHair : MonoBehaviour
{
    ImageModifier imageModifier;
    [SerializeField] float range = 50;
    [SerializeField] LayerMask hitMask;
    // Start is called before the first frame update
    void Start()
    {
        imageModifier = GetComponent<ImageModifier>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
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
        */
    }

    public void SetRange(float value)
    {
        range = value;
    }
}
