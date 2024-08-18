using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
public class LobbyLocal : MonoBehaviour
{

    private List<InputDevice> usedDevices = new List<InputDevice>();
    [SerializeField] GameObject playerSelect;
    [SerializeField] PlayerIcon playerIconPrefab;

    int currentMap;
    public List<GameMap> maps;
    [SerializeField] TextMeshProUGUI gameMap;
    [SerializeField] TextMeshProUGUI mapDescription;
    [SerializeField] Image mapImage;

    int currentGameMode;
    [SerializeField] List<GameMode> gameModes;
    [SerializeField] TextMeshProUGUI gameMode;


    
    private void Start()
    {
        PlayerSaves.SetPlayerNames(new string[] { "Alice", "Bob", "Charlie" });
        playerSelect.SetActive(false);
        if(PlayerSaves.currentMap == "")
        {
            SetMap(0);
        }
        if(PlayerSaves.currentGameMode == "")
        {
            SetGameMode(0);
        }
    }
    public void Join(InputAction.CallbackContext context)
    {
        if (context.performed)
        {

            InputControl control = context.control;
            InputDevice device = control.device;

            if (!usedDevices.Contains(device))
            {
                Debug.Log("New controller!");
                List<string> players = PlayerSaves.GetPlayerNames().ToList();
                for (int i = players.Count - 1; i >= 0; i--)
                {
                    
                    if(players[i] == PlayerSaves.playerOne || players[i] == PlayerSaves.playerTwo
                        || players[i] == PlayerSaves.playerThree || players[i] == PlayerSaves.playerFour)
                    {
                        players.Remove(players[i]);
                    }
                }


                playerSelect.SetActive(true);
                foreach(RectTransform child in playerSelect.GetComponent<RectTransform>())
                {
                    Destroy(child.gameObject);
                }
                for (int i = 0; i < players.Count; i++)
                {
                    PlayerIcon playericon = Instantiate(playerIconPrefab, playerSelect.transform);
                    playericon.playerName.text = players[i];
                    if(i == 0)
                    {
                        EventSystem.current.SetSelectedGameObject(playericon.gameObject);
                    }

                }
                usedDevices.Add(device);
            }
        }

    }
    public void SetMap(int which)
    {
        currentMap = which;
        gameMap.text = maps[which].mapDisplayName;
        mapDescription.text = maps[which].mapDescription;
        mapImage.sprite = maps[which].mapIcon;
    }
    public void SetGameMode(int which)
    {
        currentGameMode = which;
        gameMode.text = gameModes[which].modeName;
    }
    public void StartGame()
    {
        PlayerSaves.currentMap = maps[currentMap].mapScene;
        PlayerSaves.currentGameMode = gameModes[currentGameMode].modeName;
        SceneManager.LoadScene(maps[currentMap].mapScene);
    }

}
