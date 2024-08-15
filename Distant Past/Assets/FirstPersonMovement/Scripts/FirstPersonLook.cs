using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class FirstPersonLook : MonoBehaviour
{
    [SerializeField] int targetFrameRate = 30;
    [SerializeField] float multiplier;
    [SerializeField] float mouseSensitivity = 100f;
    public Transform playerBody;
    public float xRotation = 0f;
    bool set;
    public float mouseX;
    public float mouseY;
    [SerializeField] float frameCheckInterval = 5;
    int frameCount;
    [SerializeField] GameObject swimVFX;


    private float updateInterval = 0.5f; // Update interval in seconds
    private float timeSinceLastUpdate = 0.0f;
    [SerializeField] PauseHandler pauseHandler;
    [SerializeField] float frameRate;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        xRotation = 0f;
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
       // Application.targetFrameRate = targetFrameRate;
        mouseSensitivity = ((targetFrameRate * 3.33f) * multiplier);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        StartCoroutine(CheckFramerate());
        StartCoroutine(SetSensitivity());
    }

    void Update()
    {
        
        if(PauseHandler.instance.isPaused != true)
        {
            frameCount++;
            if (Input.GetAxis("Mouse X") == 0 && Input.GetAxis("Mouse Y") == 0)
            {
                set = true;
            }
            if (set)
            {
                frameRate = 1f / Time.deltaTime;
                mouseX = Input.GetAxis("Mouse X") * (mouseSensitivity/ frameRate);// mouseSensitivity; //GetAdjustedSensitivity();// * Time.fixedDeltaTime;
                mouseY = Input.GetAxis("Mouse Y") * (mouseSensitivity/ frameRate);// mouseSensitivity;// GetAdjustedSensitivity();// * Time.fixedDeltaTime;

                xRotation -= mouseY;
                if (xRotation > 90f)
                {
                    xRotation = 90f;
                }
                else if (xRotation < -90f)
                {
                    xRotation = -90f;
                }

                transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
                playerBody.Rotate(Vector3.up * mouseX);
            }

        }


    }

    private IEnumerator SetSensitivity()
    {
        while (true)
        {
            if(pauseHandler.isPaused != true)
            {
                frameCount = 0;
                yield return new WaitForSeconds(frameCheckInterval);
                float averageFps = frameCount / 1.0f;
                mouseSensitivity = (Mathf.Round(averageFps / 1.5f) * multiplier);
            }

          //  Debug.Log("Average FPS over the last second: " + averageFps);
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
