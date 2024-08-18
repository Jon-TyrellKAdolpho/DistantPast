using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GunManager : MonoBehaviour
{

    //[HideInInspector]
    KeaPlayer player;
    public List<Gun> guns;
    [SerializeField] List<Image> gunImages;
    [SerializeField] int current;
    [SerializeField] GameObject soundPrefab;

    [SerializeField] SettingsHandler settingsHandler;

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

        if (!PlayerPrefs.HasKey(guns[0].name + "Locked"))
        {
            Loadout();
            return;
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
                gun.gunImage.color = new Vector4(1, 1, 1, 1);
            }
            else
            {
                Gun gun = guns[i];
                if(gun != null)
                {
                    guns[i].gunImage.color = new Vector4(1, 1, 1, .5f);
                }

                guns[i].gameObject.SetActive(false);
            }
        }
    }
    public void CycleWeapon()
    {
        ExitAim();
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

            if (gun != null && gun.energy.currentEnergy > 0 && gun.locked == false)
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
         //   if(gun.retroScope != null)
       //     {
         //       if (settingsHandler.retro)
         //       {
          //          gun.retroScope.SetActive(true);
       //         }
      //          else
      //          {
         //           gun.hdScope.SetActive(true);
       //         }
    //        }
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
        player.GetMainCamera().fieldOfView = settingsHandler.mainFOV;
        Gun gun = guns[current];
        gun.SetAngle(true);
        player.OffScopes();
    }
    public void CheckRetro()
    {
        if (aiming)
        {
            Gun gun = guns[current];
            if (settingsHandler.retro)
            {
             //   gun.hdScope.SetActive(false);
             //   gun.retroScope.SetActive(true);
            }
            else
            {
              //  gun.hdScope.SetActive(true);
              //  gun.retroScope.SetActive(false);
            }
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
