using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class NPCController : MonoBehaviour
{
    [SerializeField]
    private NPC npcData;
    //public int NPCID;

    public void SetNpcData(NPC npc)
    { 
        npcData = npc;
    }

    public NPC GetNpcData()
    {
        return npcData;
    }
    //public void Init()
    //{
       
    //    int NPCKeyToFind = NPCID;

    //    DataManager.instance.npcDatabase.GetNPCByKey(NPCKeyToFind);
    //}
    
}
