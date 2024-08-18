using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerSignInIcon : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI namePlate;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void SetNamePlaye(string value)
    {
        namePlate.text = value;
    }
}
