using System.Collections.Generic;
using System.Text;
using UnityEngine;


public enum SlotType // ������ ��
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
    public int ID; // ������ ��ũ��Ʈ�� �������� �����ؾ� �Ѵ�
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
    public List<Item> ItemDatas;        // �̸��� �߿�!!
    public Dictionary<int, Item> itemDic = new();

    public void Initialize() // �ٽ� �����ϸ鼭 �̹��� ó��? ���̽����� ��θ� ������ �ְ� ���⼭ �İ����� �ϸ�ȴ�.
    {
        foreach (Item item in ItemDatas)
        {
            itemDic.Add(item.ID, item);
            item.sprite = Resources.Load<Sprite>("Sprites/item" + item.ID); //������ �ƴ϶� ��ζ�� �̸� ������ ���� �ʰڳ�.
            // ��θ� �˰� ������ ���ҽ� �ε��ؼ� ��������Ʈ�� �̸� ���� ���������Ϳ� ��������Ʈ �н��� ���´�. �׷��� ���ҽ� �ε带 �� �� �ִ�. 
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
