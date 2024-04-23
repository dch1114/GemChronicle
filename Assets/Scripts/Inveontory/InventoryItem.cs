using System;

[Serializable]
public class InventoryItem
{
    public Item datas;
    public int stackSize;
    //public bool isEquip;

    public InventoryItem(Item _newItemData)
    {
        datas = _newItemData;
        AddStack();
    }

    public InventoryItem(Item _newItemData, int _amount)
    {
        datas = _newItemData;
        AddStack(_amount);
    }

    public void AddStack(int amount = 1) => stackSize += amount;

    public void RemoveStack(int amount = 1) => stackSize -= amount;
}
