using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Interactable : MonoBehaviour
{
    public UnityEvent onInteract;
    public int interactIcon;
    [HideInInspector]
    public int ID;
    [SerializeField] Interactor interactor;
    void Start()
    {
        ID = Random.Range(0, 999999);   
    }
    public bool InteractorSet()
    {
        bool check = false;
        if(interactor != null)
        {
            check = true;
        }
        return check;
    }
    public void SetInteractor(Interactor value)
    {
        interactor = value;
    }

}
