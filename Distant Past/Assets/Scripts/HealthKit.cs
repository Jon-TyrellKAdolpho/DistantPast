using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthKit : MonoBehaviour
{
    [SerializeField] int healthAmount;
    [SerializeField] GameObject soundDrop;
    [SerializeField] Vector2 expGiveAmount;
    private void Awake()
    {
        if (Trailer.instance.trailer)
        {
            GetComponentInChildren<Light>().enabled = false;
            GetComponent<MeshRenderer>().enabled = false;
            soundDrop = null;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            KeaPlayer player = FindObjectOfType<KeaPlayer>();
            if(player.GetComponent<Health>().currentHealth >= player.GetComponent<Health>().maxHealth)
            {
                int trueGive = Mathf.RoundToInt(Random.Range(expGiveAmount.x, expGiveAmount.y));
                //KeaPlayer.instance.GainExp(trueGive);

            }
            else
            {
                player.GetComponent<Health>().Heal(healthAmount);
                player.SetHealthSlider();
            }

            
            if (GetComponent<LootDrop>() != null)
            {
                GetComponent<LootDrop>().DropLoot();
            }
            if(soundDrop != null)
            {
                Instantiate(soundDrop, transform.position, Quaternion.identity);
            }

            Destroy(gameObject.GetComponentInParent<Spin>().gameObject);
        }
    }
}
