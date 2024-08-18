using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMap : MonoBehaviour
{
    public Sprite mapIcon;
    public string mapDisplayName;
    public string mapScene;
    [TextArea(10,10)]
    public string mapDescription;
}
