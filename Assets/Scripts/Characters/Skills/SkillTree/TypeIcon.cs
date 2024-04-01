using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TypeIcon : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI priceTxt;

    public void SetTypeIcon(Sprite sprite, int price)
    {
        if(price > 0)
        {
            if (!gameObject.activeSelf) gameObject.SetActive(true);
            SetTypeIcon(sprite);
            SetPriceTxt(price);
        } else
        {
            gameObject.SetActive(false);
        }
    }
    private void SetTypeIcon(Sprite sprite)
    {
        icon.sprite = sprite;
    }

    private void SetPriceTxt(int price)
    {
        priceTxt.text = price.ToString();
    }
}
