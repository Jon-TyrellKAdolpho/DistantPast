using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunDrop : MonoBehaviour
{
    [SerializeField] string gun;
    [SerializeField] GameObject soundDrop;
    [SerializeField] CheckPoint point;
    private void Start()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<KeaPlayer>())
        {
            GunManager gunManager = other.GetComponentInChildren<GunManager>();

            for (int i = 0; i < gunManager.guns.Count; i++)
            {
                if(gunManager.guns[i].name == gun)
                {
                    gunManager.UnlockGun(gun);
                    FindObjectOfType<SaveManager>().SaveGame();
                }
            }
            KeaPlayer.instance.blue.Check();
            KeaPlayer.instance.yellow.Check();
            KeaPlayer.instance.green.Check();
            Instantiate(soundDrop, transform.position, Quaternion.identity);
            Destroy(gameObject.GetComponentInParent<Spin>().gameObject);
        }
    }
}
