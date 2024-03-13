using UnityEngine;
using System.Collections.Generic;
using System;
using Newtonsoft.Json;
using System.IO;
using System.Collections;

[System.Serializable]
public class Quest
{
    public int ID;
    public string Name;
    public int Gold;
    public int Exp;
    public string Description;
}

public class QuestInstance
{
    public Quest quest;
}

[System.Serializable]
public class QuestDatabase
{
    public List<Quest> QuestInfos;
    public Dictionary<int, Quest> npcDic = new();

    public void Initialize()
    {
        foreach (Quest npc in QuestInfos)
        {
            npcDic.Add(npc.ID, npc);
        }
    }

    public Quest GetNPCByKey(int id)
    {
        if (npcDic.ContainsKey(id))
            return npcDic[id];

        return null;
    }
}