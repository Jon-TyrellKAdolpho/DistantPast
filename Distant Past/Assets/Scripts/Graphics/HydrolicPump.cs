using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HydrolicPump : MonoBehaviour
{
    Animator anim;
    [SerializeField] Vector2 timeRange;
    float timetill;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.enabled = false;
        timetill = Random.Range(timeRange.x, timeRange.y);
    }

    // Update is called once per frame
    void Update()
    {
        if(timetill > 0)
        {
            timetill -= Time.deltaTime;
        }
        else
        {
            anim.enabled = true;
        }
    }
}
