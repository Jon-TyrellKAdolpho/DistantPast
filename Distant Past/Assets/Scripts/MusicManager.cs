using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public float detectionRadius = 5f;
    [SerializeField] AudioSource ambient;
    AudioClip quedAmbientClip;
    [SerializeField] List<AudioClip> ambientSongs;
    [SerializeField] AudioSource battle;
    AudioClip quedBattleClip;
    [SerializeField] List<AudioClip> battleSongs;
    public float fadeDuration = 1.0f; 
    private bool enemiesDetected;

    public Coroutine fadeCorou1;
    public Coroutine fadeCorou2;

    void Update()
    {
        enemiesDetected = DetectEnemies();
        PlayMusic();
    }
    void PlayMusic()
    {
        if (PauseHandler.instance.isPaused)
        {
            return;
        }
        if (!enemiesDetected && ambient.volume == 0)
        {
            if(quedAmbientClip != null)
            {
                ambient.clip = quedAmbientClip;
            }
            else
            {
                ambient.clip = ambientSongs[Random.Range(0, ambientSongs.Count)];
            }
           fadeCorou1 = StartCoroutine(FadeMusic(ambient, 1, fadeDuration));
           fadeCorou2 =  StartCoroutine(FadeMusic(battle, 0, fadeDuration));
        }
        if(enemiesDetected && battle.volume == 0)
        {
            if (quedBattleClip != null)
            {
                battle.clip = quedBattleClip;
            }
            else
            {
                battle.clip = battleSongs[Random.Range(0, battleSongs.Count)];
            }
            fadeCorou1 = StartCoroutine(FadeMusic(battle, 1, fadeDuration));
            fadeCorou2 = StartCoroutine(FadeMusic(ambient, 0, fadeDuration));
            
        }
    }
    public void SetAmbientClip(int which)
    {
        quedAmbientClip = ambientSongs[which];
    }
    public void UnsetAmbientClip()
    {
        quedAmbientClip = null;
    }
    public void SetBattleClip(int which)
    {
        quedBattleClip = battleSongs[which];
    }
    public void UnsetBattleClip()
    {
        quedBattleClip = null;
    }
    bool DetectEnemies()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius);
        foreach (Collider collider in colliders)
        {
            Enemy enemy = collider.GetComponent<Enemy>();
            if(enemy == null)
            {
                continue;
            }
            if (enemy.triggerBattleMusic)
            {
                return true;
            }
        }
        return false;
    }

    IEnumerator FadeMusic(AudioSource audioSource, float targetVolume, float duration)
    {
        bool play = false;
        if (targetVolume == 1)
        {
            play = true;
           // Debug.Log("Playing audio source: " + audioSource.clip.name);
            audioSource.Play();
        }
        float startVolume = audioSource.volume;
        float startTime = Time.time;

        while (Time.time < startTime + duration)
        {
            audioSource.volume = Mathf.Lerp(startVolume, targetVolume, (Time.time - startTime) / duration);
            yield return null;
        }

        audioSource.volume = targetVolume;
        if (!play)
        {
            Debug.Log("Stopping audio source: " + audioSource.clip.name);
            audioSource.Stop();
        }
    }
    public void StopRoutines()
    {
       // StopAllCoroutines();
        StopCoroutine(fadeCorou1);
        StopCoroutine(fadeCorou2);
    }
}
