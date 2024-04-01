using System.Collections.Generic;
using System.Text;
using UnityEngine;


public enum SlotType // 수정한 곳
{
    Weapon,
    Armor,
    Potion,
    Gem
}

public enum ItemType
{
    Material,
    Equipment
}

[System.Serializable]
public class Item
{
    public int ID; // 엑셀과 스크립트의 변수명이 동일해야 한다
    public string Name;
    public int Damage;
    public int Armor;
    public string Description;
    public ItemType ItemType;
    public SlotType SlotType;
    public int Price;

    //test
    public bool isSellable = false;
    private int descriptionLength;
    private StringBuilder sb = new StringBuilder();
    public Sprite sprite;

    public string GetDescription()
    {
        sb.Length = 0;
        descriptionLength = 0;

        AddItemDescription(Damage, "Damage");
        AddItemDescription(Armor, "Armor");
        AddItemDescription(Description, "Description");
        AddItemDescription(Price, "Price");


        if (descriptionLength < 5)
        {
            sb.AppendLine();
            sb.Append("");
        }

        return sb.ToString();
    }

    private void AddItemDescription(int _value, string _name)
    {
        if (_value != 0)
        {
            if (sb.Length > 0)
            {
                sb.AppendLine();
            }

            if (_value > 0)
            {
                sb.Append(_name + ": " + _value);
            }

            descriptionLength++;
        }
    }

    private void AddItemDescription(string _value, string _name)
    {
        if (_value != "")
        {
            if (sb.Length > 0)
            {
                sb.AppendLine();
            }

            sb.Append(_name + ": " + _value);

            descriptionLength++;
        }
    }
}

[System.Serializable]
public class ItemDatabase
{
    public List<Item> ItemDatas;        // 이름이 중요!!
    public Dictionary<int, Item> itemDic = new();

    public void Initialize() // 다시 정리하면서 이미지 처리? 제이슨에서 경로를 가지고 있고 여기서 후가공을 하면된다.
    {
        foreach (Item item in ItemDatas)
        {
            itemDic.Add(item.ID, item);
            item.sprite = Resources.Load<Sprite>("Sprites/item" + item.ID); //네임이 아니라 경로라면 미리 세팅이 되지 않겠냐.
            // 경로를 알고 있으면 리소스 로드해서 스프라이트를 미리 세팅 엑셀데이터에 스프라이트 패스를 적는다. 그러면 리소스 로드를 할 수 있다. 
        }
    }

    //public Item GetItemByKey(int id)
    //{
    //    //foreach (Item item in ItemInfos)
    //    //{
    //    //    if (item.ID == id)
    //    //    {
    //    //        return item;
    //    //    }
    //    //}
    //    //return null;

    //    if (itemDic.ContainsKey(id))
    //        return itemDic[id];

    //    return null;
    //}

    //public Item GetRandomItem()
    //{
    //    if (ItemDatas.Count <= 0)
    //        return null;

    //    return ItemDatas[Random.Range(0, ItemDatas.Count)];
    //}
}
