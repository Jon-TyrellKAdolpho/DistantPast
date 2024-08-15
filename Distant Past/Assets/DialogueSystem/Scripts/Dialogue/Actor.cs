using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    public string actorName;
    public List<Condition> conditions;
    public void StartConversation()
    {
        foreach(Transform child in transform)
        {
            DialoguePiece dialoguePiece = child.GetComponent<DialoguePiece>();
            if(dialoguePiece != null)
            {
                if (CheckConditions(dialoguePiece.requirements))
                {
                    DialogueManager.instance.StartDialogue(dialoguePiece);
                }

            }
        }
    }

    public void SetConditionTrue(string condition)
    {
        for (int i = 0; i < conditions.Count; i++)
        {
            if(conditions[i].condition == condition)
            {
                conditions[i].state = true;
            }
        }
    }
    public void SetConditionFalse(string condition)
    {
        for (int i = 0; i < conditions.Count; i++)
        {
            if (conditions[i].condition == condition)
            {
                conditions[i].state = false;
            }
        }
    }

    public bool CheckConditions(List<Condition> value)
    {
        for (int i = 0; i < value.Count; i++)
        {
            for (int n = 0; n < conditions.Count; n++)
            {
                if(conditions[n].condition == value[i].condition)
                {
                    if(conditions[n].state != value[i].state)
                    {
                        return false;
                    }

                }
            }
        }
        return true;
    }
}
