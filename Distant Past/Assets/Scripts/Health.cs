using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Health : MonoBehaviour
{
    public int baseHealth;
    public int maxHealth;
    public int currentHealth;
    [SerializeField] bool maxOnStart;

    [SerializeField] UnityEvent onDamaged;
    [SerializeField] UnityEvent onHealed;
    [SerializeField] UnityEvent onDeath;

    [SerializeField] bool destroyOnDeath = true;
    public Transform lastAttacker;

    bool dead;
    private void Start()
    {
        if (maxOnStart)
        {
            currentHealth = maxHealth;
        }
        KeaTask task = GetComponentInParent<KeaTask>();
        if (task != null)
        {
            task.required++;
        }
    }

    public void TakeDamage(int amount, Transform attacker)
    {
        currentHealth -= amount;
        onDamaged.Invoke();
        Check();
        CheckHealth checkHealth = GetComponent<CheckHealth>();
        lastAttacker = attacker;
        if(checkHealth != null)
        {
            checkHealth.Check();
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        onHealed.Invoke();
        Check();
        CheckHealth checkHealth = GetComponent<CheckHealth>();
        if (checkHealth != null)
        {
            checkHealth.Check();
        }
    }
    void Check()
    {
        if(currentHealth <= 0)
        {
            Death();

        }
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    void Death()
    {
        if(dead != true)
        {
            onDeath.Invoke();
            KeaTask task = GetComponentInParent<KeaTask>();
            if (task != null)
            {
                task.Progress();
            }
            if (destroyOnDeath)
            {
                Destroy(gameObject);
            }//
            dead = true;
        }
    }
    public void SetHealth(int value)
    {
        maxHealth = value;
        Heal(value);
    }
}
