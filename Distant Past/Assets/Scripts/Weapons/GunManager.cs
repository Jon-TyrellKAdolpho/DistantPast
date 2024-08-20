using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GunManager : MonoBehaviour
{

    //[HideInInspector]
    KeaPlayer player;
    public List<Gun> guns;
    public List<Image> scopeImages;
    [SerializeField] int current;
    [SerializeField] GameObject soundPrefab;

    public Energy blue;
    public Energy yellow;
    public Energy green;

    public bool aiming;

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

    bool shooting;
    private void Awake()
    {
        player = GetComponentInParent<KeaPlayer>();
        target.SetParent(null);
        guns = new List<Gun>();
        foreach (Transform child in transform)
        {
            if(child.GetComponent<Gun>() != null)
            {
                guns.Add(child.GetComponent<Gun>());
            }
        }

        if (PlayerPrefs.HasKey("Loadout"))
        {
            if(PlayerPrefs.GetString("Loadout") != "")
            {
                Loadout();
                return;
            }
        }
        for (int i = 0; i < guns.Count; i++)
        {

            if(PlayerPrefs.GetInt(guns[i].name + "Locked") == 0)
            {
                if(guns[i] != null)
                {
                    Debug.Log("Unlocked " + guns[i].name);
                    guns[i].Unlock();
                }

            }
            else
            {
                if (guns[i] != null)
                {
                    guns[i].Lock();
                }
            }
        }

        CycleWeapon();
        CycleWeapon();
    }

    float timer = 0f;
    float interval;
    int currentShot;
    private void Update()
    {
        if (shooting)
        {
            timer += Time.deltaTime;

            while (timer >= interval)
            {
                guns[current].Shoot();
                if (guns[current].type == 1)
                {

                    currentShot += 1;
                    if(currentShot >= guns[current].shotCount)
                    {
                        FinnishedShot();
                    }
                }

                timer -= interval;
            }
        }

    }
    void FinnishedShot()
    {
        currentShot = 0;
        shooting = false;
    }
    public void TryShoot()
    {
        if(guns[current].type == 0)
        {
            guns[current].Shoot();
            return;
        }
        if(guns[current].type == 1 || guns[current].type == 2)
        {
            interval = 1 / guns[current].fireRate;
            shooting = true;

        }
    }
    public void TryStopShoot()
    {
        if (guns[current].type == 2)
        {
            shooting = false;
        }
    }
    public void UnlockGun(string value)
    {
        for (int i = 0; i < guns.Count; i++)
        {
            if(guns[i].name == value)
            {
                guns[i].Unlock();
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
        if(guns[which].locked == true)
        {
            return;
        }
        for (int i = 0; i < guns.Count; i++)
        {
            if(i == which)
            {
                guns[i].gameObject.SetActive(true);
                Gun gun = guns[i];
                current = i;
            }
            else
            {
                Gun gun = guns[i];
                guns[i].gameObject.SetActive(false);
            }
        }
    }
    public void CycleWeapon()
    {
        ExitAim();
        shooting = false;
        if (guns.Count == 0)
        {
            Debug.Log("No weapons available.");
            return;
        }
        Gun gun = null;
        int maxAttempts = guns.Count; 

        for (int attempt = 0; attempt < maxAttempts; attempt++)
        {
            current++;
            if (current >= guns.Count)
            {
                current = 0;
            }
            gun = guns[current];
            if (gun != null && gun.locked == false)
            {

                if (gun.energy == 0) { if (blue.currentEnergy <= 0) { return; } }
                if (gun.energy == 1) { if (yellow.currentEnergy <= 0) { return; } }
                if (gun.energy == 2) { if (green.currentEnergy <= 0) { return; } }
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
        gun = guns[current];
        if (gun != null)
        {
            if (gun.locked == true)
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

    public void Aim()
    {
        aiming = !aiming;
        if (aiming)
        {
            player.GetCrossHairMain().SetActive(false);
            player.GetGunDisplay().gameObject.SetActive(false);
            Gun gun = guns[current];
            gun.SetAngle(false);
            player.GetMainCamera().fieldOfView = gun.aimFOV;
            scopeImages[gun.hdScope].gameObject.SetActive(true);
        }
        else
        {
            ExitAim();
        }
    }
    public void ExitAim()
    {
        player.GetCrossHairMain().SetActive(true);
        aiming = false;
        player.GetGunDisplay().gameObject.SetActive(true);
        player.GetMainCamera().fieldOfView = 60f;
        Gun gun = guns[current];
        gun.SetAngle(true);
        for (int i = 0; i < scopeImages.Count; i++)
        {
            scopeImages[i].gameObject.SetActive(false);
        }
    }
    public void CheckRetro()
    {
        if (aiming)
        {
            Gun gun = guns[current];
        }
    }
    public void Loadout()
    {
        string[] weaponArray = PlayerPrefs.GetString("Loadout").Split(',');

        GunManager gunManager = FindObjectOfType<GunManager>();
        foreach (string weapon in weaponArray)
        {
            gunManager.UnlockGun(weapon);
        }
        CycleWeapon();
        CycleWeapon();
    }
}
