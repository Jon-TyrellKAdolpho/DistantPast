using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class CheckHealth : MonoBehaviour
{
    Health health;
    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] List<HealthCheck> healthChecks;
    [SerializeField] bool isDone = true;
    private void Start()
    {
        health = GetComponent<Health>();
    }
    public void Check()
    {
        for (int i = 0; i < healthChecks.Count; i++)
        {
            if (health.currentHealth > healthChecks[i].marker.x && health.currentHealth <= healthChecks[i].marker.y)
            {
                if(isDone == true)
                {
                    if (healthChecks[i].done != true)
                    {
                        healthChecks[i].m_Event.Invoke();
                        if (meshRenderer != null)
                        {
                            meshRenderer.sharedMaterial = healthChecks[i].material;
                        }
                        healthChecks[i].done = true;
                    }
                }
                else
                {
                    healthChecks[i].m_Event.Invoke();
                    if (meshRenderer != null)
                    {
                        meshRenderer.sharedMaterial = healthChecks[i].material;
                    }
                    healthChecks[i].done = true;
                }


           
            }

        }
    }
}
