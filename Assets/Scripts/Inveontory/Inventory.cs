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
    public GameObject characterPrefab;
    public SPUM_SpriteList characterSpriteOBj;

    public GameObject characterEquipmentPrefab;
    public SPUM_SpriteList equipmentSpriteOBj;

    ItemDatabase itemDatabase;

    //public List<ItemData> startingItems;
    //public List<Item> startingItems;

    [Header("Inventory")]
    public List<InventoryItem> equipment;
    // public Dictionary<ItemData_Equipment, InventoryItem> equipmentDictionary;
    //test
    public Dictionary<Item, InventoryItem> equipmentDictionary;

    public List<InventoryItem> inventory;
    //public Dictionary<ItemData, InventoryItem> inventoryDictionary;
    //test
    public Dictionary<Item, InventoryItem> inventoryDictionary;

    [Header("Inventory UI")]
    [SerializeField] private Transform inventorySlotParent;
    [SerializeField] private Transform equipmentSlotParent;


    private UI_ItemSlot[] inventoryItemSlot;
    private UI_EquipmentSlot[] equipmentSlot;

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
        itemDatabase = GameManager.instance.dataManager.itemDatabase;

        inventory = new List<InventoryItem>();
        //inventoryDictionary = new Dictionary<ItemData, InventoryItem>();
        //test
        inventoryDictionary = new Dictionary<Item, InventoryItem>();


        equipment = new List<InventoryItem>();
        //equipmentDictionary = new Dictionary<ItemData_Equipment, InventoryItem>();
        //test
        equipmentDictionary = new Dictionary<Item, InventoryItem>();


        inventoryItemSlot = inventorySlotParent.GetComponentsInChildren<UI_ItemSlot>();
        equipmentSlot = equipmentSlotParent.GetComponentsInChildren<UI_EquipmentSlot>();

        //Gold Test
        statusData = player.Data.StatusData;
        inventoryGold = statusData.Gold;
        UpdateRetainGold();


        //AddStartingItems();
    }

    public void AddItem(Item _item)
    {
        //if (CanAddItem())
        //{
        //    AddToInventory(_item);
        //}
        AddToInventory(_item);
        //GetComponent<Image>().SetNativeSize();
        //UpdateSlotUI();
        //test
        UpdateSlotUI();

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

    private void UpdateSlotUI()
    {
        for (int i = 0; i < equipmentSlot.Length; i++)
        {
            foreach (KeyValuePair<Item, InventoryItem> item in equipmentDictionary)
            {
                if (item.Key.EquipmentType == equipmentSlot[i].slotType)
                {
                    equipmentSlot[i].UpdateSlot(item.Value);
                }
            }
        }

        for (int i = 0; i < inventoryItemSlot.Length; i++)
        {
            inventoryItemSlot[i].CleanUpSlot();
        }

        for (int i = 0; i < inventory.Count; i++)
        {
            inventoryItemSlot[i].UpdateSlot(inventory[i]);
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

        UpdateSlotUI();
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

        UpdateSlotUI();
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

    //public void EquipItem(ItemData _item)
    //{
    //    ItemData_Equipment newEquipment = _item as ItemData_Equipment;
    //    InventoryItem newItem = new InventoryItem(newEquipment);

    //    ItemData_Equipment oldEquipment = null;

    //    foreach (KeyValuePair<ItemData_Equipment, InventoryItem> item in equipmentDictionary)
    //    {
    //        if (item.Key.equipmentType == newEquipment.equipmentType)
    //        {
    //            oldEquipment = item.Key;
    //        }
    //    }

    //    if(oldEquipment != null)
    //    {
    //        UnEquipItem(oldEquipment);
    //        AddItem(oldEquipment);
    //    }

    //    equipment.Add(newItem);
    //    equipmentDictionary.Add(newEquipment, newItem);
    //    RemoveItem(_item);

    //    UpdateSlotUI();
    //}

    //public void UnEquipItem(ItemData_Equipment itemToRemove)
    //{
    //    if (equipmentDictionary.TryGetValue(itemToRemove, out InventoryItem value))
    //    {
    //        equipment.Remove(value);
    //        equipmentDictionary.Remove(itemToRemove);
    //        //itemToRemove.RemoveModifiers();
    //    }
    //}

    //private void UpdateSlotUI()
    //{
    //    for(int i=0; i < equipmentSlot.Length; i++)
    //    {
    //        foreach (KeyValuePair<ItemData_Equipment, InventoryItem> item in equipmentDictionary)
    //        {
    //            if (item.Key.equipmentType == equipmentSlot[i].slotType)
    //            {
    //                equipmentSlot[i].UpdateSlot(item.Value);
    //            }
    //        }
    //    }

    //    for(int i= 0; i< inventoryItemSlot.Length; i++)
    //    {
    //        inventoryItemSlot[i].CleanUpSlot();
    //    }

    //    for (int i = 0; i < inventory.Count; i++)
    //    {
    //        inventoryItemSlot[i].UpdateSlot(inventory[i]);
    //    }
    //}

    //public void AddItem(ItemData _item)
    //{
    //    if ((_item.itemType == ItemType.Equipment || _item.itemType == ItemType.Material) && CanAddItem())
    //    {
    //        AddToInventory(_item);
    //    }

    //    UpdateSlotUI();
    //}

    //private void AddToInventory(ItemData _item)
    //{
    //    if (inventoryDictionary.TryGetValue(_item, out InventoryItem value))
    //    {
    //        value.AddStack();
    //    }
    //    else
    //    {
    //        InventoryItem newItem = new InventoryItem(_item);
    //        inventory.Add(newItem);
    //        inventoryDictionary.Add(_item, newItem);
    //    }
    //}

    //public void RemoveItem(ItemData _item)
    //{
    //    if (inventoryDictionary.TryGetValue(_item, out InventoryItem value))
    //    {
    //        if(value.stackSize <= 1)
    //        {
    //            inventory.Remove(value);
    //            inventoryDictionary.Remove(_item);
    //        }
    //        else
    //        {
    //            value.RemoveStack();
    //        }
    //    }

    //    UpdateSlotUI() ;

    //}

    public bool CanAddItem()
    {
        if (inventory.Count >= inventoryItemSlot.Length)
        {
            return false;
        }

        return true;
    }
}
