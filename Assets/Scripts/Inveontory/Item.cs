using System.Text;
using UnityEngine;

public enum SlotType // ������ ��
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
    public int ID; // ������ ��ũ��Ʈ�� �������� �����ؾ� �Ѵ�
    public string Name;
    public int Damage;
    public int Armor;
    public int Recovery; // ����ȸ����
    public string Description;
    public ItemType ItemType;
    public SlotType SlotType;
    public int Price;
    public int Quantity;

    //test
    //public bool isSellable = false;
    private int descriptionLength;
    private StringBuilder sb = new StringBuilder();
    public Sprite sprite;

    public string GetDescription()
    {
        sb.Length = 0;
        descriptionLength = 0;

        AddItemDescription(Damage, "Damage");
        AddItemDescription(Armor, "Armor");
        AddItemDescription(Recovery, "Recovery");
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
