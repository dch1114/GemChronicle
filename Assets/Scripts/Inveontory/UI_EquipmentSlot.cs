using UnityEngine;
using UnityEngine.EventSystems;

public class UI_EquipmentSlot : UI_ItemSlot
{
    public SlotType slotType;

    private void OnValidate()
    {
        gameObject.name = "Equipment slot - " + slotType.ToString();
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (inventoryItem == null || inventoryItem.stackSize == 0)//item == null || item.datas == null)
        {
            return;
        }

        float currentTime = Time.time;
        float timeSinceLastClick = currentTime - lastClickTime;

        if (timeSinceLastClick <= clickDelay)
        {
            if (inventoryItem.datas.ItemType == ItemType.Equipment)
            {
                Inventory.Instance.UnEquipItem(inventoryItem); //수정한 곳
                Inventory.Instance.AddItem(inventoryItem.datas);
                CleanUpSlot();
                ui.itemToopTip.HideToolTip();
            }
        }
        else
        {
            AdjustToolTipPosition();
            ui.itemToopTip.ShowToolTip(inventoryItem.datas);
        }

        lastClickTime = currentTime;
    }

}
