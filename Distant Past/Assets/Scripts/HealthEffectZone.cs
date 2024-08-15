using System.Collections.Generic;
using UnityEngine;

public class HealthEffectZone : MonoBehaviour
{
    // Restrict who is affected by this area.
    [SerializeField] string tagRestriction;
    [SerializeField] bool damage;
    [SerializeField] int effectAmount;
    [SerializeField] float effectInterval;
    private List<Health> inZone = new List<Health>();
    private float nextEffectTime;

    private void Start()
    {
        nextEffectTime = Time.time + effectInterval;
    }

    private void Update()
    {
        if (Time.time >= nextEffectTime)
        {
            ApplyEffect();
            nextEffectTime = Time.time + effectInterval;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!string.IsNullOrEmpty(tagRestriction) && other.CompareTag(tagRestriction) || string.IsNullOrEmpty(tagRestriction))
        {
            Health health = other.GetComponent<Health>();
            if (health != null && !inZone.Contains(health))
            {
                inZone.Add(health);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!string.IsNullOrEmpty(tagRestriction) && other.CompareTag(tagRestriction) || string.IsNullOrEmpty(tagRestriction))
        {
            Health health = other.GetComponent<Health>();
            if (health != null && inZone.Contains(health))
            {
                inZone.Remove(health);
            }
        }
    }

    private void ApplyEffect()
    {
        if (inZone.Count > 0)
        {
            for (int i = 0; i < inZone.Count; i++)
            {
                if (damage)
                {
                    inZone[i].TakeDamage(effectAmount, null);
                    Debug.Log("Heyyy");
                }
                else
                {
                    inZone[i].Heal(effectAmount);
                }
            }
        }
    }
}
