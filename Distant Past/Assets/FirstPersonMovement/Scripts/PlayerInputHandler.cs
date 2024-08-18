using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;
public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] FirstPersonMovement fpMovement;
    [SerializeField] FirstPersonLook fpLook;
    [SerializeField] GunManager gunManager;

    public static List<PlayerInputHandler> playerInputHandlers;
    PlayerInput playerInput;


    // Start is called before the first frame update
    void Start()
    {
        if(playerInputHandlers == null)
        {
            playerInputHandlers = new List<PlayerInputHandler>();

        }
        playerInputHandlers.Add(this);
        playerInput = GetComponent<PlayerInput>();
        var fpMovements = FindObjectsOfType<FirstPersonMovement>();
        var fpLooks = FindObjectsOfType<FirstPersonLook>();
        var gunManagers = FindObjectsOfType<GunManager>();

        var index = playerInput.playerIndex;
        int value = playerInputHandlers.IndexOf(this);
        fpMovement = fpMovements[value];
        fpLook = fpLooks[value];
        gunManager = gunManagers[value];
    }


    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            gunManager.TryShoot();
        }
        if (context.canceled)
        {
            gunManager.TryStopShoot();
        }
    }
    public void OnAim(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            gunManager.Aim();
        }
    }
    public void OnCycleWeapon(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            gunManager.CycleWeapon();
        }
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            fpMovement.Jump();
        }
    }
    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            fpMovement.SetSprint(1);
        }
        if (context.canceled)
        {
            fpMovement.SetSprint(0);
        }
    }
    public void OnCrouch(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            fpMovement.ChangeHeight();
        }
    }
    public void Horizontal(CallbackContext context)
    {
        if(fpMovement != null)
            fpMovement.SetHorizontal(context.ReadValue<float>());
    }
    public void Vertical(CallbackContext context)
    {
        if(fpMovement != null)
            fpMovement.SetVertical(context.ReadValue<float>());
    }

    public void LookX(CallbackContext context)
    {
        if (fpLook != null)
            fpLook.SetMouseX(context.ReadValue<float>());
    }

    public void LookY(CallbackContext context)
    {
        if (fpLook != null)
            fpLook.SetMouseY(context.ReadValue<float>());
    }
}
