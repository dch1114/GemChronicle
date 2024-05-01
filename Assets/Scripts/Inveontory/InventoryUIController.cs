using System.Collections.Generic;
using UnityEngine;

public class InventoryUIController : MonoBehaviour
{
    [SerializeField] public GameObject inventoryUIPanel;
    [SerializeField] public GameObject shopUi;
    [SerializeField] public CheckPurchasePopup shopTradePopup;

    public UI_ItemToolTip itemToopTip;
    public UI_ItemToolTip shopitemToolTip;

    [SerializeField] Inventory inventoryPrefab;

    [Header("Inventory UI")]
    [SerializeField] private Transform inventorySlotParent;
    [SerializeField] private Transform equipmentSlotParent;
    [SerializeField] private Transform statusParent;
    [HideInInspector] public Switch _amountSwitch;

    [Header("Shop UI")]
    [SerializeField] private Transform inventoryListSlotParent;


    [SerializeField] private UI_ItemSlot[] inventoryItemSlot;
    [SerializeField] private UI_EquipmentSlot[] equipmentSlot;
    [SerializeField] private UI_Status[] uI_Statuses;

    [SerializeField] private UI_ItemSlot[] inventoryListItemSlot;

    void Start()
    {
        _amountSwitch = GetComponentInChildren<Switch>();
        itemToopTip.gameObject.SetActive(false);
    }

    public void UpdateSlotUI()
    {
        for (int i = 0; i < equipmentSlot.Length; i++)
        {
            foreach (KeyValuePair<SlotType, InventoryItem> item in inventoryPrefab.equipmentDictionary)
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

        for (int i = 0; i < inventoryPrefab.inventoryItems.Count; i++)
        {
            inventoryItemSlot[i].UpdateSlot(inventoryPrefab.inventoryItems[i]);
            inventoryListItemSlot[i].UpdateSlot(inventoryPrefab.inventoryItems[i]);
        }
    }

    public void UpdateStatus()
    {
        for (int i = 0; i < uI_Statuses.Length; i++)
        {
            uI_Statuses[i].UpdateStatValueUI();
        }
    }

    public void UseShop()
    {
        inventoryUIPanel.SetActive(false);
        shopUi.SetActive(true);
    }

    public void CloseShop()
    {
        shopitemToolTip.gameObject.SetActive(false);
        itemToopTip.gameObject.SetActive(false);
        CloseShopTradePopup();
        shopUi.SetActive(false);
    }

    private void CloseShopTradePopup()
    {
        shopTradePopup.checkPurchasePopup.SetActive(false);
        shopTradePopup.checkSellPopup.SetActive(false);
        shopTradePopup.checkPotionPurchasePopup.SetActive(false);
        shopTradePopup.checkPotionSellPopup.SetActive(false);
    }

    //test
    public void ToggleInventory()
    {
        if (inventoryUIPanel.activeSelf)
        {
            SoundManager.Instance.PlayClip(SoundManager.Instance.inventoryCloseSound);
        }
        else
        {
            SoundManager.Instance.PlayClip(SoundManager.Instance.inventoryOpenSound);
        }
    }

    public void UpdateLoadItemSlotUI()
    {
        for (int i = 0; i < equipmentSlot.Length; i++)
        {
            foreach (var item in inventoryPrefab.equipmentDictionary)
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

        for (int i = 0; i < inventoryPrefab.inventoryItems.Count; i++)
        {
            inventoryItemSlot[i].UpdateSlot(inventoryPrefab.inventoryItems[i]);
            inventoryListItemSlot[i].UpdateSlot(inventoryPrefab.inventoryItems[i]);
        }
    }
}
