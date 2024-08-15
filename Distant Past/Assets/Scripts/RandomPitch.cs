using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPitch : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] Vector2 pitchRange;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.pitch = Random.Range(pitchRange.x, pitchRange.y);
    }

}
