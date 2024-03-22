using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    ItemDatabase itemDatabase;

    Inventory playerInventory;

    public List<Item> shopItems;

    [SerializeField] private Transform shopSlotParent;
    private UI_ShopSlot[] shopItemSlot;

    private void Start()
    {
        itemDatabase = GameManager.instance.dataManager.itemDatabase;

        playerInventory = Inventory.instance;

        shopItems = new List<Item>();
        shopItemSlot = shopSlotParent.GetComponentsInChildren<UI_ShopSlot>();

        SetShopItem();
    }

    public void SetShopItem()
    {
        for (int i = 0; i< itemDatabase.ItemDatas.Count; i++)
        {
            Item shopItemInstance = new Item();
            shopItemInstance = itemDatabase.ItemDatas[i];
            shopItems.Add(shopItemInstance);
            shopItemSlot[i].UpdateSlot(shopItems[i]);
        }
    }

    private void UpdateSlotUI()
    {
        //for (int i = 0; i < shopItems.Count; i++)
        //{
        //    shopItemSlot[i].UpdateSlot(shopItems[i]);
        //}

        for (int i = 0; i < shopItemSlot.Length; i++)
        {
            shopItemSlot[i].CleanUpSlot();
        }

        for (int i = 0; i < shopItems.Count; i++)
        {
            shopItemSlot[i].UpdateSlot(shopItems[i]);
        }
    }

    public void Buy(Item selectItem)
    {
        if(!(playerInventory.inventoryGold < selectItem.Price))
        {
            shopItems.Remove(selectItem);
            playerInventory.AddItem(selectItem);

            playerInventory.inventoryGold -= selectItem.Price;

            UpdateSlotUI();

            
            playerInventory.UpdateRetainGold();
        }
        else
        {
            Debug.Log("not enough gold");
        }
        //shopItems.Remove(selectItem);
        //playerInventory.AddItem(selectItem);
        //UpdateSlotUI();
    }
}
