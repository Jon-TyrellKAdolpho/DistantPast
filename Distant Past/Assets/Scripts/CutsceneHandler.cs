using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneHandler : MonoBehaviour
{
    [SerializeField] Animator character1;
    [SerializeField] List<string> character1Bools;
    [SerializeField] List<AudioSource> audios;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void ControlCharacter1BoolTrue(string value)
    {
        for (int i = 0; i < character1Bools.Count; i++)
        {
            character1.SetBool(character1Bools[i], false);
        }
        character1.SetBool(value, true);
    }
    public void PlayAudioOneShot(int which)
    {
        audios[which].Play();
    }
    public void LoadScene(string value)
    {
        SceneManager.LoadScene(value);
    }
}
