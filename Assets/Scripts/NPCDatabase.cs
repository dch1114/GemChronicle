using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class NPC
{
    public int ID; // 이름이 동일해야 한다
    public string Name;
    public int Attack;
    public int Gold;
    public string Description;
}

public class NPCInstance
{
    int no;
    public NPC npc;
}


[System.Serializable]
public class NPCDatabase
{
    public List<NPC> NPCInfos;        // 이름이 중요!!
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
