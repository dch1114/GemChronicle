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
                shopItemInstance.Quantity = 9999;
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
        //if (!(playerInventory.inventoryGold < _selectItem.Price))
        if(!(GameManager.Instance.player.Data.StatusData.Gold < _selectItem.Price))
        {
            //if(_selectItem.ItemType == ItemType.Potion)
            //{
            //    if(_selectItem.Quantity > 1)
            //    {
            //        _selectItem.Quantity--;
            //        playerInventory.AddItem(_selectItem);
            //        playerInventory.inventoryGold -= _selectItem.Price;
            //        UpdateSlotUI();
            //        playerInventory.UpdateRetainGold();
            //        return;
            //    }
            //}

            shopItems.Remove(_selectItem);
            playerInventory.AddItem(_selectItem);

            GameManager.Instance.player.Data.StatusData.Gold -= _selectItem.Price;
            GameManager.Instance.player.Data.StatusData.UseGold(_selectItem.Price);

            UpdateSlotUI();
        }
        else
        {
            //TODO 팝업 추가
            UIManager.Instance.alertPanelUI.ShowAlert("돈이 부족합니다.");
        }
    }

    public void BuyPotion(Item _selectItem, int _amount)
    {
        //if (!(playerInventory.inventoryGold < _selectItem.Price))
        if (!(GameManager.Instance.player.Data.StatusData.Gold < _selectItem.Price))
        {
            if(_selectItem.ItemType == ItemType.Potion)
            {
                if(_selectItem.Quantity > 1)
                {
                    _selectItem.Quantity -= _amount;
                    playerInventory.AddItem(_selectItem, _amount);
                    GameManager.Instance.player.Data.StatusData.UseGold(_selectItem.Price * _amount);
                    UpdateSlotUI();
                    return;
                }
            }

            shopItems.Remove(_selectItem);
            playerInventory.AddItem(_selectItem, _amount);
            GameManager.Instance.player.Data.StatusData.UseGold(_selectItem.Price * _amount);

            UpdateSlotUI();
        }
        else
        {
            //TODO 팝업 추가
            UIManager.Instance.alertPanelUI.ShowAlert("돈이 부족합니다.");
        }
    }

    public void Sell(InventoryItem _inventoryItem)
    {
        //if(_inventoryItem != null)
        //{
        //    playerInventory.RemoveItem(_inventoryItem);
        //    shopItems.Add(_inventoryItem.datas);

        //    playerInventory.inventoryGold += _inventoryItem.datas.Price;

        //    UpdateSlotUI();

        //    playerInventory.UpdateRetainGold();
        //}

        if (_inventoryItem != null)
        {
            //if (_inventoryItem.datas.ItemType == ItemType.Potion)
            //{
            //    foreach (Item shopItem in shopItems)
            //    {
            //        if(shopItem == _inventoryItem.datas)
            //        {
            //            _inventoryItem.stackSize--;
            //            shopItem.Quantity++;
            //            playerInventory.InventoryUIController.UpdateSlotUI();

            //            if (_inventoryItem.stackSize == 0)
            //            {
            //                playerInventory.RemoveItem(_inventoryItem); // 아이템 제거
            //                UpdateSlotUI();
            //            }
            //        }
            //    }

            //}

            playerInventory.RemoveItem(_inventoryItem); // 아이템 제거
            shopItems.Add(_inventoryItem.datas); // 상점 아이템 목록에 추가
            // 골드 업데이트 및 UI 갱신
            GameManager.Instance.player.Data.StatusData.Gold += _inventoryItem.datas.Price;
            GameManager.Instance.player.Data.StatusData.GetGold(_inventoryItem.datas.Price);
            UpdateSlotUI();
        }
    }

    public void SellPotion(InventoryItem _inventoryItem, int _amount)
    {
        if (_inventoryItem != null)
        {
            if (_inventoryItem.datas.ItemType == ItemType.Potion)
            {
                foreach (Item shopItem in shopItems)
                {
                    if (shopItem == _inventoryItem.datas)
                    {
                        _inventoryItem.stackSize -= _amount;
                        shopItem.Quantity += _amount;
                        playerInventory.InventoryUIController.UpdateSlotUI();

                        if (_inventoryItem.stackSize == 0)
                        {
                            playerInventory.RemoveItem(_inventoryItem); // 아이템 제거
                            UpdateSlotUI();
                        }
                    }
                }

            }
            //else
            //{
            //    playerInventory.RemoveItem(_inventoryItem); // 아이템 제거
            //    shopItems.Add(_inventoryItem.datas); // 상점 아이템 목록에 추가
            //}

            // 골드 업데이트 및 UI 갱신
            GameManager.Instance.player.Data.StatusData.Gold += _inventoryItem.datas.Price * _amount;
            GameManager.Instance.player.Data.StatusData.GetGold(_inventoryItem.datas.Price * _amount);
            UpdateSlotUI();
        }
    }
}
