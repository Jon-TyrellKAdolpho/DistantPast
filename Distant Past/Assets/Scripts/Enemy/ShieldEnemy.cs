using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldEnemy : MonoBehaviour
{
    Transform player;
    [SerializeField] GameObject shield;
    [SerializeField] float shieldOffDistance;
  
    // Start is called before the first frame update
    void Start()
    {
        player = KeaPlayer.instance.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, player.position) > shieldOffDistance)
        {
            if(shield.activeInHierarchy == true)
            {
                shield.SetActive(false);
            }
        }
        else
        {
            if(shield.activeInHierarchy != true)
            {
                shield.SetActive(true);
            }
        }
    }
}
