using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BatteryDrop : MonoBehaviour
{
    [SerializeField] List<GameObject> batteries;
    public List<GameObject> available;
    // Start is called before the first frame update
    private void Awake()
    {
        /*
        KeaPlayer player = KeaPlayer.instance;
        if (player.blue.active == true)
        {
            available.Add(batteries[0]);
        }
        if(player.yellow.active == true)
        {
            available.Add(batteries[1]);
        }
        if(player.green.active == true)
        {
            available.Add(batteries[2]);
        }
        int which = Random.Range(0, available.Count);
        GameObject drop = Instantiate(available[which]);
        drop.transform.position = transform.position;
        List<Battery> oldBatteries = FindObjectsOfType<Battery>().ToList();

        if(oldBatteries.Count > 30)
        {
            Destroy(oldBatteries[0].GetComponentInParent<Spin>().gameObject);
        }

        Destroy(gameObject);
        */
    }
}
