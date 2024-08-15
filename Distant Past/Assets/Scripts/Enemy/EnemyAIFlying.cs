using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
public class EnemyAIFlying : MonoBehaviour
{
    Transform player;
    FlyPoint currentFlyPoint;
    public float speed = 5f; // Speed at which the enemy moves
    public float flyPointRadius = 1f; // Radius to consider the enemy has reached the fly point
    [SerializeField] Vector2 findNewSpotTime;
    [HideInInspector]
    public bool arrived;
    [SerializeField] UnityEvent onArrive;
    [SerializeField] bool headTowards = true;
    [SerializeField] float rangeCheck = 1;
    [SerializeField] LayerMask hitMask;
    bool reachedFlyZone;
    Transform entry;
    float entryDistance;
    float trueTime;
    [SerializeField] AudioSource flyingSound;
    [SerializeField] int flyPointAssign;
    List<FlyPoint> flyPoints = new List<FlyPoint>();
    private void Start()
    {
        entryDistance = Random.Range(0, 5);
        flyPoints = FindObjectsOfType<FlyPoint>().ToList();
        for (int i = flyPoints.Count - 1; i >= 0; i--)
        {
            if (flyPoints[i].flyPointAssign != flyPointAssign)
            {
                flyPoints.RemoveAt(i);
            }
        }
    }
    void Update()
    {
        if (player == null)
        {
            FirstPersonMovement fps = FindObjectOfType<FirstPersonMovement>(true);
            if (fps != null)
            {
                player = fps.transform;
            }
        }

        if (!reachedFlyZone)
        {
            if(entry == null)
            {
                entry = FindClosestEntry();
            }
            if(Vector3.Distance(transform.position, entry.position) > entryDistance)
            {
                MoveTowardsTarget(entry);
                
            }
            else
            {
                reachedFlyZone = true;
            }
        }
        else
        {
            if (player != null)
            {
                if (trueTime <= 0)
                {
                    PickNewPoint();
                    trueTime = Random.Range(findNewSpotTime.x, findNewSpotTime.y);
                }
                else
                {
                    trueTime -= Time.deltaTime;
                }
                if (currentFlyPoint == null)
                {
                    PickNewPoint();
                }
                if (currentFlyPoint.occupant != this)
                {
                    currentFlyPoint.occupant = this;
                }

                if (Vector3.Distance(transform.position, currentFlyPoint.transform.position) > .1f)
                {
                    if (arrived)
                    {
                        arrived = false;
                    }
                    MoveTowardsTarget(currentFlyPoint.transform);
                }
                else
                {
                    if (arrived != true)
                    {
                        if (CheckForObstacles())
                        {
                            flyingSound.Stop();
                            Debug.Log("HIT!");
                        }
                        else
                        {
                            flyingSound.Play();
                            Debug.Log("None");
                        }
                        onArrive.Invoke();
                        arrived = true;
                    }
                }
    

            }
        }

    }
    public void SetHeadTowards(bool value)
    {
        headTowards = value;
    }
    public void TryPickNewPoint()
    {
        int go = Random.Range(0, 2);

        // Call the appropriate function based on the coin flip result
        if (go == 0)
        {
            return;
        }
        else
        {
            PickNewPoint();
        }
    }
    public void ClearOccupant()
    {
        currentFlyPoint.occupant = null;
    }
    public void PickNewPoint()
    {
        if(currentFlyPoint != null)
        {
            currentFlyPoint.occupant = null;
        }

        if (headTowards)
        {
            currentFlyPoint = FindNearestAvailableFlyPoint();
        }
        else
        {
            currentFlyPoint = FindFurthestAvailableFlyPoint();
        }

    }
    Transform FindClosestEntry()
    {
        EntryPoint[] entryPoints = FindObjectsOfType<EntryPoint>();
        Transform nearestEntry = null;
        float nearestDistance = 500f;


        foreach (EntryPoint entryPoint in entryPoints)
        {
            float distance = Vector3.Distance(transform.position, entryPoint.transform.position);
            if (distance < nearestDistance)
            {
                nearestEntry = entryPoint.transform;
                nearestDistance = distance;
            }
        }

        return nearestEntry.transform;

    }
    
    FlyPoint FindNearestAvailableFlyPoint()
    {
        List<FlyPoint> availableFlyPoints = new List<FlyPoint>();

        foreach (FlyPoint flyPoint in flyPoints)
        {
            if (flyPoint.occupant == null)
            {
                availableFlyPoints.Add(flyPoint);
            }
        }

        if (availableFlyPoints.Count == 0)
        {
            return null;
        }

        // Sort the available fly points by distance to the player
        availableFlyPoints.Sort((a, b) =>
            Vector3.Distance(player.position, a.transform.position).CompareTo(
            Vector3.Distance(player.position, b.transform.position)));

        // Get the closest three fly points
        int count = Mathf.Min(3, availableFlyPoints.Count);
        List<FlyPoint> closestFlyPoints = availableFlyPoints.GetRange(0, count);

        // Pick one randomly from the closest three fly points
        FlyPoint chosenFlyPoint = closestFlyPoints[Random.Range(0, closestFlyPoints.Count)];

        return chosenFlyPoint;
    }

    FlyPoint FindFurthestAvailableFlyPoint()
    {
        List<FlyPoint> availableFlyPoints = new List<FlyPoint>();

        foreach (FlyPoint flyPoint in flyPoints)
        {
            if (flyPoint.occupant == null)
            {
                availableFlyPoints.Add(flyPoint);
            }
        }

        if (availableFlyPoints.Count == 0)
        {
            return null;
        }

        // Sort the available fly points by distance to the player in descending order
        availableFlyPoints.Sort((a, b) =>
            Vector3.Distance(player.position, b.transform.position).CompareTo(
            Vector3.Distance(player.position, a.transform.position)));

        // Get the furthest three fly points
        int count = Mathf.Min(3, availableFlyPoints.Count);
        List<FlyPoint> furthestFlyPoints = availableFlyPoints.GetRange(0, count);

        // Pick one randomly from the furthest three fly points
        FlyPoint chosenFlyPoint = furthestFlyPoints[Random.Range(0, furthestFlyPoints.Count)];

        return chosenFlyPoint;
    }

    void MoveTowardsTarget(Transform target)
    {
        if (!flyingSound.isPlaying)
        {
            Debug.Log("Play!");
            flyingSound.Play();
        }
        // Calculate the direction and the distance to the target
        Vector3 direction = target.position - transform.position;
        float distance = direction.magnitude;

        // Normalize the direction vector and determine the interpolation factor
        direction.Normalize();
        float step = speed * Time.deltaTime; // This is the maximum distance the enemy can move in one frame

        // Check if the distance to the target is less than or equal to the step size
        if (distance <= step)
        {
            // If the target is within one step distance, set the position to the target's position
            transform.position = target.position;
        }
        else
        {
            // Otherwise, perform Lerp interpolation
            transform.position = Vector3.Lerp(transform.position, target.position, step / distance);
        }
    }
    public bool CheckForObstacles()
    {
        Vector3[] directions = new Vector3[]
        {
            Vector3.forward,
            Vector3.back,
            Vector3.left,
            Vector3.right,
            Vector3.up,
            Vector3.down
        };

        foreach (Vector3 direction in directions)
        {
            if (Physics.Raycast(transform.position, direction, out RaycastHit hit, rangeCheck, hitMask))
            {
                if (hit.collider != GetComponent<Collider>())
                {
                    return true;
                }
            }
        }

        return false;
    }
}