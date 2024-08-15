using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parkour : MonoBehaviour
{
    // Otherwise it will relly on closest.
    [SerializeField] bool oneWay;
    Transform target;
    [SerializeField] List<StartEnd> startEnds;
    public float vaultSpeed = 5.0f;  // Speed of the vault.
    Transform startPoint;
    Transform endPoint;
    List<Transform> wayPoints;
    bool canVault = false;
    Coroutine vaulting;
    [SerializeField] GameObject soundDrop;
    private void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            target = other.transform;
            canVault = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            target = other.transform;
            canVault = false;
        }
    }
    private void Update()
    {
        if (canVault == true && vaulting == null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                FirstPersonMovement movement = target.GetComponent<FirstPersonMovement>();
                GameObject drop = Instantiate(soundDrop);
                if (movement.GetHeightState() == 0)
                {
                    vaulting = StartCoroutine(ParkourRoutine(target));
                }

            }
        }
 
    }
    private IEnumerator ParkourRoutine(Transform vaulter)
    {

        Transform closestTransform = null;
        StartEnd which = null;
        float closestDistance = float.MaxValue;
        for (int i = 0; i < startEnds.Count; i++)
        {

            float distance = Vector3.Distance(startEnds[i].startPoint.position, vaulter.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestTransform = startEnds[i].startPoint;
                which = startEnds[i];
            }
            distance = Vector3.Distance(startEnds[i].endPoint.position, vaulter.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestTransform = startEnds[i].endPoint;
                which = startEnds[i];
            }
        }

        if (closestTransform.position != which.endPoint.position)
        {
            startPoint = which.startPoint;
            endPoint = which.endPoint;
            wayPoints = which.wayPoints;
        }
        else
        {
            if(oneWay != true)
            {
                startPoint = which.endPoint;
                endPoint = which.startPoint;
                List<Transform> test = new List<Transform>(which.wayPoints);
                test.Reverse();
                wayPoints = test;
            }
            else
            {
                startPoint = which.startPoint;
                endPoint = which.endPoint;
                wayPoints = which.wayPoints;
            }

        }



        List<Transform> allWaypoints = new List<Transform>();
        allWaypoints.Add(startPoint);
        allWaypoints.AddRange(wayPoints);
        allWaypoints.Add(endPoint);
        for (int i = 0; i < allWaypoints.Count - 1; i++)
        {
            Transform currentWaypoint = allWaypoints[i];
            Transform nextWaypoint = allWaypoints[i + 1];

            float journeyLength = Vector3.Distance(currentWaypoint.position, nextWaypoint.position);
            float startTime = Time.time;
            float distanceCovered = 0f;

            while (distanceCovered < journeyLength)
            {
                float journeyTime = (Time.time - startTime) * vaultSpeed;
                float fractionOfJourney = journeyTime / journeyLength;

                if (fractionOfJourney <= 1.0f)
                {
                    Vector3 targetPosition = Vector3.Lerp(currentWaypoint.position, nextWaypoint.position, fractionOfJourney);
                    target.position = targetPosition;

                    distanceCovered = Vector3.Distance(currentWaypoint.position, target.position);
                }
                else
                {
                    // Waypoint reached.
                    target.position = nextWaypoint.position;
                    vaulting = null;
                    break;
                }

                yield return null;
            }
        }

    }
}
