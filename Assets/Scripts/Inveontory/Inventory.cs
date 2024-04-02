using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class Inventory : Singleton<Inventory>
{
    public Player player;
    public int inventoryGold;
    PlayerStatusData statusData;
    [SerializeField] private TextMeshProUGUI goldText;

    [Header("Sprite Change")]
    [SerializeField] private SPUM_SpriteList characterSpriteOBj;
    [SerializeField] private SPUM_SpriteList equipmentSpriteOBj;

    [Header("Inventory")]
    public List<InventoryItem> equipment;
    public Dictionary<SlotType, InventoryItem> equipmentDictionary;

    public List<InventoryItem> inventory;

    [SerializeField] private InventoryUIController inventoryUIController;

    private void Start()
    {
        inventory = new List<InventoryItem>();

        equipment = new List<InventoryItem>();
        equipmentDictionary = new Dictionary<SlotType, InventoryItem>();

        //Gold Test
        statusData = player.Data.StatusData;
        inventoryGold = statusData.Gold;
        UpdateRetainGold();
    }

    public void AddItem(Item _item)
    {
        //if (CanAddItem())
        //{
        //    AddToInventory(_item);
        //}
        AddToInventory(_item);
        //test
        inventoryUIController.UpdateSlotUI();

    }

    private void AddToInventory(Item _item)
    {
        InventoryItem newItem = new InventoryItem(_item);
        inventory.Add(newItem);
    }

    public void EquipItem(InventoryItem _inventoryItem)
    {
        SlotType slotType = _inventoryItem.datas.SlotType;

        if (equipmentDictionary.ContainsKey(slotType))
        {
            InventoryItem itemToRemove = equipmentDictionary[slotType];
            equipmentDictionary.Remove(slotType);
            equipment.Remove(itemToRemove);
            RemoveItemStat(itemToRemove.datas);
            AddItem(itemToRemove.datas);
        }

        equipmentDictionary[slotType] = _inventoryItem;
        equipment.Add(_inventoryItem);
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
        inventoryUIController.UpdateSlotUI();
    }

    public void UnEquipItem(InventoryItem itemToRemove)
    {
        SlotType slotType = itemToRemove.datas.SlotType;

        if (equipmentDictionary.TryGetValue(slotType, out InventoryItem value))
        {
            equipmentDictionary.Remove(slotType);
            equipment.Remove(value);
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

        inventoryUIController.UpdateSlotUI();
    }

    public void RemoveItem(InventoryItem _inventoryItem)
    {
        if (inventory.Contains(_inventoryItem))
        {
            if (_inventoryItem.stackSize <= 1)
            {
                inventory.Remove(_inventoryItem);
            }
            else
            {
                _inventoryItem.RemoveStack();
            }
        }

        inventoryUIController.UpdateSlotUI();
    }

    public void AddItemStat(Item _item)
    {
        statusData.Atk += _item.Damage;
        statusData.Def += _item.Armor;
        inventoryUIController.UpdateStatus();
    }

    public void RemoveItemStat(Item _item)
    {
        statusData.Atk -= _item.Damage;
        statusData.Def -= _item.Armor;
        inventoryUIController.UpdateStatus();
    }

    public void UpdateRetainGold()
    {
        statusData.Gold = inventoryGold;
        goldText.text = inventoryGold.ToString();
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
