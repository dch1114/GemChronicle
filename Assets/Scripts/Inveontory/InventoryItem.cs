using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InventoryItem
{
    //public ItemData data;
    //test
    //public int no;
    public Item datas;
    public int stackSize;

    public InventoryItem(Item _newItemData)  //ItemData _newItemData)
    {
        datas = _newItemData;
        AddStack();
    }

    public void AddStack() => stackSize++;

    public void RemoveStack() => stackSize--;
}
