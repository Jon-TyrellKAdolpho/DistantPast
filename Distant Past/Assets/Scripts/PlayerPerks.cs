using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PlayerPerks : MonoBehaviour
{
    public List<Perk> perks;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < perks.Count; i++)
        {
            if(PlayerPrefs.GetInt(perks[i].name) == 1)
            {
                perks[i].perk.Invoke();
                perks[i].unlocked = true;
            }
        }
    }

    public void UnlockPerk(string value)
    {
        for (int i = 0; i < perks.Count; i++)
        {
            if(perks[i].name == value)
            {
                perks[i].unlocked = true;
                perks[i].perk.Invoke();
                PlayerPrefs.SetInt(perks[i].name, 1);
            }
        }
    }
}
