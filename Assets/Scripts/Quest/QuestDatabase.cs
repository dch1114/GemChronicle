using UnityEngine;
using System.Collections.Generic;

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
    int no;
    public Quest quest;
}


[System.Serializable]
public class questDatabase
{
    public List<Quest> QuestInfos;
    public Dictionary<int, Quest> questDic = new();
}
