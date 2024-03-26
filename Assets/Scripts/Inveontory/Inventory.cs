using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    //Gold Test
    public Player player;
    public int inventoryGold;
    PlayerStatusData statusData;
    [SerializeField] private TextMeshProUGUI goldText;
    // Sprite Test
    [Header("Sprite Change")]
    [SerializeField] private SPUM_SpriteList characterSpriteOBj;
    [SerializeField] private SPUM_SpriteList equipmentSpriteOBj;

    [Header("Inventory")]
    public List<InventoryItem> equipment;
    public Dictionary<Item, InventoryItem> equipmentDictionary;

    public List<InventoryItem> inventory;
    public Dictionary<Item, InventoryItem> inventoryDictionary;

    //tset
    [SerializeField] private InventoryUIController inventoryUIController;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void Start()
    {
        inventory = new List<InventoryItem>();
        inventoryDictionary = new Dictionary<Item, InventoryItem>();

        equipment = new List<InventoryItem>();
        equipmentDictionary = new Dictionary<Item, InventoryItem>();

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
        if (inventoryDictionary.TryGetValue(_item, out InventoryItem _value))  //_item이 인벤토리에 이미 있으면 true 반환 _value에 값 저장,  
        {
            _value.AddStack();
        }
        else
        {
            InventoryItem newItem = new InventoryItem(_item);  // 이전에 인벤토리에 없으면 새로  생성해서 inventory와 딕셔너리에 추가
            inventory.Add(newItem);
            inventoryDictionary.Add(_item, newItem);
        }
    }

    public void EquipItem(Item _item)
    {
        Item newEquipment = _item;
        InventoryItem newItem = new InventoryItem(newEquipment);

        Item oldEquipment = null;

        foreach (KeyValuePair<Item, InventoryItem> item in equipmentDictionary)
        {
            if (item.Key.EquipmentType == newEquipment.EquipmentType)
            {
                oldEquipment = item.Key;
            }
        }

        if (oldEquipment != null)
        {
            UnEquipItem(oldEquipment);
            RemoveItemStat(oldEquipment);
            AddItem(oldEquipment);
        }

        equipment.Add(newItem);
        equipmentDictionary.Add(newEquipment, newItem);
        AddItemStat(_item);
        RemoveItem(_item);

        if (newItem.datas.EquipmentType == EquipmentType.Weapon)
        {
            characterSpriteOBj._weaponList[0].sprite = newItem.datas.sprite;
            equipmentSpriteOBj._weaponList[0].sprite = newItem.datas.sprite;
        }
        else if (newItem.datas.EquipmentType == EquipmentType.Armor)
        {
            characterSpriteOBj._armorList[0].sprite = newItem.datas.sprite;
            equipmentSpriteOBj._armorList[0].sprite = newItem.datas.sprite;
        }

        inventoryUIController.UpdateSlotUI();
    }

    public void UnEquipItem(Item _itemToRemove)
    {
        if (equipmentDictionary.TryGetValue(_itemToRemove, out InventoryItem _value))
        {
            equipment.Remove(_value);
            equipmentDictionary.Remove(_itemToRemove);

            RemoveItemStat(_itemToRemove);
        }

        if (_itemToRemove.EquipmentType == EquipmentType.Weapon)
        {
            characterSpriteOBj._weaponList[0].sprite = null;
            equipmentSpriteOBj._weaponList[0].sprite = null;
        }
        else if (_itemToRemove.EquipmentType == EquipmentType.Armor)
        {
            characterSpriteOBj._armorList[0].sprite = null;
            equipmentSpriteOBj._armorList[0].sprite = null;
        }

    }

    public void RemoveItem(Item _item)
    {
        if (inventoryDictionary.TryGetValue(_item, out InventoryItem _value))
        {
            if (_value.stackSize <= 1)
            {
                inventory.Remove(_value);
                inventoryDictionary.Remove(_item);
            }
            else
            {
                _value.RemoveStack();
            }
        }

        inventoryUIController.UpdateSlotUI();
    }

    public void AddItemStat(Item _item)
    {
        statusData.Atk += _item.Damage;
        statusData.Def += _item.Armor;
    }

    public void RemoveItemStat(Item _item)
    {
        statusData.Atk -= _item.Damage;
        statusData.Def -= _item.Armor;
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
