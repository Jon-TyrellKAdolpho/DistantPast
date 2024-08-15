using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class InteractableSwitch : MonoBehaviour
{
    [SerializeField] UnityEvent onTrue;
    [SerializeField] UnityEvent onFalse;
    [SerializeField] bool toggle;

    public void SwitchToggleInvoke()
    {
        toggle = !toggle;
        if(toggle == true)
        {
            onTrue.Invoke();
        }
        else
        {
            onFalse.Invoke();
        }
    }
    public void SwitchToggle()
    {
        toggle = !toggle;
    }

    public void OnTrue()
    {
        onTrue.Invoke();
    }
    public void OnFalse()
    {
        onFalse.Invoke();
    }
}
