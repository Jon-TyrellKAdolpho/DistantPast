using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MapButton : MonoBehaviour
{
    public int which;
    public Image mapImage;
    public void SetMap()
    {
        FindObjectOfType<LobbyLocal>().SetMap(which);
    }
}
