using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSound : MonoBehaviour
{
    Vector3 lastPosition;
    [SerializeField] List<AudioClip> footStepClip;
    [SerializeField] AudioSource footStepSound;
    int which;
    [SerializeField] FirstPersonMovement movement;
    float originalPitch;
    int soundType;
    // Start is called before the first frame update
    void Start()
    {
        originalPitch = footStepSound.pitch;
        lastPosition = transform.position;
    }
    void Update()
    {
        if (Vector3.Distance(lastPosition, transform.position) > 2f && movement.grounded == true)
        {
            float newPitch = Random.Range(originalPitch, originalPitch + .3f);
            footStepSound.pitch = newPitch;
            footStepSound.Play();
            lastPosition = transform.position;
        }
    }
    public int GetSoundType()
    {
        int type = soundType;
        return type;
    }
    public void SetClip(int which)
    {
        footStepSound.clip = footStepClip[which];
    }
}
