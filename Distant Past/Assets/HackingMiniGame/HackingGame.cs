using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class HackingGame : MonoBehaviour
{
    [SerializeField] UnityEvent onSuccess;
    AudioSource audioSource;
    [SerializeField] Vector2 expGive;
    
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void SetHackingGame()
    {
        HackingGameManager.instance.onSuccess = onSuccess;
        HackingGameManager.instance.onSuccess.AddListener(audioSource.Play);
        HackingGameManager.instance.onSuccess.AddListener(GiveExp);
        HackingGameManager.instance.StartGame();
        Interactable interactable = GetComponentInChildren<Interactable>();
        if(interactable != null)
        {
            interactable.enabled = false;
        }
        interactable = GetComponent<Interactable>();
        if (interactable != null)
        {
            interactable.enabled = false;
        }
        audioSource.Play();
    }
    void GiveExp()
    {
        if (expGive.x > 0 && expGive.y > 0)
        {
            KeaPlayer.instance.GainExp(Mathf.RoundToInt(Random.Range(expGive.x, expGive.y)));
        }
    }
}
