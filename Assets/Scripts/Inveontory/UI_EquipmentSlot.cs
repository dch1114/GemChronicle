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
        //if(item == null || item.data == null)
        //{
        //    return;
        //}

        //float currentTime = Time.time;
        //float timeSinceLastClick = currentTime - lastClickTime;

        //if (timeSinceLastClick <= clickDelay)
        //{
        //    if (item.data.itemType == ItemType.Equipment)
        //    {
        //        Inventory.Instance.UnEquipItem(item.data as ItemData_Equipment);
        //        Inventory.Instance.AddItem(item.data as ItemData_Equipment);
        //        CleanUpSlot();
        //        ui.itemToopTip.HideToolTip();
        //    }
        //}
        //else
        //{
        //    AdjustToolTipPosition();
        //    ui.itemToopTip.ShowToolTip(item.data as ItemData_Equipment);
        //}

        //lastClickTime = currentTime;

        
        
        
        
        //AdjustToolTipPosition();

        //Inventory.Instance.UnEquipItem(item.data as ItemData_Equipment);
        //Inventory.Instance.AddItem(item.data as ItemData_Equipment);
        //CleanUpSlot();
    }
}
