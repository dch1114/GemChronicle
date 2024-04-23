using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIController : MonoBehaviour
{

    //[SerializeField] public GameObject inventoryPanel;
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
    //test
    [Header("Shop UI")]
    [SerializeField] private Transform inventoryListSlotParent;


    [SerializeField] private UI_ItemSlot[] inventoryItemSlot;
    [SerializeField] private UI_EquipmentSlot[] equipmentSlot;
    [SerializeField] private UI_Status[] uI_Statuses;
    //test
    [SerializeField] private UI_ItemSlot[] inventoryListItemSlot;

    //test
    //[Header("Sound")]
    //public AudioClip inventoryOpenSound;
    //public AudioClip inventoryCloseSound;

    void Start()
    {
        //inventoryPrefab = Inventory.Instance;
        //inventoryItemSlot = inventorySlotParent.GetComponentsInChildren<UI_ItemSlot>();
        //equipmentSlot = equipmentSlotParent.GetComponentsInChildren<UI_EquipmentSlot>();
        _amountSwitch = GetComponentInChildren<Switch>();
        ////test
        //inventoryListItemSlot = inventoryListSlotParent.GetComponentsInChildren<UI_ItemSlot>();

        itemToopTip.gameObject.SetActive(false);
    }

    //test
    public void UpdateSlotUI()
    {
        for (int i = 0; i < equipmentSlot.Length; i++)
        {
            //test
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
        for (int i = 0; i< uI_Statuses.Length; i++)
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
        //shopTradePopup.gameObject.SetActive(false);
        shopUi.SetActive(false);
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
        //if(inventoryPrefab.equipmentDictionary == null)
        //{
        //    inventoryPrefab.equipmentDictionary = new Dictionary<SlotType, InventoryItem>();
        //}

        //foreach(var item in inventoryPrefab.equipmentItems)
        //{
        //    SlotType slotType = item.datas.SlotType;

        //    inventoryPrefab.equipmentDictionary[slotType] = item;
        //}

        for (int i = 0; i < equipmentSlot.Length; i++)
        {
            //test
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

    //private void PlayInventoryOpenSound()
    //{
    //    SoundManager.PlayClip(inventoryOpenSound);
    //}

    //private void PlayInventoryCloseSound()
    //{
    //    SoundManager.PlayClip(inventoryCloseSound);
    //}
}
