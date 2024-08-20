using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
   // [SerializeField] Transform attacker;
    [SerializeField] GameObject prefab;
    int damage;
    ParticleSystem partSystem;
    private void Start()
    {
        partSystem = GetComponent<ParticleSystem>();
    }
    public int GetDamage()
    {
        int returnDamage = damage;
        return returnDamage;
    }
    public void SetDamage(int value)
    {
        damage = value;
    }

    private void OnParticleCollision(GameObject other)
    {
        Health health = other.GetComponent<Health>();
        if(health != null)
        {
            health.TakeDamage(damage,null);
        }
        ParticleSystem particleSystem = GetComponent<ParticleSystem>();


        List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
        int collisionCount = particleSystem.GetCollisionEvents(other, collisionEvents);

        for (int i = 0; i < collisionCount; i++)
        {
            Vector3 hitPoint = collisionEvents[i].intersection;
            Vector3 hitNormal = collisionEvents[i].normal;
            Instantiate(prefab, hitPoint, Quaternion.LookRotation(hitNormal));
         //   Debug.Log("Hit at " + hitPoint.ToString());
        }
    }
    
   
}
