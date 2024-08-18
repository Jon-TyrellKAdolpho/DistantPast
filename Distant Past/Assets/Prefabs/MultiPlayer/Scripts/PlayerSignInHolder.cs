using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSignInHolder : MonoBehaviour
{
    [SerializeField] PlayerSignInIcon signInIconPrefab;

    List<PlayerSignInIcon> signInIcons;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void DisplaySignIn(string value)
    {
        PlayerSignInIcon signin = Instantiate(signInIconPrefab, transform);
        signin.SetNamePlaye(value);
    }
}
