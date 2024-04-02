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
    public string[] doMessage;
    public string[] doingMessage;
    public string[] doneMessage;
    public bool isNPC;

    public string[] npc1;
    public string[] npc2;
    public string[] npc3;
    public string[] player1;
    public string[] player2;
    public string[] player3;

}

//0315 NpcŸ���� �����ϱ� ���� enum ����
public enum NPCType
{ 
    Npc,
    Shop
}
public class NPCInstance
{
   
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
      
        if (npcDic.ContainsKey(id))
            return npcDic[id];

        return null;
    }

  
}