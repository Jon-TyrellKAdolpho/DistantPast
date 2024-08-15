using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
public class EnemyAI : MonoBehaviour
{
    NavMeshAgent agent;
    Transform player;
    public float chaseDistance = 10f;
    public float stopChaseDistance = 2f;
    public bool inRange = false;
    public UnityEvent onInRange;
    [SerializeField] Vector2 speedRange;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = Random.Range(speedRange.x, speedRange.y);

        
    }
    void Update()
    {
        if(player == null)
        {
            if(FindObjectOfType<FirstPersonMovement>() != null)
            {
                player = FindObjectOfType<FirstPersonMovement>(true).transform;
            }
        }
        if(player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);


            if (distanceToPlayer <= chaseDistance && distanceToPlayer > stopChaseDistance)
            {
                agent.SetDestination(player.position);
                inRange = false;
            }
            else if (distanceToPlayer <= stopChaseDistance)
            {
                if(inRange != true)
                {
                    inRange = true;
                    agent.SetDestination(transform.position);
                    onInRange.Invoke();
                }

            }
            else
            {
                if (inRange)
                {
                    inRange = false;
                }
              


                
            }
        }

    }
}