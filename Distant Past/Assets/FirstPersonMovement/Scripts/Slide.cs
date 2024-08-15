using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slide : MonoBehaviour
{
    FirstPersonMovement movement;
    public float moveDistance = 3f;   
    public float lerpDuration = 1f;   
    private Vector3 targetPosition;  
    private float startTime;          
    [SerializeField] List< Transform> hitter;
    [SerializeField] LayerMask notPlayer;
    bool sliding;
    void Awake()
    {
        movement = GetComponent<FirstPersonMovement>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && sliding == false)
        {
            if (movement.GetSprinting() == true && movement.GetHeightState() == 0 
                && movement.swimming != true && movement.grounded == true)
            {
                movement.SetHeight(1);
                movement.enabled = false;
                StartCoroutine(LerpToTarget());
            }

        }
    }

    IEnumerator LerpToTarget()
    {
        sliding = true;
        RaycastHit hit;
        for (int i = 0; i < hitter.Count; i++)
        {
            if(Input.GetAxis("Horizontal") > 0)
            if (Physics.Raycast(hitter[i].position, hitter[i].forward, out hit, .1f, notPlayer))
            {
                
                Debug.Log("Collision detected: " + hit.collider.gameObject.name);


                movement.enabled = true;
                movement.SetHeight(0);
                sliding = false;
                yield break;
            }
        }

        float startTime = Time.time;
        Vector3 startPosition = transform.position;
        targetPosition = transform.position + transform.forward * moveDistance;

        Transform face = GetComponentInChildren<Camera>().transform;
        while (Time.time - startTime < lerpDuration)
        {
            float journeyFraction = (Time.time - startTime) / lerpDuration;
            transform.position = Vector3.Lerp(startPosition, targetPosition, journeyFraction);

            for (int i = 0; i < hitter.Count; i++)
            {
                if (Physics.Raycast(hitter[i].position, hitter[i].forward, out hit, .1f, notPlayer))
                {
                    movement.enabled = true;
                    movement.SetHeight(0);
                    sliding = false;
                    yield break;
                }
            }

            yield return null; 
        }

        transform.position = targetPosition;
        movement.enabled = true;
        movement.SetHeight(0);
        sliding = false;
    }
}
