using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialoguePiece : MonoBehaviour
{
    public List<Condition> requirements;
    public Actor actor;
    public UnityEvent onRespond;
    [TextArea(3,1)]
    public string response; // Not applicable on conversation start.
    public UnityEvent afterDialogue;
    [TextArea(5,1)]
    public string dialogue; // Possibly not applicable on conversation end.
    public List<DialoguePiece> responses;
    private void Start()
    {
        if(actor == null)
        {
            actor = GetComponentInParent<Actor>();
        }
        if(responses.Count <= 0)
        {
            foreach(Transform child in transform)
            {
                DialoguePiece dialoguePiece = child.GetComponent<DialoguePiece>();
                if(dialoguePiece != null)
                {
                    responses.Add(dialoguePiece);
                }
            }
        }
    }
}
