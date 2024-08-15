using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Interactor : MonoBehaviour
{
    [SerializeField] ImageModifier modifier;
    [SerializeField] LayerMask interactableLayerMask;
    Interactable interactable;

    void Update()
    {
        float interactDistance = 2f;
        if(Physics.Raycast(transform.position, transform.forward, out RaycastHit raycastHit, interactDistance, interactableLayerMask))
        {
            if(raycastHit.transform.TryGetComponent(out Interactable newInteractable))
            {
                if (!newInteractable.enabled)
                {
                    return;
                }
                newInteractable.SetInteractor(this);
                if (interactable == null)
                {
                    interactable = newInteractable;
                }
                if (interactable.ID != newInteractable.ID)
                {
                    interactable.SetInteractor(null);
                    interactable = newInteractable;
                }
                if(interactable.interactIcon != 0)
                {
                    modifier.ChangeIcon(interactable.interactIcon);
                    
                }
                else
                {
                    modifier.ChangeIcon(1);
                }
                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactable.onInteract.Invoke();
                    if(interactable.TryGetComponent(out AudioSource audioSource))
                    {
                        audioSource.Play();
                    }
                }
            }
            else
            {
                modifier.ChangeIcon(0);
            }
        }
        else
        {
            modifier.ChangeIcon(0);
        }
    }

}
