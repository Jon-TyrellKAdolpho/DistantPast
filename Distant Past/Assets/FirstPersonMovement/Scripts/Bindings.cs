using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bindings : MonoBehaviour
{
    [SerializeField] FirstPersonMovement movement;
    [SerializeField] GunManager gunManager;

    [SerializeField] List<TMP_Dropdown> dropDowns;
    [SerializeField] List<KeyCode> keyCodes;
    [SerializeField] List<int> bindingNumbers;

    private bool isProgrammaticChange = false;
  
    private void Awake()
    {
        for (int i = 0; i < dropDowns.Count; i++)
        {
            Debug.Log(dropDowns[i].name + bindingNumbers[i]);
            SelectDropdownOption(dropDowns[i], bindingNumbers[i]);
        }

    }

    void SelectDropdownOption(TMP_Dropdown dropdown, int index)
    {
        if (dropdown != null && index >= 0 && index < dropdown.options.Count)
        {
            isProgrammaticChange = true;
            dropdown.value = index;
            isProgrammaticChange = false;
        }
        else
        {
            Debug.LogError("Invalid index or dropdown not assigned.");
        }
    }



    public void SetJumpKey(int value)
    {
        for (int i = 0; i < bindingNumbers.Count; i++)
        {
            if (i == 0)
            {
                continue;
            }
            if (bindingNumbers[i] == value)
            {
                SelectDropdownOption(dropDowns[0], bindingNumbers[0]);
                return;
            }
        }

        KeyCode code = keyCodes[0];
        if (keyMap.TryGetValue(value, out code))
        {
            movement.jumpKey = code;
        }
    }
    public void SetSprintKey(int value)
    {
        for (int i = 0; i < bindingNumbers.Count; i++)
        {
            if (i == 1)
            {
                continue;
            }
            if (bindingNumbers[i] == value)
            {
                SelectDropdownOption(dropDowns[1], bindingNumbers[1]);
                return;
            }
        }

        KeyCode code = keyCodes[1];
        if (keyMap.TryGetValue(value, out code))
        {
            movement.sprintKey = code;
        }
    }
    public void SetCrouchKey(int value)
    {
        for (int i = 0; i < bindingNumbers.Count; i++)
        {
            if (i == 2)
            {
                continue;
            }
            if (bindingNumbers[i] == value)
            {
                SelectDropdownOption(dropDowns[2], bindingNumbers[2]);
                return;
            }
        }

        KeyCode code = keyCodes[2];
        if (keyMap.TryGetValue(value, out code))
        {
            movement.crouchKey = code;
        }
    }
    public void SetShootKey(int value)
    {
        for (int i = 0; i < bindingNumbers.Count; i++)
        {
            if (i == 3)
            {
                continue;
            }
            if (bindingNumbers[i] == value)
            {
                SelectDropdownOption(dropDowns[3], bindingNumbers[3]);
                return;
            }
        }

        KeyCode code = keyCodes[3];
        if (keyMap.TryGetValue(value, out code))
        {
            gunManager.shootKey = code;
        }
    }
    public void SetCycleKey(int value)
    {
        for (int i = 0; i < bindingNumbers.Count; i++)
        {
            if (i == 4)
            {
                continue;
            }
            if (bindingNumbers[i] == value)
            {
                SelectDropdownOption(dropDowns[4], bindingNumbers[4]);
                return;
            }
        }

        KeyCode code = keyCodes[4];
        if (keyMap.TryGetValue(value, out code))
        {
            gunManager.cycleKey = code;
        }
    }

    public void SetForwardKey(int value)
    {
        for (int i = 0; i < bindingNumbers.Count; i++)
        {
            if (i == 5)
            {
                continue;
            }
            if (bindingNumbers[i] == value)
            {
                SelectDropdownOption(dropDowns[5], bindingNumbers[5]);
                return;
            }
        }

        KeyCode code = keyCodes[5];
        if (keyMap.TryGetValue(value, out code))
        {
            movement.forwardKey = code;
        }
    }

    public void SetBackwardKey(int value)
    {
        for (int i = 0; i < bindingNumbers.Count; i++)
        {
            if (i == 6)
            {
                continue;
            }
            if (bindingNumbers[i] == value)
            {
                SelectDropdownOption(dropDowns[6], bindingNumbers[6]);
                return;
            }
        }

        KeyCode code = keyCodes[6];
        if (keyMap.TryGetValue(value, out code))
        {
            movement.backwardKey = code;
        }
    }
    public void SetLeftKey(int value)
    {
        for (int i = 0; i < bindingNumbers.Count; i++)
        {
            if (i == 7)
            {
                continue;
            }
            if (bindingNumbers[i] == value)
            {
                SelectDropdownOption(dropDowns[7], bindingNumbers[7]);
                return;
            }
        }

        KeyCode code = keyCodes[7];
        if (keyMap.TryGetValue(value, out code))
        {
            movement.leftKey = code;
        }
    }
    public void SetRightKey(int value)
    {
        for (int i = 0; i < bindingNumbers.Count; i++)
        {
            if (i == 8)
            {
                continue;
            }
            if (bindingNumbers[i] == value)
            {
                SelectDropdownOption(dropDowns[8], bindingNumbers[8]);
                return;
            }
        }

        KeyCode code = keyCodes[8];
        if (keyMap.TryGetValue(value, out code))
        {
            movement.rightKey = code;
        }
    }
    private void Update()
    {
        for (int i = 0; i < dropDowns.Count; i++)
        {
            if (!isProgrammaticChange)
            {
                int value = dropDowns[i].value;
                switch (i)
                {
                    case 0:
                        SetJumpKey(value);
                        break;
                    case 1:
                        SetSprintKey(value);
                        break;
                    case 2:
                        SetCrouchKey(value);
                        break;
                    case 3:
                        SetShootKey(value);
                        break;
                    case 4:
                        SetCycleKey(value);
                        break;
                    case 5:
                        SetForwardKey(value);
                        break;
                    case 6:
                        SetBackwardKey(value);
                        break;
                    case 7:
                        SetLeftKey(value);
                        break;
                    case 8:
                        SetRightKey(value);
                        break;
                }
            }
        }
    }

    private Dictionary<int, KeyCode> keyMap = new Dictionary<int, KeyCode>
    {
        { 0, KeyCode.Tab },
        { 1, KeyCode.LeftShift },
        { 2, KeyCode.LeftControl },
        { 3, KeyCode.LeftAlt },
        { 4, KeyCode.Backspace },
        { 5, KeyCode.Backslash },
        { 6, KeyCode.Return },
        { 7, KeyCode.RightShift },
        { 8, KeyCode.RightAlt },
        { 9, KeyCode.RightControl },
        { 10, KeyCode.Space },
        { 11, KeyCode.Mouse0 },
        { 12, KeyCode.Mouse1 },
        { 13, KeyCode.Mouse2 },
        { 14, KeyCode.Mouse3 },
        { 15, KeyCode.Mouse4 },
        { 16, KeyCode.Mouse5 },
        { 17, KeyCode.Mouse6 },
        { 18, KeyCode.Alpha0 },
        { 19, KeyCode.Alpha1 },
        { 20, KeyCode.Alpha2 },
        { 21, KeyCode.Alpha3 },
        { 22, KeyCode.Alpha4 },
        { 23, KeyCode.Alpha5 },
        { 24, KeyCode.Alpha6 },
        { 25, KeyCode.Alpha7 },
        { 26, KeyCode.Alpha8 },
        { 27, KeyCode.Alpha9 },
        { 28, KeyCode.A },
        { 29, KeyCode.B },
        { 30, KeyCode.C },
        { 31, KeyCode.D },
        { 32, KeyCode.E },
        { 33, KeyCode.F },
        { 34, KeyCode.G },
        { 35, KeyCode.H },
        { 36, KeyCode.I },
        { 37, KeyCode.J },
        { 38, KeyCode.K },
        { 39, KeyCode.L },
        { 40, KeyCode.M },
        { 41, KeyCode.N },
        { 42, KeyCode.O },
        { 43, KeyCode.P },
        { 44, KeyCode.Q },
        { 45, KeyCode.R },
        { 46, KeyCode.S },
        { 47, KeyCode.T },
        { 48, KeyCode.U },
        { 49, KeyCode.V },
        { 50, KeyCode.W },
        { 51, KeyCode.X },
        { 52, KeyCode.Y },
        { 53, KeyCode.Z },
        { 54, KeyCode.UpArrow },
        { 55, KeyCode.DownArrow },
        { 56, KeyCode.LeftArrow },
        { 57, KeyCode.RightArrow }
    };
}
