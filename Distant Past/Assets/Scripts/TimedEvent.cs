using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class TimedEvent : MonoBehaviour
{
    [SerializeField] UnityEvent onTime;
    [SerializeField] float timeTill;
    float trueTime;
    [SerializeField] bool ready;
    bool set;
    // Start is called before the first frame update
    public void SetReady(bool value)
    {
        ready = value;
    }
    // Update is called once per frame
    void Update()
    {
        if(ready == true)
        {
            if(set != true)
            {
                trueTime = timeTill;
                set = true;
            }
            trueTime -= Time.deltaTime;
            if(trueTime <= 0)
            {
                onTime.Invoke();
                set = false;
                ready = false;

            }
        }
    }
}
