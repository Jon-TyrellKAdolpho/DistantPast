using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
public class PauseHandler : MonoBehaviour
{
    public static PauseHandler instance;
    public GameObject pauseMenu;
    [SerializeField] GameObject notifyAudio;
    [SerializeField] GameObject notifier;
    [SerializeField] TextMeshProUGUI notificationText;
    [SerializeField] GameObject bindingMenu;
    private SettingsHandler settings;
    public bool isPaused = false;
    public bool playerPause;
    [SerializeField] UnityEvent onPause;
    [SerializeField] UnityEvent onResume;

    private void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
       
        settings = GetComponent<SettingsHandler>();
        ResumeGame(); // Ensure the game starts in an unpaused state
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                playerPause = true;
                PauseGame();
            }
        }
    }

    public void Notify(string value)
    {
        notificationText.text = value;
        notifier.SetActive(true);
        playerPause = false;
        Instantiate(notifyAudio);
        PauseGame();
    }

    public void PauseGame()
    {
        FindObjectOfType<MusicManager>().StopRoutines();
        onPause.Invoke();
        settings.DeactivateSettings();
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        isPaused = true;
        if(playerPause == true)
        {
            notifier.SetActive(false);
            if (pauseMenu != null)
            {
                pauseMenu.SetActive(true);
            }
        }
    }

    public void ResumeGame()
    {
        onResume.Invoke();
        Time.timeScale = 1f; 
        isPaused = false;//
        notifier.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseMenu.SetActive(false);
        bindingMenu.SetActive(false);
        playerPause = false;
    }

    public void Quit()
    {
        
    }
}