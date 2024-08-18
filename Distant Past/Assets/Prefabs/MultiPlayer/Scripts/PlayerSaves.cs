using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerSaves 
{
    public static string playerOne = "";
    public static string playerTwo = "";
    public static string playerThree = "";
    public static string playerFour = "";


    public static string currentMap = "";
    public static string currentGameMode = "";
    public static string[] GetPlayerNames()
    {
        if (PlayerPrefs.HasKey("PlayerNames"))
        {
            string playerNames = PlayerPrefs.GetString("PlayerNames");
            return playerNames.Split(',');
        }
        else
        {
            return new string[0];
        }
    }

    public static void SetPlayerNames(string[] playerNames)
    {
        // Join the string array into a single comma-separated string
        string playerNamesString = string.Join(",", playerNames);

        // Set the string in PlayerPrefs
        PlayerPrefs.SetString("PlayerNames", playerNamesString);

        // Save changes to PlayerPrefs
        PlayerPrefs.Save();
    }
}
