using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Energy : MonoBehaviour
{
    public List<GameObject> guns = new List<GameObject>();
    [SerializeField] GameObject ui;
    public bool active;
    public int energyType;
    Slider energySlider;
    public float maxEnergy;
    public float currentEnergy;
    public float rechargeRate;
    [HideInInspector]
    public float trueRechargeTime;
    public float rechargeTime;
    [SerializeField] GameObject warningUI;
    // Start is called before the first frame update
    void Start()
    {
        energySlider = GetComponent<Slider>();
        energySlider.maxValue = maxEnergy;
        energySlider.value = currentEnergy;
        Check();
    }

    public void Check()
    {
        for (int i = 0; i < guns.Count; i++)
        {
            Gun gun = guns[i].GetComponent<Gun>();
            Cannon cannon = guns[i].GetComponent<Cannon>();
            if(gun != null)
            {
                if (gun.locked != true)
                {
                    ui.SetActive(true);
                    active = true;
                    return;
                }
            }
            if(cannon != null)
            {
                if(cannon.locked != true)
                {
                    ui.SetActive(true);
                    active = true;
                    return;
                }
            }
        }
        ui.SetActive(false);
        active = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (trueRechargeTime > 0)
        {
            if(warningUI.activeInHierarchy != true)
            {
                warningUI.SetActive(true);
                
            }
            trueRechargeTime -= Time.deltaTime;
        }
        else
        {
            if (warningUI.activeInHierarchy == true)
            {
                warningUI.SetActive(false);
            }
            if (currentEnergy < maxEnergy)
            {
                currentEnergy += rechargeRate * Time.deltaTime;
                energySlider.value = currentEnergy;
            }
        }
    }

    public void UseEnergy(float amount)
    {
        currentEnergy -= amount;
        energySlider.value = currentEnergy;
        if (currentEnergy <= 0)
        {
            trueRechargeTime = rechargeTime;
        }
    }
    public void ChargeEnergy(float amount)
    {
        currentEnergy += amount;
        energySlider.value = currentEnergy;
        if(currentEnergy > maxEnergy)
        {
            currentEnergy = maxEnergy;
        }
        trueRechargeTime = 0;
    }

    public void SetMaxEnergy(float amount)
    {
        maxEnergy = amount;
    }
    public void SetRechargeTime(float amount)
    {
        rechargeTime = amount;
    }
    public void SetRechargeRate(float amount)
    {
        rechargeRate = amount;
    }
}
