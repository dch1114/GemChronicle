using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEditor.Progress;


public class Inventory : Singleton<Inventory>
{
    //public Player player;
    Player player;
    public int inventoryGold;
    PlayerStatusData statusData;
    [SerializeField] private TextMeshProUGUI goldText;

    [Header("Sprite Change")]
    /*[SerializeField]*/ private SPUM_SpriteList characterSpriteOBj;
    [SerializeField] private SPUM_SpriteList equipmentSpriteOBj;

    [Header("Inventory")]
    public List<InventoryItem> equipmentItems;
    public Dictionary<SlotType, InventoryItem> equipmentDictionary;

    public List<InventoryItem> inventoryItems;

    [SerializeField] private InventoryUIController _inventoryUIController;

    public InventoryUIController InventoryUIController
    {
        get { return _inventoryUIController; }
        set { _inventoryUIController = value; }
    }

    private void Start()
    {  
        if(inventoryItems == null)
        {
            inventoryItems = new List<InventoryItem>();
        }
        
        if(equipmentItems == null)
        {
            equipmentItems = new List<InventoryItem>();
        }
        
        if (equipmentDictionary == null)
        {
            equipmentDictionary = new Dictionary<SlotType, InventoryItem>();
        }
            
        //characterSpriteOBj = player.gameObject.GetComponentInChildren<SPUM_SpriteList>();


        UpdateRetainGold();
    }

    public void AddItem(Item _item)
    {
        //if (CanAddItem())
        //{
        //    AddToInventory(_item);
        //}
        AddToInventory(_item);
        _inventoryUIController.UpdateSlotUI();

    }

    public void AddItem(Item _item, int _amount)
    {
        AddToInventory(_item, _amount);
        _inventoryUIController.UpdateSlotUI();
    }

    private void AddToInventory(Item _item)
    {
        //if(_item.ItemType == ItemType.Potion)
        //{
        //    foreach(InventoryItem item in inventory)
        //    {
        //        if(item.datas == _item)
        //        {
        //            item.AddStack();
        //            return;
        //        }
        //    }
        //}

        InventoryItem newItem = new InventoryItem(_item);
        inventoryItems.Add(newItem);
    }

    private void AddToInventory(Item _item, int _amount)
    {
        if (_item.ItemType == ItemType.Potion)
        {
            foreach (InventoryItem item in inventoryItems)
            {
                if (item.datas == _item)
                {
                    item.AddStack(_amount);
                    return;
                }
            }
        }

        InventoryItem newItem = new InventoryItem(_item, _amount);
        inventoryItems.Add(newItem);
    }

    public void EquipItem(InventoryItem _inventoryItem)
    {
        SlotType slotType = _inventoryItem.datas.SlotType;

        if (equipmentDictionary.ContainsKey(slotType))
        {
            InventoryItem itemToRemove = equipmentDictionary[slotType];
            equipmentDictionary.Remove(slotType);
            equipmentItems.Remove(itemToRemove);
            RemoveItemStat(itemToRemove.datas);
            AddItem(itemToRemove.datas);
        }

        equipmentDictionary[slotType] = _inventoryItem;
        equipmentItems.Add(_inventoryItem);
        AddItemStat(_inventoryItem.datas);
        RemoveItem(_inventoryItem);

        switch (slotType)
        {
            case SlotType.Weapon:
                characterSpriteOBj._weaponList[0].sprite = _inventoryItem.datas.sprite;
                equipmentSpriteOBj._weaponList[0].sprite = _inventoryItem.datas.sprite;
                break;
            case SlotType.Armor:
                characterSpriteOBj._armorList[0].sprite = _inventoryItem.datas.sprite;
                equipmentSpriteOBj._armorList[0].sprite = _inventoryItem.datas.sprite;
                break;
        }
        _inventoryUIController.UpdateSlotUI();
    }

    public void UnEquipItem(InventoryItem itemToRemove)
    {
        SlotType slotType = itemToRemove.datas.SlotType;

        if (equipmentDictionary.TryGetValue(slotType, out InventoryItem value))
        {
            equipmentDictionary.Remove(slotType);
            equipmentItems.Remove(value);
            RemoveItemStat(itemToRemove.datas);
        }

        switch (slotType)
        {
            case SlotType.Weapon:
                characterSpriteOBj._weaponList[0].sprite = null;
                equipmentSpriteOBj._weaponList[0].sprite = null;
                break;
            case SlotType.Armor:
                characterSpriteOBj._armorList[0].sprite = null;
                equipmentSpriteOBj._armorList[0].sprite = null;
                break;
        }

        _inventoryUIController.UpdateSlotUI();
    }

    public void RemoveItem(InventoryItem _inventoryItem)
    {
        if (inventoryItems.Contains(_inventoryItem))
        {
            if (_inventoryItem.stackSize <= 1)
            {
                inventoryItems.Remove(_inventoryItem);
            }
            else
            {
                _inventoryItem.RemoveStack();
            }
        }

        _inventoryUIController.UpdateSlotUI();
    }

    public void AddItemStat(Item _item)
    {
        statusData.Atk += _item.Damage;
        statusData.Def += _item.Armor;
        _inventoryUIController.UpdateStatus();
    }

    public void RemoveItemStat(Item _item)
    {
        statusData.Atk -= _item.Damage;
        statusData.Def -= _item.Armor;
        _inventoryUIController.UpdateStatus();
    }

    public void UpdateRetainGold()
    {
        statusData.Gold = inventoryGold;
        goldText.text = inventoryGold.ToString();
    }

    public void UseItem(InventoryItem _potion)
    {
        if (_potion.datas.ItemType != ItemType.Potion)
        {
            return;
        }
        else
        {
            if(_potion.stackSize > 1)
            {
                player.Data.StatusData.TakeHeal(_potion.datas.Recovery);
                _potion.stackSize--;
                _inventoryUIController.UpdateSlotUI();
                _inventoryUIController.UpdateStatus();
            }
            else
            {
                player.Data.StatusData.TakeHeal(_potion.datas.Recovery);
                RemoveItem(_potion);
                _inventoryUIController.UpdateSlotUI();
                _inventoryUIController.UpdateStatus();
            }
        }
    }

    public void LoadEquipItem(InventoryItem _inventoryItem)
    {
        SlotType slotType = _inventoryItem.datas.SlotType;

        if (this.equipmentDictionary == null)
        {
            this.equipmentDictionary = new Dictionary<SlotType, InventoryItem>();

            //foreach (var item in this.equipmentItems)
            //{
            //    slotType = item.datas.SlotType;

            //    this.equipmentDictionary[slotType] = item;

            //}
        }

        slotType = _inventoryItem.datas.SlotType;
        this.equipmentDictionary[slotType] = _inventoryItem;

        _inventoryUIController.UpdateLoadItemSlotUI();
        AddItemStat(_inventoryItem.datas);

        switch (slotType)
        {
            case SlotType.Weapon:
                characterSpriteOBj._weaponList[0].sprite = _inventoryItem.datas.sprite;
                equipmentSpriteOBj._weaponList[0].sprite = _inventoryItem.datas.sprite;
                break;
            case SlotType.Armor:
                characterSpriteOBj._armorList[0].sprite = _inventoryItem.datas.sprite;
                equipmentSpriteOBj._armorList[0].sprite = _inventoryItem.datas.sprite;
                break;
        }

        //if (equipmentDictionary.ContainsKey(slotType))
        //{
        //    InventoryItem itemToRemove = equipmentDictionary[slotType];
        //    equipmentDictionary.Remove(slotType);
        //    equipmentItems.Remove(itemToRemove);
        //    RemoveItemStat(itemToRemove.datas);
        //    AddItem(itemToRemove.datas);
        //}

        //equipmentDictionary[slotType] = _inventoryItem;

        //RemoveItem(_inventoryItem);

    }

    public void UpdateLoadInventoryItems()
    {
        foreach (var item in equipmentItems)
        {
            LoadEquipItem(item);
            _inventoryUIController.UpdateLoadItemSlotUI();
        }

        foreach (var item in inventoryItems)
        {
            _inventoryUIController.UpdateLoadItemSlotUI();
        }

        UpdateRetainGold();
    }

    public void SetPlayerData()
    {
        player = GameManager.Instance.player;
        statusData = player.Data.StatusData;
        inventoryGold = statusData.Gold;
        characterSpriteOBj = player.gameObject.GetComponentInChildren<SPUM_SpriteList>();
    }

    //public bool CanAddItem()
    //{
    //    if (inventory.Count >= inventoryItemSlot.Length)
    //    {
    //        return false;
    //    }

    //    return true;
    //}
}
