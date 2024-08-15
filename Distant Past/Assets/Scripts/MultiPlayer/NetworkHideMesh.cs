using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class NetworkHideMesh : NetworkBehaviour
{
    [SerializeField] bool onOwner;
    bool setUp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!onOwner)
        {
            if (!IsOwner)
            {
                if (!setUp)
                {
                    GetComponent<MeshRenderer>().enabled = false;
                    Debug.Log("Turned off mesh");
                    setUp = true;
                }
            }
        }
        else
        {
            if (IsOwner)
            {
                if (!setUp)
                {
                    GetComponent<MeshRenderer>().enabled = false;
                    Debug.Log("Turned off mesh");
                    setUp = true;
                }
            }
        }
    }
}
