using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{


    public Transform target; 
    float maxDistance = 20f; 
    public Color startColor = Color.white; 
    public Color endColor = Color.red; 

    private Renderer rend; 
    private MaterialPropertyBlock propertyBlock; 
    private void Awake()
    {

        rend = GetComponent<MeshRenderer>();
        propertyBlock = new MaterialPropertyBlock(); 
        rend.GetPropertyBlock(propertyBlock); 
        propertyBlock.SetColor("_Color", startColor); 
        rend.SetPropertyBlock(propertyBlock);
    }
    
    private void Update()
    {
        if(target == null && KeaPlayer.instance != null)
        {
            target = KeaPlayer.instance.transform;
        }
        if(transform != null && target != null)
        {
            float distance = Vector3.Distance(transform.position, target.position);
            float distancePercentage = Mathf.Clamp01(distance / maxDistance);
            Color lerpedColor = Color.Lerp(startColor, endColor, distancePercentage);
            propertyBlock.SetColor("_Color", lerpedColor);
            rend.SetPropertyBlock(propertyBlock);
        }

    }
}
