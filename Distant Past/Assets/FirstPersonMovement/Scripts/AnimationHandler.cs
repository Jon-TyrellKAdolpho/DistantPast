using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void SetBoolTrue(string value)
    {
        anim.SetBool(value, true);
    }
    public void SetBoolFalse(string value)
    {
        anim.SetBool(value, false);
    }
    public void ToggleBool(string booltoggle)
    {
        anim.SetBool(booltoggle, !anim.GetBool(booltoggle));
    }
}
