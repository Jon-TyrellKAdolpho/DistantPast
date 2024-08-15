using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
public class HackingGameManager : MonoBehaviour
{
    [SerializeField] FirstPersonLook look;
    [SerializeField] FirstPersonMovement movement;
    public static HackingGameManager instance;
    [SerializeField] GameObject gfx;
    [SerializeField] Color selectColor;
    [SerializeField] Color deselectColor;
    [SerializeField] List<HackingSlider> sliders;
    [SerializeField] int current;

    [SerializeField] UnityEvent onStart;
    [SerializeField] UnityEvent standardOnSuccess;
    public UnityEvent onSuccess;

    [SerializeField] AudioSource scrollSound;
    int keysPressed;
    bool done;

    
    private void Start()
    {
        instance = this;
        FinnishGame();
    }

    public void StartGame()
    {
        look.enabled = false;
        movement.enabled = false;
        current = 0;
        done = false;
        for (int i = 0; i < sliders.Count; i++)
        {
            sliders[i].topped = false;
            sliders[i].slider.value = Random.Range(0,99);
            sliders[i].range.x = Random.Range(10, 90);
            sliders[i].range.y = sliders[i].range.x + 10;
        }
        Check();
        UpdateBackBackgrounds();
        gfx.SetActive(true);
    }
    public void FinnishGame()
    {
        look.enabled = true;
        movement.enabled = true;
        gfx.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (gfx.activeInHierarchy)
        {
            if(look.enabled != false)
            {
                movement.enabled = false;
                look.enabled = false;
            }
            HandleControls();
            AutoMovement();
        }
    }
    void AutoMovement()
    {
        foreach (var sliderData in sliders)
        {
            var slider = sliderData.slider;
            sliderData.number.text = sliderData.slider.value.ToString("F0");
            if (!sliderData.topped)
            {
                if (slider.value < slider.maxValue)
                {
                    slider.value += 5 * Time.deltaTime;
                    Check();
                }
                else
                {
                    sliderData.topped = true;
                }
            }
            else
            {
                if (slider.value > 0)
                {
                    slider.value -= 5 * Time.deltaTime;
                    Check();
                }
                else
                {
                    sliderData.topped = false;
                }
            }
        }
    }

    void HandleControls()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            FinnishGame();
        }
        if (Input.GetKey(KeyCode.W))
        {
            if (sliders[current].slider.value < sliders[current].slider.maxValue)
            {
                sliders[current].slider.value += 40 * Time.deltaTime;
                Check();
            }

        }
        if (Input.GetKey(KeyCode.S))
        {
            if (sliders[current].slider.value > 0)
            {
                sliders[current].slider.value -= 40 * Time.deltaTime;
                Check();
            }

        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            current++;
            if (current >= sliders.Count)
            {
                current = 0;
            }
            UpdateBackBackgrounds();

        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            current--;
            if (current < 0)
            {
                current = sliders.Count - 1;
            }
            UpdateBackBackgrounds();
        }
    }
    void UpdateBackBackgrounds()
    {
        for (int i = 0; i < sliders.Count; i++)
        {
            if (i != current)
            {
                sliders[i].background.color = selectColor;
            }
            else
            {
                sliders[i].background.color = deselectColor;
            }
        }
    }
    private void Check()
    {
        int correct = 0;
        for (int i = 0; i < sliders.Count; i++)
        {
            if (sliders[i].slider.value >= sliders[i].range.x && sliders[i].slider.value <= sliders[i].range.y)
            {
                sliders[i].sliderBackground.color = Color.green;
                correct++;
            }
            else
            {
                sliders[i].sliderBackground.color = Color.red;
            }
        }
        if(done != true)
        {
            if (correct == sliders.Count)
            {
                onSuccess.Invoke();
                standardOnSuccess.Invoke();
                FinnishGame();
                done = true;
            }
        }


    }
}
