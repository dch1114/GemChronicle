using System.Collections.Generic;
using UnityEngine;

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
