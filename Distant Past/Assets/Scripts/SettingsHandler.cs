using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Audio;
using TMPro;
public class SettingsHandler : MonoBehaviour
{
    [Tooltip("General")]
    [SerializeField] Image mainGFX;
    [SerializeField] GameObject buttonOrganizer;
    [SerializeField] GameObject settingsGFX;
    //Retro Toggle
    [Tooltip("Retro")]
    [HideInInspector]
    public bool retro = true;
    [SerializeField] Camera main;
    [SerializeField] RawImage mainImage;
    [SerializeField] RenderTexture mainRetro;
    [SerializeField] RenderTexture mainNormal;
    [SerializeField] Camera gun;
    [SerializeField] RawImage gunImage;
    [SerializeField] RenderTexture gunRetro;
    [SerializeField] RenderTexture gunNormal;

    [Tooltip("Brightness")]
    //BrightnessSetting
    [SerializeField] Slider brightnessSlider;
    [SerializeField] PostProcessProfile brightness;
    [SerializeField] PostProcessLayer brightnessLayer;
    [SerializeField] PostProcessProfile uiBrightness;
    [SerializeField] PostProcessLayer uiBrightnessLayer;
    AutoExposure exposure;
    AutoExposure uiExposure;

    [Tooltip("Audio")]
    [SerializeField] AudioMixer mixer;

    [Tooltip("FrameRate")]
    [SerializeField] TextMeshProUGUI frameRate;
    [SerializeField] TextMeshProUGUI targetFrameRateText;
    private int lastFrameIndex;
    private float[] frameDeltaTimeArray;

    public KeyCode retroKey = KeyCode.Tab;
    [SerializeField] Toggle retroToggle;

    [SerializeField] Camera mainCam;
    [SerializeField] GunManager gunManager;
    public float mainFOV;
    

    // Start is called before the first frame update
    private void Awake()
    {
        uiBrightness.TryGetSettings(out uiExposure);
        brightness.TryGetSettings(out exposure);
        frameDeltaTimeArray = new float[100];
        SetFrameRate(2);
        DeactivateSettings();
        mainFOV = mainCam.fieldOfView;

    }
    void Start()
    {
        AdjustBrightness(1);
    }
    float timer;
    private void Update()
    {
        frameDeltaTimeArray[lastFrameIndex] = Time.unscaledDeltaTime;
        lastFrameIndex = (lastFrameIndex + 1) % frameDeltaTimeArray.Length;

        timer -= Time.unscaledDeltaTime;
        if(timer <= 0)
        {
            frameRate.text = "FR\n" + Mathf.RoundToInt(CalculateFrameRate()).ToString();
            timer = .25f;
        }
        if (Input.GetKeyDown(retroKey))
        {
            ToggleRetro(!retro);
        }
    }
    public void ActivateSettings()
    {
        settingsGFX.SetActive(true);
        buttonOrganizer.SetActive(false);
        mainGFX.enabled = false;
    }
    public void DeactivateSettings()
    {
        settingsGFX.SetActive(false);
        buttonOrganizer.SetActive(true);
        mainGFX.enabled = true;
    }
    public void AdjustBrightness(float value)
    {
        if(value != 0)
        {
            if(exposure != null && uiExposure != null)
            {
                exposure.keyValue.value = value;
                uiExposure.keyValue.value = value;
            }

        }
        else
        {
            exposure.keyValue.value = .05f;
            uiExposure.keyValue.value = .05f;
        }
    }
    public void ToggleRetro(bool value)
    {
        gunManager.CheckRetro();
        retro = value;
        if (retro)
        {
            mainImage.texture = mainRetro;
            main.targetTexture = mainRetro;
            gunImage.texture = gunRetro;
            gun.targetTexture = gunRetro;
            retroToggle.isOn = true;
        }
        else
        {
            mainImage.texture = mainNormal;
            main.targetTexture = mainNormal;
            gunImage.texture = gunNormal;
            gun.targetTexture = gunNormal;
            retroToggle.isOn = false;
        }
    }
    public void SetFrameRate(float value)
    {
        if(value == 0)
        {
            Application.targetFrameRate = 30;
            targetFrameRateText.text = "Target Frame Rate: 30";
        }
        if(value == 1)
        {
            Application.targetFrameRate = 60;
            targetFrameRateText.text = "Target Frame Rate: 60";
        }
        if(value == 2)
        {
            Application.targetFrameRate = 100;
            targetFrameRateText.text = "Target Frame Rate: 100";
        }
        if(value == 3)
        {
            Application.targetFrameRate = 200;
            targetFrameRateText.text = "Target Frame Rate: 200";
        }
        if(value == 4)
        {
            Application.targetFrameRate = 300;
            targetFrameRateText.text = "Target Frame Rate: 300";
        }
        if(value == 5)
        {
            Application.targetFrameRate = 600;
            targetFrameRateText.text = "Target Frame Rate: 600";
        }
    }
    public void SetMasterVolume(float value)
    {
        if(value <= -30)
        {
            mixer.SetFloat("MasterVolume", -60);
            return;
        }
        mixer.SetFloat("MasterVolume", value);
    }
    public void SetSFXVolume(float value)
    {
        if (value <= -30)
        {
            mixer.SetFloat("SFXVolume", -60);
            return;
        }
        mixer.SetFloat("SFXVolume", value);
    }
    public void SetMusicVolume(float value)
    {
        if (value <= -30)
        {
            mixer.SetFloat("MusicVolume", -60);
            return;
        }
        mixer.SetFloat("MusicVolume", value);
    }
    public float CalculateFrameRate()
    {
        float total = 0f;
        foreach(float deltaTime in frameDeltaTimeArray)
        {
            total += deltaTime;
        }
        return frameDeltaTimeArray.Length / total;
    }

    public void SetFieldOfView(float value)
    {
        mainFOV = value;
        if (!gunManager.aiming)
        {
            mainCam.fieldOfView = mainFOV;
        }
    }
}
