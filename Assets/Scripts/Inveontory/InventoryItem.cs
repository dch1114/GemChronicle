using System;

[Serializable]
public class InventoryItem
{
    public Item datas;
    public int stackSize;

    public InventoryItem(Item _newItemData)
    {
        datas = _newItemData;
        AddStack();
    }

    public void AddStack(int amount = 1) => stackSize += amount;

    public void RemoveStack(int amount = 1) => stackSize -= amount;
}
