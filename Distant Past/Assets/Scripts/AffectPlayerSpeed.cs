using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AffectPlayerSpeed : MonoBehaviour
{
    FirstPersonMovement playerMovement;
    // Start is called before the first frame update
    void Start()
    {
        playerMovement = FindObjectOfType<FirstPersonMovement>();
    }

    public void SetSpeed(float value)
    {
        if(playerMovement == null)
        {
            playerMovement = FindObjectOfType<FirstPersonMovement>();
        }
        playerMovement.walkSpeed = value;
        playerMovement.sprintSpeed = value;
        playerMovement.trueSpeed = value;
        playerMovement.jumpHeight = value;
    }
    public void ResetSpeed()
    {
        if (playerMovement == null)
        {
            playerMovement = FindObjectOfType<FirstPersonMovement>();
        }
        playerMovement.walkSpeed = playerMovement.originalWalkSpeed;
        playerMovement.sprintSpeed = playerMovement.originalSprintSpeed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            playerMovement.trueSpeed = playerMovement.sprintSpeed;
        }
        else
        {
            playerMovement.trueSpeed = playerMovement.walkSpeed;
        }

        playerMovement.jumpHeight = playerMovement.originalJumpHeight;
    }
}
