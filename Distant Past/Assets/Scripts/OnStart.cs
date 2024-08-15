using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class OnStart : MonoBehaviour
{
    [SerializeField] UnityEvent onStart;
    [SerializeField] UnityEvent onAwake;
    // Start is called before the first frame update
    void Start()
    {
        onStart.Invoke();
    }
    private void Awake()
    {
        onAwake.Invoke();
    }
}
