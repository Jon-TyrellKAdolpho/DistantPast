using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    EnemyAIFlying enemyAIFlying;
    EnemyAI enemyAI;
    Transform playerTransform;
    [SerializeField] Transform projectileSpawnPoint;
    [SerializeField] Rigidbody projectilePrefab;
    [SerializeField] float shootingInterval = 2f;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float rise;
    [SerializeField] GameObject shootSound;

    private float shootTimer;

    private void Start()
    {
        enemyAI = GetComponent<EnemyAI>();
        enemyAIFlying = GetComponent<EnemyAIFlying>();
        shootTimer = shootingInterval;
    }

    private void Update()
    {
        if(playerTransform == null)
        {
            if(FindObjectOfType<FirstPersonMovement>() != null)
            {
                playerTransform = FindObjectOfType<FirstPersonMovement>(true).GetComponentInChildren<Camera>().transform;
            }
        }
        if(enemyAI != null)
        {
            if (enemyAI.inRange)
            {
                shootTimer -= Time.deltaTime;

                if (shootTimer <= 0f)
                {
                    Shoot();
                    shootTimer = shootingInterval;
                }
            }
        }
        if(enemyAIFlying != null)
        {
            if (enemyAIFlying.arrived)
            {
                shootTimer -= Time.deltaTime;

                if (shootTimer <= 0f)
                {
                    Shoot();
                    shootTimer = shootingInterval;
                }
            }
        }

    }

    public void SetRise(float value)
    {
        rise = value;
    }
    void Shoot()
    {
        if (projectilePrefab && projectileSpawnPoint && playerTransform)
        {
            Rigidbody projectileInstance = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
            Vector3 directionToPlayer = (playerTransform.position - projectileSpawnPoint.position).normalized;

            // Adjust the direction to include the rise
            directionToPlayer += Vector3.up * rise;

            // Normalize the direction again
            directionToPlayer.Normalize();

            projectileInstance.velocity = directionToPlayer * projectileSpeed;
            projectileInstance.GetComponent<Projectile>().attacker = transform;
            Instantiate(shootSound);
        }
    }
}