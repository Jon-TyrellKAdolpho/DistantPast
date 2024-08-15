using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class KeaPlayer : MonoBehaviour
{
    public static KeaPlayer instance;
    [SerializeField] Slider healthSlider;
    [SerializeField] GameObject canvas;
    Health playerHealth;
    public Energy blue;
    public Energy yellow;
    public Energy green;
    [SerializeField] GameObject levelUpSoundDrop;
    [SerializeField] Slider expSlider;
    [SerializeField] TextMeshProUGUI expInfo;
    public int spendPoints;
    public int[] expToNextLevel;
    public int currentLevel;
    public float currentExp;
    int maxLevel = 50;
    private void Awake()
    {
        instance = this;

        playerHealth = GetComponent<Health>();
        healthSlider.maxValue = playerHealth.maxHealth;
        healthSlider.value = playerHealth.currentHealth;
        canvas.transform.SetParent(null);

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
