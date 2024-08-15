using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFrameRate : MonoBehaviour
{
    [SerializeField] int targetFrame;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = targetFrame;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
