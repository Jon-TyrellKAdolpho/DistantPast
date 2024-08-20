using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class FirstPersonLook : MonoBehaviour
{
    [SerializeField] int targetFrameRate = 30;
    [SerializeField] float multiplier;
    [SerializeField] float mouseSensitivity = 10f;
    float maxSensitivity;
    float minSensitivity;
    // if the look axis is more than .8f
    [SerializeField] float senseMultiplier = 1.1f;
    // if the axis is less than .125f
    [SerializeField] float senseDivider = .9f;
    public Transform playerBody;
    public float xRotation = 0f;
    bool set;
    public float mouseX;
    public float mouseY;
    [SerializeField] float frameCheckInterval = 5;
    int frameCount;
    [SerializeField] GameObject swimVFX;

    [SerializeField] float aimAssistRadius = 5f;    // Radius around crosshair for aim assist
    [SerializeField] float aimAssistStrength = 0.5f; // Strength of the aim assist adjustment
    [SerializeField] LayerMask targetLayer;

    private float updateInterval = 0.5f; // Update interval in seconds
    private float timeSinceLastUpdate = 0.0f;
    [SerializeField] float frameRate;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        xRotation = 0f;
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
       // Application.targetFrameRate = targetFrameRate;
        mouseSensitivity = ((targetFrameRate * 3.33f) * multiplier);
        maxSensitivity = mouseSensitivity * senseMultiplier;
        minSensitivity = mouseSensitivity * senseDivider;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        StartCoroutine(CheckFramerate());
        StartCoroutine(SetSensitivity());
    }
    public void SetMouseX(float value)
    {
        
        mouseX = value * (mouseSensitivity / frameRate);
    }
    public void SetMouseY(float value)
    {
        mouseY = value * (mouseSensitivity / frameRate);
    }

    void Update()
    {
        frameCount++;
        if (mouseX == 0 && mouseY == 0)
        {
            set = true;
        }
        if (set)
        {
            frameRate = 1f / Time.deltaTime;
            xRotation -= mouseY;
            if (xRotation > 90f)
            {
                xRotation = 90f;
            }
            else if (xRotation < -90f)
            {
                xRotation = -90f;
            }

            
            Vector3 adjustedMouseInput = new Vector3(mouseX, mouseY, 0f);
            Quaternion targetRotation = Quaternion.Euler(xRotation, 0f, 0f);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime * 12f);

            playerBody.Rotate(Vector3.up * adjustedMouseInput.x);
          //  AimAssist();
        }
    }


    void AimAssist()
    {
        RaycastHit hit;

        // Create a ray from the center of the screen
        Ray ray = new Ray(transform.position, transform.forward);

        // If we hit a target within the aim assist radius
        if (Physics.SphereCast(ray, aimAssistRadius, out hit, Mathf.Infinity, targetLayer))
        {
            if(hit.transform != GetComponentInParent<KeaPlayer>().transform)
            {
                LookAtXAxis(transform, hit.transform.position);
                LookAtYAxis(playerBody, hit.transform.position);
            }

 
        }
    }

    void LookAtXAxis(Transform looker, Vector3 targetPosition)
    {
        // Calculate the direction from the current position to the target position
        Vector3 direction = targetPosition - looker.position;

        // Calculate the rotation needed to look at the target
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        // Convert the lookRotation to Euler angles and restrict to X axis
        Vector3 eulerRotation = lookRotation.eulerAngles;
        eulerRotation.y = looker.eulerAngles.y; // Maintain current Y rotation
        eulerRotation.z = looker.eulerAngles.z; // Maintain current Z rotation

        // Create the target rotation
        Quaternion targetRotation = Quaternion.Euler(eulerRotation);

        // Smoothly rotate towards the target rotation
        looker.rotation = Quaternion.Lerp(looker.rotation, targetRotation, Time.deltaTime * aimAssistStrength);
    }

    void LookAtYAxis(Transform looker, Vector3 targetPosition)
    {
        // Calculate the direction from the current position to the target position
        Vector3 direction = targetPosition - looker.position;

        // Calculate the rotation needed to look at the target
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        // Convert the lookRotation to Euler angles and restrict to Y axis
        Vector3 eulerRotation = lookRotation.eulerAngles;
        eulerRotation.x = looker.eulerAngles.x; // Maintain current X rotation
        eulerRotation.z = looker.eulerAngles.z; // Maintain current Z rotation

        // Create the target rotation
        Quaternion targetRotation = Quaternion.Euler(eulerRotation);

        // Smoothly rotate towards the target rotation
        looker.rotation = Quaternion.Lerp(looker.rotation, targetRotation, Time.deltaTime * aimAssistStrength);
    }

    private IEnumerator SetSensitivity()
    {
        while (true)
        {
            frameCount = 0;
            yield return new WaitForSeconds(frameCheckInterval);
            float averageFps = frameCount / 1.0f;
            mouseSensitivity = (Mathf.Round(averageFps / 1.5f) * multiplier);
        }
    }
    public void SetSwimActive(bool value)
    {
        if (value == true)
        {
            swimVFX.SetActive(true);
        }
        else
        {
            swimVFX.SetActive(false);
        }
    }

    private IEnumerator CheckFramerate()
    {
        while (true)
        {
            float deltaTime = 0.0f;
            deltaTime += (Time.deltaTime - deltaTime) * 0.1f;

            timeSinceLastUpdate += Time.deltaTime;

            if (timeSinceLastUpdate > updateInterval)
            {
                float fps = 1.0f / deltaTime;
               // fpsText.text = "FPS: " + Mathf.Round(fps);
                timeSinceLastUpdate = 0.0f;
            }

            yield return null;
        }
    }
    public void SetLookSensitivity(float value)
    {
        multiplier = value;
    }
}
