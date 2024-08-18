using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class CheckPoint : MonoBehaviour
{
    string levelName;
    public string checkPointName;
    public UnityEvent onCheckPointLoad;
    public UnityEvent onCheckPoint;
    public bool reached;
    public Transform spawnPoint;
    private void Start()
    {
        levelName = GetComponentInParent<CheckPointManager>().levelName;
        if(PlayerPrefs.GetInt("CheckPointReached") == 1)
        {
            reached = true;
        }
    }
    public void SetCheckPoint()
    {
        PlayerPrefs.SetString("LastLevel", levelName);
        PlayerPrefs.SetInt("CheckPointReached" + checkPointName, 1);
        PlayerPrefs.SetString("LastCheckPoint" + levelName, checkPointName);
        //FindObjectOfType<SaveManager>().SaveGame();
        onCheckPoint.Invoke();
    }
    
}
