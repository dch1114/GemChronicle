using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_EquipmentSlot : UI_ItemSlot
{

    public EquipmentType slotType;

    private void OnValidate()
    {
        gameObject.name = "Equipment slot - " + slotType.ToString();  
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (item.stackSize == 0 || item == null)//item == null || item.datas == null)
        {
            return;
        }

        float currentTime = Time.time;
        float timeSinceLastClick = currentTime - lastClickTime;

        if (timeSinceLastClick <= clickDelay)
        {
            if (item.datas.ItemType == ItemType.Equipment)
            {
                Inventory.instance.UnEquipItemTest(item.datas);
                Inventory.instance.AddItemTest(item.datas);
                CleanUpSlot();
                ui.itemToopTip.HideToolTip();
            }
        }
        else
        {
            AdjustToolTipPosition();
            ui.itemToopTip.ShowToolTip(item.datas);
        }

        lastClickTime = currentTime;
    }

}
