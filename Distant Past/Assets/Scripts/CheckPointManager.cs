using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class CheckPointManager : MonoBehaviour
{
    public string levelName;
    public List<CheckPoint> checkPoints;
    private void Start()
    {
        checkPoints.Clear();
        checkPoints = GetComponentsInChildren<CheckPoint>().ToList();
        string lastCheckpointName = PlayerPrefs.GetString("LastCheckPoint" + levelName);


        for (int i = 0; i < checkPoints.Count; i++)
        {
            if (checkPoints[i].checkPointName == lastCheckpointName)
            {
                for (int j = 0; j < i; j++)
                {
                    Destroy(checkPoints[j].gameObject);
                }
                checkPoints[i].onCheckPointLoad.Invoke();
                //FindObjectOfType<SaveManager>().LoadGame();
//KeaPlayer.instance.transform.position = checkPoints[i].spawnPoint.position;
                break;
            }
        }
    }
}

