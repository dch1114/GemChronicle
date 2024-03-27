using System.Collections.Generic;
using UnityEngine;

public class InventoryUIController : MonoBehaviour
{

    [SerializeField] public GameObject inventoryUI;
    [SerializeField] public GameObject shopUi;
    [SerializeField] public CheckPurchasePopup shopTradePopup;

    public UI_ItemToolTip itemToopTip;
    public UI_ItemToolTip shopitemToolTip;

    Inventory inventoryContorller;

    //test
    [Header("Inventory UI")]
    [SerializeField] private Transform inventorySlotParent;
    [SerializeField] private Transform equipmentSlotParent;


    private UI_ItemSlot[] inventoryItemSlot;
    private UI_EquipmentSlot[] equipmentSlot;
    // Start is called before the first frame update
    void Start()
    {
        //test
        inventoryContorller = Inventory.Instance;
        inventoryItemSlot = inventorySlotParent.GetComponentsInChildren<UI_ItemSlot>();
        equipmentSlot = equipmentSlotParent.GetComponentsInChildren<UI_EquipmentSlot>();

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
        }

        for (int i = 0; i < inventoryContorller.inventory.Count; i++)
        {
            inventoryItemSlot[i].UpdateSlot(inventoryContorller.inventory[i]);
        }
    }


    public void UseShop()
    {
        shopUi.SetActive(true);
    }

    public void CloseShop()
    {
        shopitemToolTip.gameObject.SetActive(false);
        shopTradePopup.gameObject.SetActive(false);
        shopUi.SetActive(false);
    }
}
