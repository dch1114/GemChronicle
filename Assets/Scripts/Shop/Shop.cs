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
        itemDatabase = DataManager.instance.itemDatabase;

        playerInventory = Inventory.Instance;

        shopItems = new List<Item>();
        shopItemSlot = shopSlotParent.GetComponentsInChildren<UI_ShopSlot>();

        SetShopItem();
    }

    public void SetShopItem()
    {
        for (int i = 0; i < itemDatabase.ItemDatas.Count; i++)
        {
            Item shopItemInstance = new Item();
            shopItemInstance = itemDatabase.ItemDatas[i];
            shopItems.Add(shopItemInstance);
            shopItemSlot[i].UpdateSlot(shopItems[i]);
        }
    }

    private void UpdateSlotUI()
    {
        for (int i = 0; i < shopItemSlot.Length; i++)
        {
            shopItemSlot[i].CleanUpSlot();
        }

        for (int i = 0; i < shopItems.Count; i++)
        {
            shopItemSlot[i].UpdateSlot(shopItems[i]);
        }
    }

    public void Buy(Item _selectItem)
    {
        if (!(playerInventory.inventoryGold < _selectItem.Price))
        {
            shopItems.Remove(_selectItem);
            playerInventory.AddItem(_selectItem);

            playerInventory.inventoryGold -= _selectItem.Price;

            UpdateSlotUI();


            playerInventory.UpdateRetainGold();
        }
        else
        {
            //TODO ÆË¾÷ Ãß°¡
            Debug.Log("not enough gold");
        }
    }

    public void Sell(InventoryItem _inventoryItem)
    {
        if(_inventoryItem != null)
        {
            playerInventory.RemoveItem(_inventoryItem);
            shopItems.Add(_inventoryItem.datas);

            playerInventory.inventoryGold += _inventoryItem.datas.Price;

            UpdateSlotUI();

            playerInventory.UpdateRetainGold();
        }
    }
}
