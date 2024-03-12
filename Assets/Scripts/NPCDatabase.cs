using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class NPC
{
    public int ID; // �̸��� �����ؾ� �Ѵ�
    public string role;
    public string name;
    public string place;
    public string discription;
    public string []conversation;
    public bool isNPC;

}

public class NPCInstance
{
    int no;
    public NPC npc;
}


[System.Serializable]
public class NPCDatabase
{
    public List<NPC> NPCInfos;        // �̸��� �߿�!!
    public Dictionary<int, NPC> npcDic = new();

    public void Initialize()
    {
        foreach (NPC npc in NPCInfos)
        {
            npcDic.Add(npc.ID, npc);
        }
    }

    public NPC GetNPCByKey(int id)
    {
        //foreach (Item item in ItemInfos)
        //{
        //    if (item.ID == id)
        //    {
        //        return item;
        //    }
        //}
        //return null;

        if (npcDic.ContainsKey(id))
            return npcDic[id];

        return null;
    }

    public NPC GetRandomItem()
    {
        if (NPCInfos.Count <= 0)
            return null;

        return NPCInfos[Random.Range(0, NPCInfos.Count)];
    }
}
