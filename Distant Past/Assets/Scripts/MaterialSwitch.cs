using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialSwitch : MonoBehaviour
{
    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] Material newMaterial;
    // Start is called before the first frame update
    public void SwitchMaterial(int which)
    {
        Material[] newMaterials = meshRenderer.materials.Clone() as Material[];

        // Assign the new material to the specified index in the new materials array
        newMaterials[which] = newMaterial;
        meshRenderer.materials = newMaterials;
        Debug.Log("Happened");
    }
}
