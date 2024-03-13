using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    //Test
    GameManager gameManager;
    DataManager dataManager;
    ItemDatabase itemDatabase;

    public List<ItemData> startingItems;
    //public List<Item> startingItems;

    public List<InventoryItem> equipment;
   // public Dictionary<ItemData_Equipment, InventoryItem> equipmentDictionary;
    //test
    public Dictionary<Item, InventoryItem> equipmentDictionaryTest;

    public List<InventoryItem> inventory;
    //public Dictionary<ItemData, InventoryItem> inventoryDictionary;
    //test
    public Dictionary<Item, InventoryItem> inventoryDictionaryTest;

    [Header("Inventory UI")]
    [SerializeField] private Transform inventorySlotParent;
    [SerializeField] private Transform equipmentSlotParent;
    

    private UI_ItemSlot[] inventoryItemSlot;
    private UI_EquipmentSlot[] equipmentSlot;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void Start()
    {
        //test
        gameManager = GameManager.instance;
        dataManager = gameManager.dataManager;
        itemDatabase = dataManager.itemDatabase;

        inventory = new List<InventoryItem>();
        //inventoryDictionary = new Dictionary<ItemData, InventoryItem>();
        //test
        inventoryDictionaryTest = new Dictionary<Item, InventoryItem>();


        equipment = new List<InventoryItem>();
        //equipmentDictionary = new Dictionary<ItemData_Equipment, InventoryItem>();
        //test
        equipmentDictionaryTest = new Dictionary<Item, InventoryItem>();

        inventoryItemSlot = inventorySlotParent.GetComponentsInChildren<UI_ItemSlot>();
        equipmentSlot = equipmentSlotParent.GetComponentsInChildren<UI_EquipmentSlot>();

        //AddStartingItems();
    }

    public void Gacha() // test
    {
        Item item = itemDatabase.GetRandomItem();  // 데이터베이스에서 랜덤한 데이터를 반환

        InventoryItem itemInstanceas = new InventoryItem(item); // 인벤토리에 들어갈 아이템 인스턴스 생성

        //inventory.Add(itemInstanceas);  
        AddItemTest(itemInstanceas.datas); // 인벤토리에 아이템 추가

        Debug.Log(itemInstanceas.datas.Name);
    }

    public void AddItemTest(Item _item)
    {
        //if ((_item.itemType == ItemType.Equipment || _item.itemType == ItemType.Material) && CanAddItem())
        //{
        //    AddToInventory(_item);
        //}

        AddToInventoryTest(_item);
        //UpdateSlotUI();
        //test
        UpdateSlotUITest();

    }

    private void AddToInventoryTest(Item _item)
    {
        if (inventoryDictionaryTest.TryGetValue(_item, out InventoryItem value))
        {
            value.AddStack();
        }
        else
        {
            InventoryItem newItem = new InventoryItem(_item);
            inventory.Add(newItem);
            inventoryDictionaryTest.Add(_item, newItem);
        }
    }

    private void UpdateSlotUITest()
    {
        for (int i = 0; i < equipmentSlot.Length; i++)
        {
            foreach (KeyValuePair<Item, InventoryItem> item in equipmentDictionaryTest)
            {
                //if (item.Key.equipmentType == equipmentSlot[i].slotType)
                //{
                //    equipmentSlot[i].UpdateSlot(item.Value);
                //}

                equipmentSlot[i].UpdateSlot(item.Value);
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
        if(inventory.Count >= inventoryItemSlot.Length)
        {
            return false;
        }

        return true;
    }
}
