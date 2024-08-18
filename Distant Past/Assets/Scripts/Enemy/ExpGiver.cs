using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpGiver : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] bool giveOnStart = true;
    [SerializeField] Vector2 expToGive;
    void Awake()
    {
        if (giveOnStart)
        {
            GiveExp();
        }

    }

    public void GiveExp()
    {
        /*
        int trueGive = Mathf.RoundToInt(Random.Range(expToGive.x, expToGive.y));
        KeaPlayer.instance.GainExp(trueGive);
        */
    }
}
