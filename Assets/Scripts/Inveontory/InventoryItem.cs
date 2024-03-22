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

    public void AddStack() => stackSize++;

    public void RemoveStack() => stackSize--;
}
