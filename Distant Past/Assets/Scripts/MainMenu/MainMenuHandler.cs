using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenuHandler : MonoBehaviour
{
    [SerializeField] Button continueButton;
    private void Start()
    {
        if(PlayerPrefs.GetString("LastLevel") == "")
        {
            continueButton.interactable = false;
        }
    }
    public void NewGame()
    {
        Cursor.visible = false;
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("NewGame", 1);
        PlayerPrefs.SetInt("AutoRifle", 0);
        PlayerPrefs.SetInt("RepeatRifle", 1);
        PlayerPrefs.SetInt("Shotgun", 1);
        PlayerPrefs.SetInt("PistolLocked", 0);
        PlayerPrefs.SetInt("Rifle", 1);
        PlayerPrefs.SetInt("SniperRifle", 1);
        PlayerPrefs.SetInt("HeavyShotgun", 1);
        PlayerPrefs.SetInt("Cannon", 1);
 
        SceneManager.LoadScene("AAIntro3(Begin)");

    }
    public void LoadSavedGame()
    {
        PlayerPrefs.SetInt("LoadSave", 1);
        SceneManager.LoadScene(PlayerPrefs.GetString("LastLevel"));
    }

    public void LoadScene(string scenename)
    {
        SceneManager.LoadScene(scenename);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
