using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePitch : MonoBehaviour
{
    AudioSource audioSource;
    float originalPitch;
    // Start is called before the first frame update
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        originalPitch = audioSource.pitch;
        float newPitch = Random.Range(originalPitch, originalPitch + .3f);
        audioSource.pitch = newPitch;
        audioSource.Play();
    }

}
