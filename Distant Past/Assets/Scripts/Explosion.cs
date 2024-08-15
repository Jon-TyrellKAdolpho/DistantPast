using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public Transform attacker;
    List<Health> inRange = new List<Health>();
    [SerializeField] int damage;
    [SerializeField] float lifeSpan;
    [SerializeField] float explosionRadius; // Add a field for the radius of the explosion
    [SerializeField] int maxTargets = 3;
    bool exploded;

    private void Update()
    {
        if (!exploded)
        {
            // Use Physics.OverlapSphere here to find all colliders within the explosion radius at the start

            Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);
            int targetsHit = 0; // Keep track of the number of targets hit

            foreach (Collider hitCollider in hitColliders)
            {
                // Check if the collider belongs to a player
                KeaPlayer player = hitCollider.GetComponent<KeaPlayer>();
                if (player != null && !inRange.Contains(player.GetComponent<Health>()) && player.GetComponent<Health>().currentHealth > 0)
                {
                    Debug.Log(player.name);
                    player.GetComponent<Health>().TakeDamage(damage,attacker);
                    inRange.Add(player.GetComponent<Health>());
                    targetsHit++;
                }

                EnemyAI enemy = hitCollider.GetComponent<EnemyAI>();
                if (enemy != null && !inRange.Contains(enemy.GetComponent<Health>()) && enemy.GetComponent<Health>().currentHealth > 0)
                {
                    Debug.Log(enemy.name);
                    enemy.GetComponent<Health>().TakeDamage(damage,attacker);
                    inRange.Add(enemy.GetComponent<Health>());
                    targetsHit++;
                }


                // Break the loop if the maximum number of targets is reached
                if (targetsHit >= maxTargets)
                    break;
            }
            exploded = true;
        }


        lifeSpan -= Time.deltaTime;
        if (lifeSpan <= 0)
        {
            Destroy(gameObject);
        }
    }

}
