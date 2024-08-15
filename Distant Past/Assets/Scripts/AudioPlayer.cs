using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] List<AudioSource> audioSources;

    public void PlayStopAudio(string value)
    {
        string[] info = value.Split(',');
        int whichSource = int.Parse(info[0]);
        int playPause = int.Parse(info[1]);

        if(playPause == 0)
        {
            audioSources[whichSource].Stop();
        }
        else
        {
            audioSources[whichSource].Play();
        }

    }
}
