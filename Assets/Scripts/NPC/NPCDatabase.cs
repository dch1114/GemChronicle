using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class NPC
{
    public int ID; // 이름이 동일해야 한다
    public string role;
    public string name;
    public string place;
    public string discription;
    public bool isNPC;
    public int portraitId;
}
[System.Serializable]
public class QuestTableData
{
    public int id;
    public int talk_1;
    public int talk_2;
    public int talk_3;
}
[System.Serializable]
public class TalkTableData
{
    public int id;
    public int[] scriptId;
}
[System.Serializable]
public class ScriptTableData
{
    public int id;
    public int speaker;
    public string[] dialog;

}


//0315 Npc타입을 구별하기 위해 enum 선언
public enum NPCType
{ 
    Teacher,
    Friend,
    SubNpc,
    Shop,
    Healer,
    Diary

}
public class NPCInstance
{
   
    public NPC npc;
}


[System.Serializable]
public class NPCDatabase
{
    public List<NPC> NPCInfos;        // 이름이 중요!!
    public Dictionary<int, NPC> npcDic = new();

    public List<QuestTableData> questTable;
    public Dictionary<int, QuestTableData> questTableDic = new();

    public List<TalkTableData> talkTable;
    public Dictionary<int, TalkTableData> talkTableDic = new();

    public List<ScriptTableData> dialogTable;
    public Dictionary<int, ScriptTableData> dialogTableDic = new();

    public void Initialize()
    {
        foreach (NPC npc in NPCInfos)
        {
            npcDic.Add(npc.ID, npc);
        }
        foreach (QuestTableData quest in questTable)
        {
            questTableDic.Add(quest.id, quest);
        }
        foreach (TalkTableData talk in talkTable)
        {
            //Debug.Log(talk.id);
            talkTableDic.Add(talk.id, talk);
        }
        foreach (ScriptTableData script in dialogTable)
        {
            //Debug.Log(script.id);
            dialogTableDic.Add(script.id, script);
        }

    }

    public NPC GetNPCByKey(int id)
    {

        if (npcDic.ContainsKey(id))
        {
            return npcDic[id];
        }

        return null;
    }
    public QuestTableData GetQuestByKey(int id)
    {

        if (questTableDic.ContainsKey(id))
            return questTableDic[id];

        return null;
    }
    public TalkTableData GetTalkByKey(int id)
    {

        if (talkTableDic.ContainsKey(id))
            return talkTableDic[id];

        return null;
    }
    public ScriptTableData GetScriptByKey(int id)
    {

        if (dialogTableDic.ContainsKey(id))
            return dialogTableDic[id];

        return null;
    }

}