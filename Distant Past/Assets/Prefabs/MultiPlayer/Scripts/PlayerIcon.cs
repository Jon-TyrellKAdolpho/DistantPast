using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
public class PlayerIcon : MonoBehaviour
{
    public Image playerImage;
    public TextMeshProUGUI playerName;

    public void SetUpPlayer()
    {
        if(PlayerSaves.playerOne == "")
        {
            PlayerSaves.playerOne = playerName.text;
        }
        else if(PlayerSaves.playerTwo == "")
        {
            PlayerSaves.playerTwo = playerName.text;
        }
        else if (PlayerSaves.playerThree == "")
        {
            PlayerSaves.playerThree = playerName.text;
        }
        else if (PlayerSaves.playerFour == "")
        {
            PlayerSaves.playerFour = playerName.text;
        }
        else
        {
            Debug.Log("Full roster.");
            return;
        }
        FindObjectOfType<PlayerSignInHolder>().DisplaySignIn(playerName.text);
        AfterSetUp();
    }
    void AfterSetUp()
    {
        GetComponentInParent<PlayerSelect>().gameObject.SetActive(false);
        EventSystem.current.SetSelectedGameObject(FindObjectOfType<PlayButton>().gameObject);
    }
}
