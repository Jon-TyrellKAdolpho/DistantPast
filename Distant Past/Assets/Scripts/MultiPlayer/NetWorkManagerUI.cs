using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
public class NetWorkManagerUI : MonoBehaviour
{
    [SerializeField] bool server;
    [SerializeField] GameObject ui;
    private void Start()
    {
        if(server == true)
        {
            Server();
        }
    }
    public void Server()
    {
        NetworkManager.Singleton.StartServer();
        ui.SetActive(false);
    }
    public void Host()
    {
        NetworkManager.Singleton.StartHost();
        ui.SetActive(false);
    }
    public void Client()
    {
        NetworkManager.Singleton.StartClient();
        ui.SetActive(false);
    }
}
