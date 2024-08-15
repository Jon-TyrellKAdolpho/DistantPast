using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    [SerializeField] Transform bottom;
    [SerializeField] Transform ladderArea;
    [SerializeField] Transform top;
    Transform subject;

    private void OnTriggerEnter(Collider other)
    {
        if(subject == null)
        {
            if (other.GetComponent<Climber>() != null)
            {
               
                subject = other.gameObject.transform;
                subject.GetComponent<FirstPersonMovement>().enabled = false;
                subject.GetComponent<FirstPersonMovement>().grounded = true;
                if(subject.position.y < bottom.position.y)
                {
                    subject.position = new Vector3(ladderArea.position.x, bottom.position.y + .1f, ladderArea.position.z);
                }
                else if(subject.position.y > top.position.y)
                {
                    subject.position = new Vector3(ladderArea.position.x, top.position.y - .1f, ladderArea.position.z);
                }
                else
                {
                    subject.position = new Vector3(ladderArea.position.x, subject.position.y, ladderArea.position.z);
                }

            }
        }
    }
    float verticalInput;
    private void Update()
    {
        if (subject != null)
        {
            
            if (IsFacingSameDirection(subject))
            {
                verticalInput = Input.GetAxis("Vertical");
            }
            else
            {
                verticalInput = -Input.GetAxis("Vertical");
            }

            Vector3 movement = new Vector3(0.0f, verticalInput, 0.0f) * 2 * Time.deltaTime;
            subject.Translate(movement);

            if(subject.position.y < bottom.position.y )
            {
                subject.GetComponent<FirstPersonMovement>().enabled = true;
                subject.position = bottom.position;
                subject = null;

            }
            else if (subject.position.y > top.position.y)
            {
                subject.GetComponent<FirstPersonMovement>().enabled = true;
                subject.position = top.position;
                subject = null;



            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                subject.GetComponent<FirstPersonMovement>().enabled = true;
                subject.GetComponent<FirstPersonMovement>().Jump();
                subject = null;
            }
        }
        
    }
    bool IsFacingSameDirection(Transform value)
    {
        Vector3 forward1 = value.forward.normalized;
        Vector3 forward2 = ladderArea.forward.normalized;
        float dotProduct = Vector3.Dot(forward1, forward2);
        return dotProduct >= 0;
    }

}
