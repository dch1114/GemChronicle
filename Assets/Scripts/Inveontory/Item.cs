using System.Text;
using UnityEngine;

public enum SlotType
{
    Weapon,
    Armor,
    Potion,
}

public enum ItemType
{
    Potion,
    Equipment
}

[System.Serializable]
public class Item
{
    public int ID; // 엑셀과 스크립트의 변수명이 동일해야 한다
    public string Name;
    public int Damage;
    public int Armor;
    public int Recovery; // 포션회복량
    public string Description;
    public ItemType ItemType;
    public SlotType SlotType;
    public int Price;
    public int Quantity;

    private int descriptionLength;
    private StringBuilder sb = new StringBuilder();
    public Sprite sprite;

    public string GetDescription()
    {
        sb.Length = 0;
        descriptionLength = 0;

        AddItemDescription(Damage, "공격력");
        AddItemDescription(Armor, "방어력");
        AddItemDescription(Recovery, "회복");
        AddItemDescription(Description, "설명");
        AddItemDescription(Price, "가격");


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
