using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootDrop : MonoBehaviour
{
    [SerializeField] GameObject effect;
    [SerializeField] List<GameObject> possibleDrops;
    
    public void DropLoot()
    {
        if(effect != null)
        {
            GameObject newEffect = Instantiate(effect);
            newEffect.transform.position = transform.position;
            newEffect.GetComponent<ParticleSystem>().Play();
        }

        GameObject newDrop = null;
        if (possibleDrops.Count > 0)
        {
            int which = Random.Range(0, possibleDrops.Count);
            newDrop = Instantiate(possibleDrops[which],transform.position,Quaternion.identity);
            if(newDrop.GetComponent<Explosion>() == true)
            {
                newDrop.GetComponent<Explosion>().attacker = transform;
            }
        }
        Destroy(gameObject);
    }
}
