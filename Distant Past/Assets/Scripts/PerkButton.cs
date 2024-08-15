using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PerkButton : MonoBehaviour
{
    [SerializeField] string perkName;
    Button button;
    // Start is called before the first frame update
    private void Start()
    {
        button = GetComponent<Button>();
        if(PlayerPrefs.GetInt(perkName) == 1)
        {
            button.enabled = false;
        }
    }


}
