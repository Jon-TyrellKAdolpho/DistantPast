using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class NetworkPlayerLook : NetworkBehaviour
{
    [SerializeField] int targetFrameRate = 30;
    [SerializeField] float multiplier;
    public float mouseSensitivity = 100f;
    public Transform playerBody;
    public float xRotation = 0f;
    bool set;
    public float mouseX;
    public float mouseY;
    [SerializeField] int frameCheckInterval = 5;
    int frameCount;
    [SerializeField] GameObject swimVFX;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        xRotation = 0f;
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        Application.targetFrameRate = targetFrameRate;
        mouseSensitivity = ((targetFrameRate * 3.33f) * multiplier);
        StartCoroutine(SetSensitivity());
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (!IsOwner) return;
        frameCount++;
        if (Input.GetAxis("Mouse X") == 0 && Input.GetAxis("Mouse Y") == 0)
        {
            set = true;
        }
        if (set)
        {
            mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

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
    private IEnumerator SetSensitivity()
    {
        while (true)
        {
            frameCount = 0;
            yield return new WaitForSeconds(frameCheckInterval);
            float averageFps = frameCount / 1.0f;
            mouseSensitivity = (Mathf.Round(averageFps / 1.5f) * multiplier);
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
}
