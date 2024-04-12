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
        playerInventory = Inventory.Instance;
        shopItems = new List<Item>();
        shopItemSlot = shopSlotParent.GetComponentsInChildren<UI_ShopSlot>();
    }

    public void SetShopItem()
    {
        itemDatabase = DataManager.Instance.itemDatabase;

        //for (int i = 0; i < itemDatabase.ItemDatas.Count; i++)
        //{
        //    Item shopItemInstance = new Item();
        //    shopItemInstance = itemDatabase.ItemDatas[i];
        //    shopItems.Add(shopItemInstance);
        //    shopItemSlot[i].UpdateSlot(shopItems[i]);
        //}

        for (int i = 0; i < itemDatabase.ItemDatas.Count; i++)
        {
            Item shopItemInstance = itemDatabase.ItemDatas[i];

            if (shopItemInstance.ItemType == ItemType.Potion)
            {
                shopItemInstance.Quantity = 10;
                shopItems.Add(shopItemInstance);
            }
            else
            {
                shopItems.Add(shopItemInstance);
            }

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
            if(_selectItem.ItemType == ItemType.Potion)
            {
                if(_selectItem.Quantity > 1)
                {
                    _selectItem.Quantity--;
                    playerInventory.AddItem(_selectItem);
                    playerInventory.inventoryGold -= _selectItem.Price;
                    UpdateSlotUI();
                    playerInventory.UpdateRetainGold();
                    return;
                }
            }

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
