using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class TaskManager : MonoBehaviour
{
    List<KeaTask> tasks = new List<KeaTask>();
    [HideInInspector]
    public int complete;
    bool alreadyComplete;
    [SerializeField] UnityEvent onComplete;
    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform child in transform)
        {
            KeaTask task = child.GetComponent<KeaTask>();
            if (task != null)
            {
                tasks.Add(task);
                if (task.falseOnStart)
                {
                    task.gameObject.SetActive(false);
                }

            }
        }
    }

    public void Check()
    {
        if(alreadyComplete == true)
        {
            return;
        }
        if(complete >= tasks.Count)
        {
            onComplete.Invoke();
            alreadyComplete = true;
        }
    }
}
