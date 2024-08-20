using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Display : MonoBehaviour
{
    public List<KeaPlayer> players;
    [SerializeField] List<GameObject> displays;
    
    [SerializeField] Slider onePHealthSlider;
    [SerializeField] Energy onePBlueEnergy;
    [SerializeField] Energy onePYellowEnergy;
    [SerializeField] Energy onePGreenEnergy;
    [SerializeField] Slider onePExpSlider;
    [SerializeField] TextMeshProUGUI onePExpInfo;
    [SerializeField] RenderTexture onePMainText;
    [SerializeField] RenderTexture onePGunText;
    [SerializeField] RenderTexture onePMainTextRetro;
    [SerializeField] RenderTexture onePGunTextRetro;
    [SerializeField] RawImage onePMainDisplay;
    [SerializeField] RawImage onePGunDisplay;
    [SerializeField] GameObject onePCrossHair;
    [SerializeField] List<Image> onePScopes;
    [SerializeField] ImageModifier onePInteractModifier;

    [SerializeField] List<Slider> twoPHealthSliders;
    [SerializeField] List<Energy> twoPBlueEnergies;
    [SerializeField] List<Energy> twoPYellowEnergies;
    [SerializeField] List<Energy> twoPGreenEnergies;
    [SerializeField] List<Slider> twoPExpSliders;
    [SerializeField] List<TextMeshProUGUI> twoPExpInfo;
    [SerializeField] List<RenderTexture> twoPMainTexts;
    [SerializeField] List<RenderTexture> twoPGunTexts;
    [SerializeField] List<RenderTexture> twoPMainTextRetros;
    [SerializeField] List<RenderTexture> twoPGunTextRetros;
    [SerializeField] List<RawImage> twoPMainDisplay;
    [SerializeField] List<RawImage> twoPGunDisplay;
    [SerializeField] List<GameObject> twoPCrossHair;
    [SerializeField] List<Image> twoPScopes;
    [SerializeField] List<ImageModifier> twoPInteractModifier;
    public void AddPlayer(KeaPlayer player)
    {
        players.Add(player);
        int which = players.Count - 1;

        for (int i = 0; i < displays.Count; i++)
        {
            if (i != which)
            {
                displays[i].SetActive(false);
            }
            else
            {
                displays[i].SetActive(true);
                if (i == 0)
                {
                    player.SetDisplay(onePHealthSlider, onePBlueEnergy, onePYellowEnergy, onePGreenEnergy, onePExpSlider, onePExpInfo, onePMainText, onePGunText, 
                        onePMainTextRetro, onePGunTextRetro,onePMainDisplay, onePGunDisplay,onePCrossHair);
                    player.SetScopes(onePScopes);
                    player.SetInteractor(onePInteractModifier);
                }
                if (i == 1)
                {
                    players[0].SetDisplay(twoPHealthSliders[0], twoPBlueEnergies[0], twoPYellowEnergies[0], twoPGreenEnergies[0], twoPExpSliders[0], twoPExpInfo[0],
                        twoPMainTexts[0], twoPGunTexts[0], twoPMainTextRetros[0], twoPGunTextRetros[0],twoPMainDisplay[0], twoPGunDisplay[0], twoPCrossHair[0]);
                    List<Image> scopelist = twoPScopes.Take(10).ToList();
                    players[0].SetScopes(scopelist);
                    players[0].SetInteractor(twoPInteractModifier[0]);

                    player.SetDisplay(twoPHealthSliders[1], twoPBlueEnergies[1], twoPYellowEnergies[1], twoPGreenEnergies[1], twoPExpSliders[1], twoPExpInfo[1],
                        twoPMainTexts[1], twoPGunTexts[1], twoPMainTextRetros[1], twoPGunTextRetros[1],twoPMainDisplay[1], twoPGunDisplay[1],twoPCrossHair[1]);
                    List<Image> scopelist2 = twoPScopes.Skip(Mathf.Max(0, twoPScopes.Count - 10)).ToList();
                    players[1].SetScopes(scopelist2);
                    players[1].SetInteractor(twoPInteractModifier[1]);
                }
            }
        }

    }
}
