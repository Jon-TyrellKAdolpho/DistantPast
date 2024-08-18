using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    LobbyLocal lobby;
    [SerializeField] MapButton mapButtonPrefab;
    [SerializeField] Transform holder;
    private void Start()
    {
        lobby = GetComponent<LobbyLocal>();
        for (int i = 0; i < lobby.maps.Count; i++)
        {
            MapButton mapbutton = Instantiate(mapButtonPrefab, holder);
            mapbutton.which = i;
            mapbutton.mapImage.sprite = lobby.maps[i].mapIcon;
        }
    }
    
}
