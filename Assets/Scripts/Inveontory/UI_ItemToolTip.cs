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
        if (item == null)
        {
            return;
        }

        SlotType slotType = item.SlotType;

        itemNameText.text = item.Name;
        //itemTypeText.text = item.SlotType.ToString();
        ChangeSlotTypeToKorean(slotType);
        itemDescription.text = item.GetDescription();

        if (itemNameText.text.Length > 12)
        {
            itemNameText.fontSize = itemNameText.fontSize * 0.7f;
        }
        else
        {
            itemNameText.fontSize = defaultFontSize;
        }
        gameObject.SetActive(true);
    }

    private void ChangeSlotTypeToKorean(SlotType _slotType)
    {
        switch (_slotType)
        {
            case SlotType.Weapon:
                itemTypeText.text = "公扁";
                break;
            case SlotType.Armor:
                itemTypeText.text = "规绢备";
                break;
            case SlotType.Potion:
                itemTypeText.text = "器记";
                break;
        }
    }
    public void HideToolTip()
    {
        itemNameText.fontSize = defaultFontSize;
        gameObject.SetActive(false);
    }

    public void OnClickCloseToopTip()
    {
        gameObject.SetActive(false);
    }

}
