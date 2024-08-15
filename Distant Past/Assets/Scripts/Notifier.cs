using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notifier : MonoBehaviour
{
    [SerializeField] string notification;
    public void Notify()
    {
        PauseHandler.instance.Notify(notification);
    }
}
