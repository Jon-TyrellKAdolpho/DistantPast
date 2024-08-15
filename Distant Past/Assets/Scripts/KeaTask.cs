using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class KeaTask : MonoBehaviour
{
    [SerializeField] UnityEvent onComplete;
    [SerializeField] bool complete;
    public int required;
    [SerializeField] int current;
    public bool falseOnStart = true;
    // Start is called before the first frame update
    public void Progress()
    {

        if(complete != true)
        {
            current++;
            Check();
        }

    }
    public void Check()
    {
        if(current >= required)
        {
            GetComponentInParent<TaskManager>().complete++;
            GetComponentInParent<TaskManager>().Check();
            complete = true;
            onComplete.Invoke();
        }
    }
}
