using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{

    [SerializeField] bool destroyOnStart;
    [SerializeField] bool delayDestroy;
    [SerializeField] float delayTime;
    bool start;
    private void Start()
    {
        if (destroyOnStart)
        {
            if (delayDestroy)
            {
                start = true;
            }
            else
            {
                Destroy();
            }

        }
    }
    private void Update()
    {
        if (start)
        {
            delayTime -= Time.deltaTime;
            if (delayTime <= 0)
            {
                Destroy();
            }//
        }

    }

    public void Destroy()
    {

        Destroy(gameObject);
    }
}
