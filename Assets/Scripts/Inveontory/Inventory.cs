using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Progress;


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
    //test
    //public Dictionary<Item, InventoryItem> equipmentDictionary;
    public Dictionary<SlotType, InventoryItem> equipmentDictionary;

    public List<InventoryItem> inventory;
    //test
    //public Dictionary<Item, InventoryItem> inventoryDictionary;

    [SerializeField] private InventoryUIController inventoryUIController;

    private void Start()
    {
        inventory = new List<InventoryItem>();
        //inventoryDictionary = new Dictionary<Item, InventoryItem>();

        equipment = new List<InventoryItem>();
        //test
        //equipmentDictionary = new Dictionary<Item, InventoryItem>();
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
        //test
        InventoryItem newItem = new InventoryItem(_item);
        inventory.Add(newItem);
    }

    //public void EquipItem(Item _item)
    //{
    //    InventoryItem newItem = new InventoryItem(_item);

    //    //Item oldEquipment = null;

    //    //test
    //    //foreach (KeyValuePair<Item, InventoryItem> item in equipmentDictionary)
    //    //{
    //    //    if (item.Key.EquipmentType == _item.EquipmentType)
    //    //    {
    //    //        oldEquipment = item.Key;
    //    //    }
    //    //}

    //    //if (oldEquipment != null)
    //    //{
    //    //    UnEquipItem(oldEquipment);
    //    //    RemoveItemStat(oldEquipment);
    //    //    AddItem(oldEquipment);
    //    //}

    //    //equipment.Add(newItem);
    //    //equipmentDictionary.Add(_item, newItem);
    //    //AddItemStat(_item);
    //    //RemoveItem(_item);

    //    if (equipmentDictionary.ContainsKey(newItem.datas.SlotType))
    //    {
    //        InventoryItem itemToRemove = equipmentDictionary[newItem.datas.SlotType];
    //        equipment.Remove(itemToRemove);
    //        RemoveItemStat(itemToRemove.datas);
    //    }

    //    equipment.Add(newItem);
    //    equipmentDictionary[newItem.datas.SlotType] = newItem;
    //    AddItemStat(_item);
    //    RemoveItem(_item);

    //    if (newItem.datas.SlotType == SlotType.Weapon)
    //    {
    //        characterSpriteOBj._weaponList[0].sprite = newItem.datas.sprite;
    //        equipmentSpriteOBj._weaponList[0].sprite = newItem.datas.sprite;
    //    }
    //    else if (newItem.datas.SlotType == SlotType.Armor)
    //    {
    //        characterSpriteOBj._armorList[0].sprite = newItem.datas.sprite;
    //        equipmentSpriteOBj._armorList[0].sprite = newItem.datas.sprite;
    //    }

    //    inventoryUIController.UpdateSlotUI();
    //}

    public void EquipItem(InventoryItem _inventoryItem)
    {
        SlotType slotType = _inventoryItem.datas.SlotType;

        if (equipmentDictionary.ContainsKey(slotType))
        {
            InventoryItem itemToRemove = equipmentDictionary[slotType];
            equipmentDictionary.Remove(slotType); //
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

        //if (_inventoryItem.datas.SlotType == SlotType.Weapon)
        //{
        //    characterSpriteOBj._weaponList[0].sprite = _inventoryItem.datas.sprite;
        //    equipmentSpriteOBj._weaponList[0].sprite = _inventoryItem.datas.sprite;
        //}
        //else if (_inventoryItem.datas.SlotType == SlotType.Armor)
        //{
        //    characterSpriteOBj._armorList[0].sprite = _inventoryItem.datas.sprite;
        //    equipmentSpriteOBj._armorList[0].sprite = _inventoryItem.datas.sprite;
        //}

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

        //if (itemToRemove.datas.SlotType == SlotType.Weapon)
        //{
        //    characterSpriteOBj._weaponList[0].sprite = null;
        //    equipmentSpriteOBj._weaponList[0].sprite = null;
        //}
        //else if (itemToRemove.datas.SlotType == SlotType.Armor)
        //{
        //    characterSpriteOBj._armorList[0].sprite = null;
        //    equipmentSpriteOBj._armorList[0].sprite = null;
        //}
    }

    //public void RemoveItem(Item _item)
    //{
    //    if (inventoryDictionary.TryGetValue(_item, out InventoryItem _value))
    //    {
    //        if (_value.stackSize <= 1)
    //        {
    //            inventory.Remove(_value);
    //            inventoryDictionary.Remove(_item);
    //        }
    //        else
    //        {
    //            _value.RemoveStack();
    //        }
    //    }

    //    inventoryUIController.UpdateSlotUI();
    //}

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
