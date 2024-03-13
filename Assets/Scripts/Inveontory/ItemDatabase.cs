using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public int ID; // 이름이 동일해야 한다
    public string Name;
    public int Attack;
    public int Gold;
    public string Description;
}

public class ItemInstance
{
    int no;
    public Item item;
}

public class ItemDatabase : MonoBehaviour
{
    public List<Item> ItemInfos;        // 이름이 중요!!
    public Dictionary<int, Item> itemDic = new();

    public void Initialize()
    {
        foreach (Item item in ItemInfos)
        {
            itemDic.Add(item.ID, item);
        }
    }

    public Item GetItemByKey(int id)
    {
        //foreach (Item item in ItemInfos)
        //{
        //    if (item.ID == id)
        //    {
        //        return item;
        //    }
        //}
        //return null;

        if (itemDic.ContainsKey(id))
            return itemDic[id];

        return null;
    }

    public Item GetRandomItem()
    {
        if (ItemInfos.Count <= 0)
            return null;

        return ItemInfos[Random.Range(0, ItemInfos.Count)];
    }
}
