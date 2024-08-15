using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Battery : MonoBehaviour
{
    [SerializeField] float chargeAmount;
    [SerializeField] int energyType;
    [SerializeField] GameObject soundDrop;
    [SerializeField] Vector2 expGiveAmount;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Energy energy = null;
            if (energyType == 0)
            {
                energy = KeaPlayer.instance.blue;
            }
            if (energyType == 1)
            {
                energy = KeaPlayer.instance.yellow;
            }
            if (energyType == 2)
            {
                energy = KeaPlayer.instance.green;
            }
            if (energy.currentEnergy >= energy.maxEnergy)
            {
                int trueGive = Mathf.RoundToInt(Random.Range(expGiveAmount.x, expGiveAmount.y));
                KeaPlayer.instance.GainExp(trueGive);
            }
            else
            {
                energy.ChargeEnergy(chargeAmount);
            }

            if(GetComponent<LootDrop>() != null)
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
