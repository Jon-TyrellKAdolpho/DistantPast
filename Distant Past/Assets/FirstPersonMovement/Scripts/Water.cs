using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    [SerializeField] bool nightTime;
    [SerializeField] Material dayTimeMaterial;
    [SerializeField] Material nightTimeMaterial;
    [SerializeField] Transform waterGFX;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("SwimEnter"))
        {
            other.GetComponentInParent<FirstPersonMovement>().UnderWater();
        }
        if(other.GetComponent<FirstPersonLook>() != null)
        {
            other.GetComponent<FirstPersonLook>().SetSwimActive(true);
            waterGFX.localEulerAngles = new Vector3(180, 0, 0);
            waterGFX.GetComponent<MeshRenderer>().material = dayTimeMaterial;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("SwimExit"))
        {
            other.GetComponentInParent<FirstPersonMovement>().OnLand();
        }
        if (other.GetComponent<FirstPersonLook>() != null)
        {
            other.GetComponent<FirstPersonLook>().SetSwimActive(false);
            waterGFX.localEulerAngles = Vector3.zero;
            if(nightTime == true)
            {
                waterGFX.GetComponent<MeshRenderer>().material = nightTimeMaterial;
            }
            else
            {
                waterGFX.GetComponent<MeshRenderer>().material = dayTimeMaterial;
            }
            
        }

    }

}
