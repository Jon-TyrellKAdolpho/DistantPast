using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    [SerializeField] DialoguePiece currentDialogue;

    [SerializeField] GameObject display;
    [SerializeField] TextMeshProUGUI speakerName;
    [SerializeField] TextMeshProUGUI speakerText;

    [SerializeField] RectTransform responseHolder;
    [SerializeField] GameObject responseButtonPrefab;

    [SerializeField] int displayType;
    [SerializeField] float delayTime = 2;

    [SerializeField] bool controlCursor;
    [SerializeField] List<GameObject> turnOff;

    [SerializeField] GameObject audioPrefab;

    void Start()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        EndDialogue();
    }
    public void StartDialogue(DialoguePiece value)
    {
        currentDialogue = value;
        speakerName.text = value.actor.actorName + ":";
        currentDialogue.onRespond.Invoke();
        
        if(display.activeInHierarchy != true)
        {
            if (controlCursor)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0; // Or pause feature
            }
            for (int i = 0; i < turnOff.Count; i++)
            {
                turnOff[i].SetActive(false);
            }
            display.SetActive(true);
        }


        foreach(RectTransform child in responseHolder)
        {
            Destroy(child.gameObject);
        }
        if(displayType == 0)
        {
            //Instant
            speakerText.text = currentDialogue.dialogue;
            DisplayResponses();
        }
        if(displayType == 1)
        {
            StartCoroutine(TypeWriterST(currentDialogue.dialogue));
        }
        if(displayType == 2)
        {
            StartCoroutine(DelayST(currentDialogue.dialogue));
        }
        
    }

    IEnumerator TypeWriterST(string value)
    {
        for (int i = 0; i <= value.Length; i++)
        {
            speakerText.text = value.Substring(0, i);
            Instantiate(audioPrefab, transform.position, Quaternion.identity);
            yield return new WaitForSecondsRealtime(.03f);
        }
        DisplayResponses();
    }
    IEnumerator DelayST(string value)
    {
        speakerText.text = value;
        yield return new WaitForSecondsRealtime(value.Length * .03f);
        DisplayResponses();
    }
    void DisplayResponses()
    {
        if (currentDialogue.responses.Count <= 0 && currentDialogue.dialogue != "")
        {
            StartCoroutine(DelayEnd(currentDialogue.dialogue));
        }

        for (int i = 0; i < currentDialogue.responses.Count; i++)
        {
            if (currentDialogue.actor.CheckConditions(currentDialogue.responses[i].requirements))
            {
                Button responseButton = Instantiate(responseButtonPrefab.GetComponent<Button>());
                RectTransform rectTransform = responseButton.GetComponent<RectTransform>();
                rectTransform.SetParent(responseHolder);
                rectTransform.localScale = Vector3.one;

                TextMeshProUGUI textMesh = responseButton.GetComponentInChildren<TextMeshProUGUI>();
                textMesh.text = currentDialogue.responses[i].response;
                Button button = responseButton.GetComponent<Button>();
                DialoguePiece dialoguePiece = currentDialogue.responses[i];
                button.onClick.AddListener(() => StartDialogue(dialoguePiece));
            }

        }
    }
    IEnumerator DelayEnd(string value)
    {
        yield return new WaitForSecondsRealtime((value.Length * .03f) + delayTime);
        EndDialogue();
    }
    void EndDialogue()
    {
        if (controlCursor)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1; // Or pause feature
        }
        if (currentDialogue != null)
        {
            currentDialogue.afterDialogue.Invoke();
        }
        if(turnOff.Count > 0)
        {
            for (int i = 0; i < turnOff.Count; i++)
            {
                turnOff[i].SetActive(true);
            }
        }

        display.SetActive(false);
    }
}
