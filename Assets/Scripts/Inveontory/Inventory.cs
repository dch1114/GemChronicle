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
    public Dictionary<ItemData_Equipment, InventoryItem> equipmentDictionary;

    public List<InventoryItem> inventory;
    public Dictionary<ItemData, InventoryItem> inventoryDictionary;
    //test
    public Dictionary<Item, InventoryItem> itemDictionaryTest;

   // public List<InventoryItem> stash;
    //public Dictionary <ItemData, InventoryItem> stashDictionary;

    [Header("Inventory UI")]

    [SerializeField] private Transform inventorySlotParent;
    [SerializeField] private Transform equipmentSlotParent;
    //[SerializeField] private Transform stashSlotParent;
    

    private UI_ItemSlot[] inventoryItemSlot;
    private UI_EquipmentSlot[] equipmentSlot;
    //private UI_ItemSlot[] stashItemSlot;

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
        inventoryDictionary = new Dictionary<ItemData, InventoryItem>();

        //stash = new List<InventoryItem>();
        //stashDictionary = new Dictionary<ItemData, InventoryItem>();

        equipment = new List<InventoryItem>();
        equipmentDictionary = new Dictionary<ItemData_Equipment, InventoryItem>();

        inventoryItemSlot = inventorySlotParent.GetComponentsInChildren<UI_ItemSlot>();
        //stashItemSlot = stashSlotParent.GetComponentsInChildren<UI_ItemSlot>();
        equipmentSlot = equipmentSlotParent.GetComponentsInChildren<UI_EquipmentSlot>();

        AddStartingItems();
    }

    public void Gacha() // test
    {
        Item item = itemDatabase.GetRandomItem();  // �����ͺ��̽����� ������ �����͸� ��ȯ

        InventoryItem itemInstanceas = new InventoryItem(item); // �κ��丮�� �� ������ �ν��Ͻ� ����

        inventory.Add(itemInstanceas);  // �κ��丮�� ������ �߰�

        Debug.Log(itemInstanceas.datas.Name);
    }

    public void AddItemTest(Item _item)
    {
        //if ((_item.itemType == ItemType.Equipment || _item.itemType == ItemType.Material) && CanAddItem())
        //{
        //    AddToInventory(_item);
        //}
        //else if(_item.itemType == ItemType.Material)
        //{
        //    AddToStash(_item);
        //}

        AddToInventoryTest(_item);
        UpdateSlotUI();
    }

    private void AddToInventoryTest(Item _item)
    {
        if (itemDictionaryTest.TryGetValue(_item, out InventoryItem value))
        {
            value.AddStack();
        }
        else
        {
            InventoryItem newItem = new InventoryItem(_item);
            inventory.Add(newItem);
            itemDictionaryTest.Add(_item, newItem);
        }
    }

    private void AddStartingItems()
    {
        for (int i = 0; i < startingItems.Count; i++)
        {
            if (startingItems[i] != null)
                AddItem(startingItems[i]);
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

    public void UnEquipItem(ItemData_Equipment itemToRemove)
    {
        if (equipmentDictionary.TryGetValue(itemToRemove, out InventoryItem value))
        {
            equipment.Remove(value);
            equipmentDictionary.Remove(itemToRemove);
            //itemToRemove.RemoveModifiers();
        }
    }

    private void UpdateSlotUI()
    {
        for(int i=0; i < equipmentSlot.Length; i++)
        {
            foreach (KeyValuePair<ItemData_Equipment, InventoryItem> item in equipmentDictionary)
            {
                if (item.Key.equipmentType == equipmentSlot[i].slotType)
                {
                    equipmentSlot[i].UpdateSlot(item.Value);
                }
            }
        }

        for(int i= 0; i< inventoryItemSlot.Length; i++)
        {
            inventoryItemSlot[i].CleanUpSlot();
        }

        //for(int i= 0; i< stashItemSlot.Length; i++)
        //{
        //    stashItemSlot[i].CleanUpSlot();
        //}

        for (int i = 0; i < inventory.Count; i++)
        {
            inventoryItemSlot[i].UpdateSlot(inventory[i]);
        }

        //for (int i = 0;i < stash.Count; i++)
        //{
        //    stashItemSlot[i].UpdateSlot(stash[i]);
        //}
    }

    public void AddItem(ItemData _item)
    {
        if ((_item.itemType == ItemType.Equipment || _item.itemType == ItemType.Material) && CanAddItem())
        {
            //AddToInventory(_item);
        }
        //else if(_item.itemType == ItemType.Material)
        //{
        //    AddToStash(_item);
        //}


        UpdateSlotUI();
    }

    //private void AddToStash(ItemData _item)
    //{
    //    if (stashDictionary.TryGetValue(_item, out InventoryItem value))
    //    {
    //        value.AddStack();
    //    }
    //    else
    //    {
    //        InventoryItem newItem = new InventoryItem(_item);
    //        stash.Add(newItem);
    //        stashDictionary.Add(_item, newItem);
    //    }
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

    public void RemoveItem(ItemData _item)
    {
        if (inventoryDictionary.TryGetValue(_item, out InventoryItem value))
        {
            if(value.stackSize <= 1)
            {
                inventory.Remove(value);
                inventoryDictionary.Remove(_item);
            }
            else
            {
                value.RemoveStack();
            }
        }

        //if(stashDictionary.TryGetValue(_item, out InventoryItem stashValue))
        //{
        //    if(stashValue.stackSize <= 1)
        //    {
        //        stash.Remove(stashValue);
        //        stashDictionary.Remove(_item);
        //    }
        //    else
        //    {
        //        stashValue.RemoveStack();
        //    }
        //}

        UpdateSlotUI() ;

    }

    public bool CanAddItem()
    {
        if(inventory.Count >= inventoryItemSlot.Length)
        {
            return false;
        }

        return true;
    }
}