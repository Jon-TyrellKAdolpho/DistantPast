using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
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
    bool ceiled;
    Transform lookTransform;
    public bool swimming;
   // [HideInInspector]
    public bool grounded;
    public bool sprinting;
    bool heightIncrease;
    public int heightState;
    [SerializeField] List<Vector4> heightSettings;
    [SerializeField] GameObject jumpSound;

    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.LeftControl;

    public KeyCode forwardKey = KeyCode.W;
    public KeyCode backwardKey = KeyCode.S;
    public KeyCode rightKey = KeyCode.D;
    public KeyCode leftKey = KeyCode.A;

    float horizontal;
    public float vertical;


    void Awake()
    {
        originalWalkSpeed = walkSpeed;
        originalSprintSpeed = sprintSpeed;
        originalJumpHeight = jumpHeight;
        trueSpeed = walkSpeed;
        if(swimDivider < 1.1)
        {
            Debug.LogError("Increase swim divider so that it works properly.", gameObject);
        }
        trueGravity = gravity;
        controller = GetComponent<CharacterController>();
        stepOffset = controller.stepOffset;
        lookTransform = GetComponentInChildren<FirstPersonLook>().transform;
        
    }
    float jumpTime;
    bool jumped;

    MovementSound movementSound;
    Collider currentGroundCollider;
    private void Start()
    {
        movementSound = GetComponentInChildren<MovementSound>();
    }
    void Update()
    {
       // RaycastHit groundHit;
      //  grounded = Physics.Raycast(transform.position, -transform.up, out groundHit, .15f + (.15f * heightState), groundMask);
        Collider[] groundColliders = Physics.OverlapSphere(transform.position, 0.125f + (0.15f * heightState), groundMask);
        grounded = groundColliders.Length > 0;

        if(groundColliders.Length > 0)
        {
            if (currentGroundCollider != groundColliders[0])
            {
                GroundType groundtype =  groundColliders[0].GetComponent<GroundType>();
                if (groundtype != null)
                {
                    if (movementSound.GetSoundType() != groundtype.groundType)
                    {
                        movementSound.SetClip(groundtype.groundType);
                    }
                }
                else
                {
                    movementSound.SetClip(0);
                }
                currentGroundCollider = groundColliders[0];
            }
        }


        ceiled = Physics.Raycast(ceilingCheck.position, transform.up, out RaycastHit ceilHit, .5f, ceilingMask);
        if (grounded == true && velocity.y < 0 || swimming == true || ceiled)
        {
            velocity.y = -1f;
            if(jumpTime != 1)
            {
                jumped = false;
                jumpTime = 1;
            }
        }
        if(grounded != true)
        {
            controller.stepOffset = 0;
            if(jumpTime > 0)
            {
                jumpTime -= Time.deltaTime;
            }
        }
        else
        {
            controller.stepOffset = stepOffset;
        }

        vertical = Input.GetKey(forwardKey) ? 1 : 0;
        if(vertical != 1) {vertical = Input.GetKey(backwardKey) ? -1 : 0;}

        horizontal = Input.GetKey(rightKey) ? 1 : 0;
        if (horizontal != 1) {horizontal = Input.GetKey(leftKey) ? -1 : 0;}



        Vector2 coordinates = new Vector2(horizontal, vertical);
        if (swimming != true)
        {
            movement = transform.right * coordinates.x + transform.forward * coordinates.y;
        }
        if(swimming == true)
        {
            movement = transform.right * coordinates.x + lookTransform.forward * coordinates.y;
        }


        if (Input.GetKeyDown(sprintKey))
        {
            trueSpeed = sprintSpeed;
            sprinting = true;
        }
        if (Input.GetKeyUp(sprintKey))
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

        if (Input.GetKeyDown(jumpKey))
        {
            Jump();
        }
        if (Input.GetKeyDown(crouchKey) && swimming != true)
        {
            ChangeHeight();
        }
        if(velocity.y > -25)
        {
            velocity.y += trueGravity * Time.deltaTime;
        }
        if(swimming != true)
        {
            controller.Move(velocity * Time.deltaTime);
        }

    }
    public void Jump()
    {
        if(heightState != 0)
        {
            return;
        }
        RaycastHit hit;
        if (Physics.Raycast(ceilingCheck.position, transform.up, out hit, .25f, ceilingMask))
        {
            return;
        }
        if (jumpTime > 0 && jumped == false)
        {
            GameObject jumpsound = Instantiate(jumpSound);
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * trueGravity);
            jumped = true;
        }
    }
    public void ChangeHeight()
    {
        if(heightState == heightSettings.Count - 1)
        {
            heightIncrease = false;
        }
        if(heightState == 0)
        {
            heightIncrease = true;
        }
        if(heightIncrease == true)
        {
            heightState += 1;
        }
        else
        {
            heightState -= 1;
        }

        RaycastHit hit;
        Vector3 origin = new Vector3(transform.position.x, transform.position.y - .1f, transform.position.z);
        if(Physics.Raycast(origin, transform.up, out hit, heightSettings[0].w, ceilingMask))
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
            if(i == heightState)
            {
                Debug.Log(i);
                SetHeight(i);
            }
        }
    }
    public void SetHeight(int which)
    {
        StartCoroutine(LerpToHeight(heightSettings[which], .1f));
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
        if(trueSpeed == sprintSpeed)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    
}
