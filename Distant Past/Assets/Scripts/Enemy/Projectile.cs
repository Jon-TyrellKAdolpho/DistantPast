using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Projectile : MonoBehaviour
{
    public Transform attacker;
    [SerializeField] int damage;
    [SerializeField] UnityEvent onHit;
    [SerializeField] LayerMask layer;
    private void OnTriggerEnter(Collider other)
    {
        if ((layer.value & (1 << other.gameObject.layer)) != 0)
        {
            if (other.GetComponent<Health>() != null)
            {
                other.GetComponent<Health>().TakeDamage(damage,attacker);

            }
            onHit.Invoke();
            Destroy(gameObject);
        }

    }
}
