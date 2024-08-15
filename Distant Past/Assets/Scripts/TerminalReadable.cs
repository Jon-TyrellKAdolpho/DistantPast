using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminalReadable : MonoBehaviour
{
    [TextArea(10,10)]
    [SerializeField] string log;
    bool read;
    [SerializeField] Vector2 expGive;
    public void ReadLog()
    {
        KeaPlayer.instance.GetComponentInChildren<TerminalReader>().NewLog(log);
        if(read != true)
        {
            if(expGive.x > 0 && expGive.y > 0)
            {
                KeaPlayer.instance.GainExp(Mathf.RoundToInt(Random.Range(expGive.x, expGive.y)));
            }

            read = true;
        }
    }
}
