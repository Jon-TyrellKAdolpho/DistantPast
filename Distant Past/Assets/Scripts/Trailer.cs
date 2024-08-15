using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trailer : MonoBehaviour
{
    public static Trailer instance;
    public bool trailer;
    private void Start()
    {
        instance = this;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backslash))
        {
            trailer = !trailer;
        }
    }
}
