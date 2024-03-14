using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_ItemToolTip : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemTypeText;
    [SerializeField] private TextMeshProUGUI itemDescription;

    [SerializeField] private int defaultFontSize = 32;

    public void ShowToolTip(Item item)
    {
        if(item == null)
        {
            return;
        }

        itemNameText.text = item.Name;
        itemTypeText.text = item.EquipmentType.ToString();
        itemDescription.text = item.GetDescription();

        if(itemNameText.text.Length > 12)
        {
            itemNameText.fontSize = itemNameText.fontSize * 0.7f;
        }
        else
        {
            itemNameText.fontSize = defaultFontSize;
        }
        gameObject.SetActive(true);
    }

    public void HideToolTip()
    {
        itemNameText.fontSize = defaultFontSize;
        gameObject.SetActive(false);
    }

}
