using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class TriggerHandler : MonoBehaviour
{
    [SerializeField] string onEnterTag;
    [SerializeField] UnityEvent onEnter;
    [SerializeField] string onExitTag;
    [SerializeField] UnityEvent onExit;
    [SerializeField] bool destroyAfterEnter;
    [SerializeField] bool destroyAfterExit;
    private void Start()
    {
        if(onEnterTag == "")
        {
            onEnterTag = "Untagged";
        }
        if(onExitTag == "")
        {
            onExitTag = "Untagged";
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(onEnterTag))
        {
            onEnter.Invoke();
            if (destroyAfterEnter)
            {
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(onExitTag))
        {
            onExit.Invoke();
            if (destroyAfterExit)
            {
                Destroy(gameObject);
            }
        }
    }
}
