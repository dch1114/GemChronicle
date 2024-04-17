using System.Collections.Generic;
using UnityEngine;

public class InventoryUIController : MonoBehaviour
{

    [SerializeField] public GameObject inventoryPanel;
    [SerializeField] public GameObject shopUi;
    [SerializeField] public CheckPurchasePopup shopTradePopup;

    public UI_ItemToolTip itemToopTip;
    public UI_ItemToolTip shopitemToolTip;

    Inventory inventoryContorller;

    [Header("Inventory UI")]
    [SerializeField] private Transform inventorySlotParent;
    [SerializeField] private Transform equipmentSlotParent;
    [SerializeField] private Transform statusParent;

    //test
    [Header("Shop UI")]
    [SerializeField] private Transform inventoryListSlotParent;


    private UI_ItemSlot[] inventoryItemSlot;
    private UI_EquipmentSlot[] equipmentSlot;
    [SerializeField] private UI_Status[] uI_Statuses;
    //test
    private UI_ItemSlot[] inventoryListItemSlot;

    void Start()
    {
        inventoryContorller = Inventory.Instance;
        inventoryItemSlot = inventorySlotParent.GetComponentsInChildren<UI_ItemSlot>();
        equipmentSlot = equipmentSlotParent.GetComponentsInChildren<UI_EquipmentSlot>();
        //uI_Statuses = statusParent.GetComponentsInChildren<UI_Status>(); // 그냥 연결을해버리고 프리팹화?
        ////test
        inventoryListItemSlot = inventoryListSlotParent.GetComponentsInChildren<UI_ItemSlot>();

        itemToopTip.gameObject.SetActive(false);
    }

    //test
    public void UpdateSlotUI()
    {
        for (int i = 0; i < equipmentSlot.Length; i++)
        {
            //test
            foreach (KeyValuePair<SlotType, InventoryItem> item in inventoryContorller.equipmentDictionary)
            {
                if (item.Key == equipmentSlot[i].slotType)
                {
                    equipmentSlot[i].UpdateSlot(item.Value);
                }
            }
        }

        for (int i = 0; i < inventoryItemSlot.Length; i++)
        {
            inventoryItemSlot[i].CleanUpSlot();
            inventoryListItemSlot[i].CleanUpSlot();
        }

        for (int i = 0; i < inventoryContorller.inventory.Count; i++)
        {
            inventoryItemSlot[i].UpdateSlot(inventoryContorller.inventory[i]);
            inventoryListItemSlot[i].UpdateSlot(inventoryContorller.inventory[i]);
        }
    }

    public void UpdateStatus()
    {
        for (int i = 0; i< uI_Statuses.Length; i++)
        {
            uI_Statuses[i].UpdateStatValueUI();
        }
    }

    public void UseShop()
    {
        inventoryPanel.SetActive(false);
        shopUi.SetActive(true);
    }

    public void CloseShop()
    {
        shopitemToolTip.gameObject.SetActive(false);
        itemToopTip.gameObject.SetActive(false);
        //shopTradePopup.gameObject.SetActive(false);
        shopUi.SetActive(false);
    }
}
