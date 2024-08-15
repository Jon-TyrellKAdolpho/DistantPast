using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class NetworkPlayerMovement : NetworkBehaviour
{
    [SerializeField] LayerMask ceilingMask;
    [SerializeField] LayerMask groundMask;

    [HideInInspector]
    public float originalWalkSpeed;
    [HideInInspector]
    public float originalSprintSpeed;
    public float walkSpeed;
    public float sprintSpeed;
    // How much should you reduce the speed by if swimming?
    [SerializeField] float swimDivider;
    public float trueSpeed;
    [HideInInspector]
    public float originalJumpHeight;
    public float jumpHeight;
    [SerializeField] float gravity;
    float trueGravity;
    float stepOffset;

    CharacterController controller;
    Vector3 movement;
    Vector3 velocity;

    [SerializeField] Transform ceilingCheck;
    Transform lookTransform;
    public bool swimming;
    // [HideInInspector]
    public bool grounded;
    public bool sprinting;
    bool heightIncrease;
    public int heightState;
    [SerializeField] List<Vector4> heightSettings;

    void Awake()
    {
        originalWalkSpeed = walkSpeed;
        originalSprintSpeed = sprintSpeed;
        originalJumpHeight = jumpHeight;
        trueSpeed = walkSpeed;
        if (swimDivider < 1.1)
        {
            Debug.LogError("Increase swim divider so that it works properly.", gameObject);
        }
        trueGravity = gravity;
        controller = GetComponent<CharacterController>();
        stepOffset = controller.stepOffset;
        lookTransform = GetComponentInChildren<NetworkPlayerLook>().transform;

    }

    void Update()
    {
        if (!IsOwner) return;
        RaycastHit groundHit;
        grounded = Physics.Raycast(transform.position, -transform.up, out groundHit, .15f + (.15f * heightState), groundMask);
        if (grounded == true && velocity.y < 0 || swimming == true)
        {
            velocity.y = -1f;
        }
        if (grounded != true)
        {
            controller.stepOffset = 0;
        }
        else
        {
            controller.stepOffset = stepOffset;
        }


        Vector2 coordinates = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (swimming != true)
        {
            movement = transform.right * coordinates.x + transform.forward * coordinates.y;
        }
        if (swimming == true)
        {
            movement = transform.right * coordinates.x + lookTransform.forward * coordinates.y;
        }


        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            trueSpeed = sprintSpeed;
            sprinting = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            trueSpeed = walkSpeed;
            sprinting = false;
        }


        if (swimming != true)
        {
            controller.Move(movement * Mathf.CeilToInt(trueSpeed / ((heightState * 2) + 1)) * Time.deltaTime);
        }
        else
        {
            controller.Move(movement * Mathf.CeilToInt(trueSpeed / swimDivider) * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        if (Input.GetKeyDown(KeyCode.LeftControl) && swimming != true)
        {
            ChangeHeight();
        }
        if (velocity.y > -10)
        {
            velocity.y += trueGravity * Time.deltaTime;
        }
        if (swimming != true)
        {
            controller.Move(velocity * Time.deltaTime);
        }

    }
    public void Jump()
    {
        RaycastHit hit;
        if (Physics.Raycast(ceilingCheck.position, transform.up, out hit, .25f, ceilingMask))
        {
            return;
        }
        if (grounded == true)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * trueGravity);
        }
    }
    public void ChangeHeight()
    {
        if (heightState == heightSettings.Count - 1)
        {
            heightIncrease = false;
        }
        if (heightState == 0)
        {
            heightIncrease = true;
        }
        if (heightIncrease == true)
        {
            heightState += 1;
        }
        else
        {
            heightState -= 1;
        }

        RaycastHit hit;
        Vector3 origin = new Vector3(transform.position.x, transform.position.y - .1f, transform.position.z);
        if (Physics.Raycast(origin, transform.up, out hit, heightSettings[0].w, ceilingMask))
        {
            if (heightState == 0)
            {
                heightIncrease = !heightIncrease;
                heightState = 2;
            }
            if (hit.distance < heightSettings[2].w && heightState != 2 ||
                hit.distance < heightSettings[1].w && heightIncrease == false)
            {
                heightState = 2;
            }

        }

        for (int i = 0; i < heightSettings.Count; i++)
        {
            if (i == heightState)
            {
                Debug.Log(i);
                SetHeight(i);
            }
        }
    }
    Coroutine heightLerp;
    public void SetHeight(int which)
    {
        heightLerp = StartCoroutine(LerpToHeight(heightSettings[which], .1f));
    }

    IEnumerator LerpToHeight(Vector4 targetHeight, float lerpDuration)
    {
        float startTime = Time.time;
        Vector3 initialLocalPosition = GetComponentInChildren<FirstPersonLook>().transform.localPosition;
        Vector3 initialCenter = controller.center;
        float initialRadius = controller.radius;
        float initialHeight = controller.height;
        CapsuleCollider collider = GetComponent<CapsuleCollider>();

        while (Time.time - startTime < lerpDuration)
        {
            float journeyFraction = (Time.time - startTime) / lerpDuration;

            // Interpolate the height settings.
            GetComponentInChildren<FirstPersonLook>().transform.localPosition =
                Vector3.Lerp(initialLocalPosition, new Vector3(0, targetHeight.x, 0), journeyFraction);
            controller.center = Vector3.Lerp(initialCenter, new Vector3(0, targetHeight.y, 0), journeyFraction);
            controller.radius = Mathf.Lerp(initialRadius, targetHeight.z, journeyFraction);
            controller.height = Mathf.Lerp(initialHeight, targetHeight.w, journeyFraction);

            // Update the collider settings.
            collider.center = controller.center;
            collider.radius = controller.radius + .0625f;
            collider.height = controller.height + .0625f;

            yield return null; // Wait for the next frame.
        }

        // Ensure that the height settings reach the exact target values.
        GetComponentInChildren<FirstPersonLook>().transform.localPosition = new Vector3(0, targetHeight.x, 0);
        controller.center = new Vector3(0, targetHeight.y, 0);
        controller.radius = targetHeight.z;
        controller.height = targetHeight.w;

        // Update the collider settings one last time.
        collider.center = controller.center;
        collider.radius = controller.radius + .0625f;
        collider.height = controller.height + .0625f;
    }

    public void UnderWater()
    {
        swimming = true;
        trueGravity = 0;
        SetHeight(0);
    }
    public void OnLand()
    {
        swimming = false;
        trueGravity = gravity;
    }

    public int GetHeightState()
    {
        return heightState;
    }
    public bool GetSprinting()
    {
        if (trueSpeed == sprintSpeed)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
