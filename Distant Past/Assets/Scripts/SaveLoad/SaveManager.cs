using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;
public class SaveManager : MonoBehaviour
{
    [SerializeField] string loadoutGuns;
    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject playerInputPrefab;
    [SerializeField] PlayerInputManager manager;
    int playerCount = 1;
    List<Transform> spawnPoints = new List<Transform>();

    void Start()
    {

        playerCount = Mathf.Max(1, PlayerSaves.playerTwo != "" ? 2 : 1, 
            PlayerSaves.playerThree != "" ? 3 : 1, PlayerSaves.playerFour != "" ? 4 : 1);


        Debug.Log("PlayerCount: " + playerCount);
        foreach (Transform child in transform)
        {
            SpawnPoint spawnpoint = child.GetComponent<SpawnPoint>();
            if(spawnpoint != null)
            {
                spawnPoints.Add(spawnpoint.transform);
            }
        }
        Debug.Log(spawnPoints.Count);
        for (int i = 0; i < playerCount; i++)
        {
            Spawn(i);
        }

    }
    public void Spawn(int which)
    {
        GameObject playerprefab = Instantiate(playerPrefab, spawnPoints[0]);
        KeaPlayer player = playerprefab.GetComponent<KeaPlayer>();
        FindObjectOfType<PlayerInputManager>().JoinPlayer(
            playerIndex: which + 1, // Assign this player to index 1
            splitScreenIndex: 0, // Assign this player to the first split-screen position
            controlScheme: "Gamepad", // Use the "Gamepad" control scheme
            pairWithDevice: Gamepad.all.Count > 0 ? Gamepad.all[which] : null // Pair with the first available gamepad
        );
        playerprefab.transform.SetParent(null);
        player.SetPlayerName(which);

    }
    public Transform SpawnPoint()
    {
        Debug.Log("Spawns + " + spawnPoints.Count);
        int which = Random.Range(0, spawnPoints.Count);
        return spawnPoints[which];
    }
    /*
    public void SaveGame()
    {
        Health playerhealth = KeaPlayer.instance.GetComponent<Health>();

        PlayerPrefs.SetInt("MaxHealth", playerhealth.maxHealth);
        PlayerPrefs.SetInt("CurrentHealth", playerhealth.currentHealth);

        PlayerPrefs.SetInt("Level", KeaPlayer.instance.currentLevel);
        PlayerPrefs.SetFloat("CurrentExp", KeaPlayer.instance.currentExp);

        Energy blueenergy = KeaPlayer.instance.blue;
        PlayerPrefs.SetFloat("BlueMax", blueenergy.maxEnergy);
        PlayerPrefs.SetFloat("BlueCurrent", blueenergy.currentEnergy);
        PlayerPrefs.SetFloat("BlueRechargeRate", blueenergy.rechargeRate);
        PlayerPrefs.SetFloat("BlueRechargeTime", blueenergy.rechargeTime);

        Energy yellowenergy = KeaPlayer.instance.yellow;
        PlayerPrefs.SetFloat("YellowMax", yellowenergy.maxEnergy);
        PlayerPrefs.SetFloat("YellowCurrent", yellowenergy.currentEnergy);
        PlayerPrefs.SetFloat("YellowRechargeRate", yellowenergy.rechargeRate);
        PlayerPrefs.SetFloat("YellowRechargeTime", yellowenergy.rechargeTime);

        Energy greenenergy = KeaPlayer.instance.green;
        PlayerPrefs.SetFloat("GreenMax", greenenergy.maxEnergy);
        PlayerPrefs.SetFloat("GreenCurrent", greenenergy.currentEnergy);
        PlayerPrefs.SetFloat("GreenRechargeRate", greenenergy.rechargeRate);
        PlayerPrefs.SetFloat("GreenRechargeTime", greenenergy.rechargeTime);

        GunManager gunmanager = KeaPlayer.instance.GetComponentInChildren<GunManager>();

        Gun autorifle = gunmanager.guns[0].GetComponent<Gun>();
        PlayerPrefs.SetInt("AutoRifleLocked", autorifle.locked ? 1 : 0);
        PlayerPrefs.SetInt("AutoRifleDamage", autorifle.damage);
        PlayerPrefs.SetFloat("AutoRifleEnergyPerShot", autorifle.energyPerShot);

        Gun repeatrifle = gunmanager.guns[1].GetComponent<Gun>();
        PlayerPrefs.SetInt("RepeatRifleLocked", repeatrifle.locked ? 1 : 0);
        PlayerPrefs.SetInt("RepeatRifleDamage", repeatrifle.damage);
        PlayerPrefs.SetFloat("RepeatRifleEnergyPerShot", repeatrifle.energyPerShot);

        Gun heavyrifle = gunmanager.guns[2].GetComponent<Gun>();
        PlayerPrefs.SetInt("HeavyRifleLocked", heavyrifle.locked ? 1 : 0);
        PlayerPrefs.SetInt("HeavyRifleDamage", heavyrifle.damage);
        PlayerPrefs.SetFloat("HeavyRifleEnergyPerShot", heavyrifle.energyPerShot);

        Gun pistol = gunmanager.guns[3].GetComponent<Gun>();
        Debug.Log(pistol.name);
        PlayerPrefs.SetInt("PistolLocked", pistol.locked ? 1 : 0);
        PlayerPrefs.SetInt("PistolDamage", pistol.damage);
        PlayerPrefs.SetFloat("PistolEnergyPerShot", pistol.energyPerShot);

        Gun rifle = gunmanager.guns[4].GetComponent<Gun>();
        PlayerPrefs.SetInt("RifleLocked", rifle.locked ? 1 : 0);
        PlayerPrefs.SetInt("RifleDamage", rifle.damage);
        PlayerPrefs.SetFloat("RifleEnergyPerShot", rifle.energyPerShot);

        Gun sniperrifle = gunmanager.guns[5].GetComponent<Gun>();
        PlayerPrefs.SetInt("SniperRifleLocked", sniperrifle.locked ? 1 : 0);
        PlayerPrefs.SetInt("SniperRifleDamage", sniperrifle.damage);
        PlayerPrefs.SetFloat("SniperRifleEnergyPerShot", sniperrifle.energyPerShot);

        Gun shotgun = gunmanager.guns[6].GetComponent<Gun>();
        PlayerPrefs.SetInt("ShotgunLocked", shotgun.locked ? 1 : 0);
        PlayerPrefs.SetInt("ShotgunDamage", shotgun.damage);
        PlayerPrefs.SetFloat("ShotgunEnergyPerShot", shotgun.energyPerShot);

        Gun cannon = gunmanager.guns[7].GetComponent<Gun>();
        PlayerPrefs.SetInt("CannonLocked", cannon.locked ? 1 : 0);
        PlayerPrefs.SetInt("CannonDamage", cannon.damage);
        PlayerPrefs.SetFloat("CannonEnergyPerShot", cannon.energyPerShot);



    }
    public void LoadGame()
    {
        // Health.
        Health playerhealth = KeaPlayer.instance.GetComponent<Health>();
        playerhealth.maxHealth = PlayerPrefs.GetInt("MaxHealth");  
        playerhealth.currentHealth = PlayerPrefs.GetInt("CurrentHealth");

        // Set up player level.
        KeaPlayer.instance.currentLevel = PlayerPrefs.GetInt("Level");
        KeaPlayer.instance.currentExp = PlayerPrefs.GetFloat("CurrentExp");
        KeaPlayer.instance.GainExp(0);

        // Blue energy savestate.
        Energy blueenergy = KeaPlayer.instance.blue;
        blueenergy.maxEnergy = PlayerPrefs.GetFloat("BlueMax");
        blueenergy.currentEnergy = PlayerPrefs.GetFloat("BlueCurrent");
        blueenergy.SetRechargeRate(PlayerPrefs.GetFloat("BlueRechargeRate"));
        blueenergy.SetRechargeTime(PlayerPrefs.GetFloat("BlueRechargeTime"));

        // Yellow energy savestate.
        Energy yellowenergy = KeaPlayer.instance.yellow;
        yellowenergy.maxEnergy = PlayerPrefs.GetFloat("YellowMax");
        yellowenergy.currentEnergy = PlayerPrefs.GetFloat("YellowCurrent");
        yellowenergy.SetRechargeRate(PlayerPrefs.GetFloat("YellowRechargeRate"));
        yellowenergy.SetRechargeTime(PlayerPrefs.GetFloat("YellowRechargeTime"));

        // Green energy savestate.
        Energy greenenergy = KeaPlayer.instance.yellow;
        greenenergy.maxEnergy = PlayerPrefs.GetFloat("GreenMax");
        greenenergy.currentEnergy = PlayerPrefs.GetFloat("GreenCurrent");
        greenenergy.SetRechargeRate(PlayerPrefs.GetFloat("GreenRechargeRate"));
        greenenergy.SetRechargeTime(PlayerPrefs.GetFloat("GreenRechargeTime"));


        GunManager gunmanager = KeaPlayer.instance.GetComponentInChildren<GunManager>();



        // Auto-Rifle
        Gun autorifle = gunmanager.guns[0].GetComponent<Gun>();
        autorifle.locked = (0 != PlayerPrefs.GetInt("AutoRifleLocked"));
        autorifle.damage = PlayerPrefs.GetInt("AutoRifleDmage");
        autorifle.energyPerShot = PlayerPrefs.GetFloat("AutoRifleEnergyPerShot");

        Gun repeatrifle = gunmanager.guns[1].GetComponent<Gun>();
        repeatrifle.locked = (0 != PlayerPrefs.GetInt("RepeatRifleLocked"));
        repeatrifle.damage = PlayerPrefs.GetInt("RepeatRifleDmage");
        repeatrifle.energyPerShot = PlayerPrefs.GetFloat("RepeatRifleEnergyPerShot");

        // Heavy-Rifle
        Gun heavyrifle = gunmanager.guns[2].GetComponent<Gun>();
        heavyrifle.locked = (0 != PlayerPrefs.GetInt("HeavyRifleLocked"));
        heavyrifle.damage = PlayerPrefs.GetInt("HeavyRifleDmage");
        heavyrifle.energyPerShot = PlayerPrefs.GetFloat("HeavyRifleEnergyPerShot");

        // Pistol
        Gun pistol = gunmanager.guns[3].GetComponent<Gun>();
        pistol.locked = (0 != PlayerPrefs.GetInt("PistolLocked"));
        pistol.damage = PlayerPrefs.GetInt("PistolDamage");
        pistol.energyPerShot = PlayerPrefs.GetFloat("PistolEnergyPerShot");

        // Rifle
        Gun rifle = gunmanager.guns[4].GetComponent<Gun>();
        rifle.locked = (0 != PlayerPrefs.GetInt("RifleLocked"));
        rifle.damage = PlayerPrefs.GetInt("RifleDamage");
        rifle.energyPerShot = PlayerPrefs.GetFloat("RifleEnergyPerShot");

        // Sniper Rifle
        Gun sniperrifle = gunmanager.guns[5].GetComponent<Gun>();
        sniperrifle.locked = (0 != PlayerPrefs.GetInt("SniperRifleLocked"));
        sniperrifle.damage = PlayerPrefs.GetInt("SniperRifleDamage");
        sniperrifle.energyPerShot = PlayerPrefs.GetFloat("SniperRifleEnergyPerShot");


        // Shotgun
        Gun shotgun = gunmanager.guns[6].GetComponent<Gun>();
        shotgun.locked = (0 != PlayerPrefs.GetInt("ShotgunLocked"));
        shotgun.damage = PlayerPrefs.GetInt("ShotgunDmage");
        shotgun.energyPerShot = PlayerPrefs.GetFloat("ShotgunEnergyPerShot");

        // HeavyShotGun
        Gun cannon = gunmanager.guns[7].GetComponent<Gun>();
        cannon.locked = (0 != PlayerPrefs.GetInt("CannonLocked"));
        cannon.damage = PlayerPrefs.GetInt("CannonDmage");
        cannon.energyPerShot = PlayerPrefs.GetFloat("CannonPerShot");
    }
   */
   
}
