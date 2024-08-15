using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    [SerializeField] EnemyAI enemyAI;
    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] List<Material> idle;
    [SerializeField] List<Material> walkAnimation;
    [SerializeField] List<Material> attackAnimation;
    int status;
    [SerializeField] float attackTimer;
    [SerializeField] float walkTime;
    float trueTime;
    bool left;
    float trueWalkTime;
    // Start is called before the first frame update
    void Start()
    {
        // Start the coroutine with a 5-second timer
    }

    // Coroutine timer method
    public void AttackAnimation()
    {
        trueTime = attackTimer;
        meshRenderer.material = attackAnimation[status];
     
    }
    public void SetStatus(int value)
    {
        status = value;
        trueWalkTime = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if(trueTime > 0)
        {
            trueTime -= Time.deltaTime;
        }
        else
        {
            if(enemyAI.inRange != true)
            {
                if (trueWalkTime <= 0)
                {
                    left = !left;

                    if (left)
                    {
                        meshRenderer.material = walkAnimation[status + status];
                    }
                    else
                    {
                        meshRenderer.material = walkAnimation[status + status + 1];
                    }
                    trueWalkTime = walkTime;

                }
                else
                {

                }
                trueWalkTime -= Time.deltaTime;
            }
            else
            {
                if(meshRenderer.material != idle[status])
                {
                    meshRenderer.material = idle[status];
                }
            }

        }
    }
}
