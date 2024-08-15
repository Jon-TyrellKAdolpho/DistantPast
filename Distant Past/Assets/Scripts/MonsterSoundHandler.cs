using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSoundHandler : MonoBehaviour
{
    AudioSource audioSource;
    public Vector2 playIntervalRange = new Vector2(3f, 7f); // Adjust the interval range between sound plays as needed
    float originalPitch;
    void OnEnable()
    {
        audioSource = GetComponent<AudioSource>();
        originalPitch = audioSource.pitch;
        // Invoke the PlayRandomSound method with a random delay initially
        Invoke("PlayRandomSound", Random.Range(playIntervalRange.x, playIntervalRange.y));
    }
    private void OnDisable()
    {
        CancelInvoke("PlayRandomSound");
    }

    void PlayRandomSound()
    {
        if (audioSource != null)
        {
            float newPitch = Random.Range(originalPitch, originalPitch + .3f);
            audioSource.pitch = newPitch;
            audioSource.Play();

            // Invoke the PlayRandomSound method with a new random delay
            Invoke("PlayRandomSound", Random.Range(playIntervalRange.x, playIntervalRange.y));
        }
        else
        {
            Debug.LogWarning("AudioSource is not assigned!");
        }
    }
}
