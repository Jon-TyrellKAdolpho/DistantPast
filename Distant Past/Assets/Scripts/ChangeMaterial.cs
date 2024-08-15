using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterial : MonoBehaviour
{
    MeshRenderer meshRenderer;
    [SerializeField] int whichMaterial;
    [SerializeField] List<Material> materials;
    public void SetMaterial(int value)
    {
        meshRenderer.materials[whichMaterial] = materials[value];
    }
}
