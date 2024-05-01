using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemDatabase
{
    public List<Item> ItemDatas; // �̸��� �߿�!!
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
}
