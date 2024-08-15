using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ImageModifier : MonoBehaviour
{
    Image image;
    Color originalColor;
    public List<Icon> icons;
    private void Awake()
    {
        image = GetComponent<Image>();
        originalColor = image.color;
        ChangeIcon(0);
    }
    public void ChangeIcon(int value)
    {
        SetIcon(icons[value].sprite, icons[value].sizeDelta, icons[value].color);
    }
    void SetIcon(Sprite icon, Vector2 size, Color color)
    {
        image.sprite = icon;
        if (size == new Vector2(0, 0))
        {
            image.rectTransform.sizeDelta = new Vector2(10, 10);
        }
        else
        {
            image.rectTransform.sizeDelta = size;
        }
        image.color = color;
    }
}