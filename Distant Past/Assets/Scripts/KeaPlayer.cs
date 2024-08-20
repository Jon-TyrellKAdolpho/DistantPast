using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class KeaPlayer : MonoBehaviour
{
    string playerName;

    [SerializeField] Camera mainCam;
    [SerializeField] Camera gunCam;

    RawImage mainDisplay;
    RawImage gunDisplay;
    GameObject crossHairMain;
    RenderTexture mainText;
    RenderTexture gunText;
    RenderTexture mainTextRetro;
    RenderTexture gunTextRetro;
    bool retro;

    Health playerHealth;
    Slider healthSlider;

    Slider expSlider;
    TextMeshProUGUI expInfo;
    [SerializeField] GameObject levelUpSoundDrop;


    public int spendPoints;
    public int[] expToNextLevel;
    public int currentLevel;
    public float currentExp;
    int maxLevel = 50;
    private void Awake()
    {
        FindObjectOfType<Display>().AddPlayer(this);

        playerHealth = GetComponent<Health>();
        healthSlider.maxValue = playerHealth.maxHealth;
        healthSlider.value = playerHealth.currentHealth;

        expToNextLevel = new int[maxLevel];
        expToNextLevel[1] = 1000;

        for(int i = 2; i < expToNextLevel.Length; i++)
        {
            expToNextLevel[i] = Mathf.RoundToInt(expToNextLevel[i - 1] * 1.07f);
        }
        expSlider.maxValue = expToNextLevel[currentLevel];
        expSlider.value = currentExp;
        expInfo.text = "LVL: " + currentLevel + " / EXP: " + Mathf.RoundToInt(currentExp);
    }
    public void SetDisplay(Slider health, Energy blue, Energy yellow, Energy green, Slider expslider, TextMeshProUGUI expinfo,
        RenderTexture maintext, RenderTexture guntext, RenderTexture maintextretro, RenderTexture guntextretro,RawImage maindisplay,  RawImage gundisplay, GameObject crosshair)
    {
        GunManager gunmanager = GetComponentInChildren<GunManager>();
        healthSlider = health; gunmanager.blue = blue; gunmanager.yellow = yellow; gunmanager.green = green; expSlider = expslider; expInfo = expinfo;
        mainText = maintext; gunText = guntext; mainTextRetro = maintextretro; gunTextRetro = guntextretro;mainDisplay = maindisplay; gunDisplay = gundisplay; 
        crossHairMain = crosshair; 

        if (retro)
        {
            mainCam.targetTexture = mainTextRetro;
            gunCam.targetTexture = gunTextRetro;
            maindisplay.texture = mainTextRetro;
            gundisplay.texture = gunTextRetro;
        }
        else
        {
            mainCam.targetTexture = mainText;
            gunCam.targetTexture = gunText;
            mainDisplay.texture = mainText;
            gunDisplay.texture = gunText;
        }
    }
    public void SetInteractor(ImageModifier modifier)
    {
        GetComponentInChildren<Interactor>().SetModifier(modifier);
    }
    public void SetScopes(List<Image> gunImages)
    {
        GetComponentInChildren<GunManager>().scopeImages = gunImages;
    }
    public string GetPlayerName()
    {
        string name = new string(playerName);
        return name;
    }
    public void SetPlayerName(int which)
    {
        playerName = which == 0 ? PlayerSaves.playerOne : which == 1 ? PlayerSaves.playerTwo 
            : which == 2 ? PlayerSaves.playerThree : PlayerSaves.playerFour;
    }
    public Camera GetMainCamera()
    {
        return mainCam;
    }
    public RawImage GetGunDisplay()
    {
        return gunDisplay;
    }
    public GameObject GetCrossHairMain()
    {
        return crossHairMain;
    }
    public void SetHealthSlider()
    {
        healthSlider.value = playerHealth.currentHealth;
    }

    public void GainExp(int value)
    {
        StartCoroutine(GainExpCoroutine(value));
    }

    private IEnumerator GainExpCoroutine(int value)
    {
        float startExp = currentExp;
        float targetExp = currentExp + value;

        if (targetExp > expToNextLevel[currentLevel])
        {
            GameObject sounddrop = Instantiate(levelUpSoundDrop);
            targetExp -= expToNextLevel[currentLevel];
            currentLevel++;
        }

        float elapsedTime = 0f;

        while (elapsedTime < .4f)
        {
            currentExp = Mathf.Lerp(startExp, targetExp, elapsedTime / .4f);
            expSlider.value = currentExp;
            expInfo.text = "LVL: " + currentLevel + " / EXP: " + Mathf.RoundToInt( currentExp);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        currentExp = targetExp;
        expSlider.maxValue = expToNextLevel[currentLevel];
        expSlider.value = currentExp;
        expInfo.text = "LVL: " + currentLevel + " / EXP: " + Mathf.RoundToInt(currentExp);
    }

    public void SpendPoint(int which)
    {

    }
}
