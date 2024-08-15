using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyMelee : MonoBehaviour
{
 
    [SerializeField] Transform attacker;
    EnemyAI enemyAI;
    Health playerHealth;
    [SerializeField] float attackInterval = 2f;
    [SerializeField] int damage = 10;

    float attackTimer;
    [SerializeField] GameObject attackSound;
    [SerializeField] UnityEvent onAttack;

    void Start()
    {
        enemyAI = GetComponent<EnemyAI>();
        attackTimer = attackInterval;

    }

    void Update()
    {
        if(playerHealth == null)
        {
            if(FindObjectOfType<FirstPersonMovement>() != null)
            {
                playerHealth = FindObjectOfType<FirstPersonMovement>(true).GetComponent<Health>();
            }
        }

        if (enemyAI.inRange)
        {
            if (attackTimer <= 0f)
            {
                AttackPlayer();
                attackTimer = attackInterval;
            }
        }
        if(attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
        }
    }

    void AttackPlayer()
    {
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage, attacker);
            GameObject attacksound = Instantiate(attackSound);
            onAttack.Invoke();
        }
    }
}