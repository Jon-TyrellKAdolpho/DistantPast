using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GunManager : MonoBehaviour
{
    public List<GameObject> guns;
    [SerializeField] List<Image> gunImages;
    [SerializeField] int current;
    [SerializeField] GameObject soundPrefab;

    [SerializeField] Camera mainCam;
    [SerializeField] GameObject gunDisplay;
    [SerializeField] SettingsHandler settingsHandler;

    public bool aiming;
    [SerializeField] GameObject crossHair;

    public KeyCode shootKey = KeyCode.Mouse0;
    public KeyCode aimKey = KeyCode.Mouse1;
    public KeyCode cycleKey = KeyCode.R;

    public KeyCode gun1Key = KeyCode.Alpha1;
    public KeyCode gun2Key = KeyCode.Alpha2;
    public KeyCode gun3Key = KeyCode.Alpha3;
    public KeyCode gun4Key = KeyCode.Alpha4;
    public KeyCode gun5Key = KeyCode.Alpha5;
    public KeyCode gun6Key = KeyCode.Alpha6;
    public KeyCode gun7Key = KeyCode.Alpha7;
    public KeyCode gun8Key = KeyCode.Alpha8;

    public Transform target;
    bool first;
    private void Awake()
    {
        target.SetParent(null);
        if (!PlayerPrefs.HasKey(guns[0].name + "Locked"))
        {
            Loadout();
            return;
        }
        for (int i = 0; i < guns.Count; i++)
        {

            if(PlayerPrefs.GetInt(guns[i].name + "Locked") == 0)
            {
                if(guns[i].GetComponent<Gun>() != null)
                {
                    Debug.Log("Unlocked " + guns[i].name);
                    guns[i].GetComponent<Gun>().Unlock();
                }

            }
            else
            {
                if (guns[i].GetComponent<Gun>() != null)
                {
                    guns[i].GetComponent<Gun>().Lock();
                }
            }
        }

        CycleWeapon();
        CycleWeapon();
    }
    public void UnlockGun(string value)
    {
        Debug.Log(value);
        for (int i = 0; i < guns.Count; i++)
        {
            if(guns[i].name == value)
            {
                Debug.Log("Hey!");
                guns[i].GetComponent<Gun>().Unlock();
            }
        }
    }
    public void SelectGun(int which)
    {
        if (current != which && first != false)
        {
            ExitAim();
            Instantiate(soundPrefab, transform.position, Quaternion.identity);
        }
        if (!first)
        {
            first = true;
        }
        if(guns[which].GetComponent<Gun>().locked == true)
        {
            return;
        }
        for (int i = 0; i < guns.Count; i++)
        {
            if(i == which)
            {
                guns[i].SetActive(true);
                Gun gun = guns[i].GetComponent<Gun>();
                current = i;
                gun.gunImage.color = new Vector4(1, 1, 1, 1);
            }
            else
            {
                Gun gun = guns[i].GetComponent<Gun>();
                if(gun != null)
                {
                    guns[i].GetComponent<Gun>().gunImage.color = new Vector4(1, 1, 1, .5f);
                    gun.canShoot = false;
                }

                guns[i].SetActive(false);
            }
        }
    }
    public void CycleWeapon()
    {
        ExitAim();
        if (guns.Count == 0)
        {
            // Handle the case where there are no weapons available
            Debug.Log("No weapons available.");
            return;
        }
        Gun gun = null;
        Cannon cannon = null;
        int maxAttempts = guns.Count; // Set a maximum number of attempts

        for (int attempt = 0; attempt < maxAttempts; attempt++)
        {
            current++;
            if (current >= guns.Count)
            {
                current = 0;
            }

            gun = guns[current].GetComponent<Gun>();
            cannon = guns[current].GetComponent<Cannon>();

            if (gun != null && gun.energy.currentEnergy > 0 && gun.locked == false)
            {
                if (first != false)
                {
                    Instantiate(soundPrefab, transform.position, Quaternion.identity);
                }
                SelectGun(current);
                return;
            }
            else if (cannon != null && cannon.energy.currentEnergy > 0 && cannon.locked == false)
            {
                if (first != false)
                {
                    Instantiate(soundPrefab, transform.position, Quaternion.identity);
                }
                SelectGun(current);
                return;
            }
        }

        current++;
        if (current >= guns.Count)
        {
            current = 0;
        }
        gun = guns[current].GetComponent<Gun>();
        if (gun != null)
        {
            if (gun.locked == true)
            {
                CycleWeapon();
                return;
            }
        }
        cannon = guns[current].GetComponent<Cannon>();
        if (cannon != null)
        {
            if (cannon.locked == true)
            {
                CycleWeapon();
                return;
            }
        }
        if (first != false)
        {
            Instantiate(soundPrefab, transform.position, Quaternion.identity);
        }
        SelectGun(current);
    }
    private void Update()
    {
        if (Input.GetKeyDown(cycleKey))
        {
            CycleWeapon();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if(guns[0].GetComponent<Gun>().locked != true)
            {
                SelectGun(0);
            }

        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (guns[0].GetComponent<Gun>().locked != true)
            {
                SelectGun(1);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (guns[0].GetComponent<Gun>().locked != true)
            {
                SelectGun(2);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (guns[0].GetComponent<Gun>().locked != true)
            {
                SelectGun(3);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            if (guns[0].GetComponent<Gun>().locked != true)
            {
                SelectGun(4);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            if (guns[0].GetComponent<Gun>().locked != true)
            {
                SelectGun(5);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            if (guns[0].GetComponent<Gun>().locked != true)
            {
                SelectGun(6);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            if (guns[0].GetComponent<Gun>().locked != true)
            {
                SelectGun(7);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            if (guns[0].GetComponent<Gun>().locked != true)
            {
                SelectGun(8);
            }
        }

        if (Input.GetKeyDown(aimKey))
        {
            Aim();
        }

    }
    public void Aim()
    {
        aiming = !aiming;
        if (aiming)
        {
            crossHair.SetActive(false);
            gunDisplay.SetActive(false);
            Gun gun = guns[current].GetComponent<Gun>();
            gun.SetAngle(false);
            mainCam.fieldOfView = gun.aimFOV;
            if(gun.retroScope != null)
            {
                if (settingsHandler.retro)
                {
                    gun.retroScope.SetActive(true);
                }
                else
                {
                    gun.hdScope.SetActive(true);
                }
            }
        }
        else
        {
            ExitAim();
        }
    }
    public void ExitAim()
    {
        crossHair.SetActive(true);
        aiming = false;
        gunDisplay.SetActive(true);
        mainCam.fieldOfView = settingsHandler.mainFOV;
        Gun gun = guns[current].GetComponent<Gun>();
        gun.SetAngle(true);
        if(gun.retroScope != null)
        {
            gun.retroScope.SetActive(false);
            gun.hdScope.SetActive(false);
        }


    }
    public void CheckRetro()
    {
        if (aiming)
        {
            Gun gun = guns[current].GetComponent<Gun>();
            if (settingsHandler.retro)
            {
                gun.hdScope.SetActive(false);
                gun.retroScope.SetActive(true);
            }
            else
            {
                gun.hdScope.SetActive(true);
                gun.retroScope.SetActive(false);
            }
        }
    }
    public void Loadout()
    {
        string[] weaponArray = PlayerPrefs.GetString("Loadout").Split(',');

        GunManager gunManager = FindObjectOfType<GunManager>();
        foreach (string weapon in weaponArray)
        {
            Debug.Log("Hey!");
            gunManager.UnlockGun(weapon);
        }
        CycleWeapon();
        CycleWeapon();
    }
}
