using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

using UnityEngine.UI;
public class NetworkPlayer : NetworkBehaviour
{
    [SerializeField] Slider healthSlider;
    
    private NetworkVariable<int> currentHealth = new NetworkVariable<int>(100);
    [SerializeField] Health health;

    [SerializeField] Camera cam;
    [SerializeField] Camera gunCam;
    bool setUp;
    [SerializeField] GameObject ui;


    public override void OnNetworkSpawn()
    {
        if (!IsOwner)
        {
            ui.SetActive(false);
            cam.depth = -5;
            gunCam.enabled = false;
        }
    }
    [ServerRpc(RequireOwnership = false)]
    public void TakeDamageServerRPC(int value)
    {
        currentHealth.Value -= value;
        UpdateHealthClientRPC(currentHealth.Value);
    }

    [ClientRpc]
    void UpdateHealthClientRPC(int newHealth)
    {
        healthSlider.value = newHealth;
    }


}
