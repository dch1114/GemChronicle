using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class NPCController : MonoBehaviour
{
    [SerializeField]
    private NPC npcData;
    //public int NPCID;

    public void SetNpcData(NPC npc)  //초기화로 해주고 매니저가 npc를 초기화하면서 자기를 역참조하면서 보내기
    { 
        npcData = npc;
    }

    public NPC GetNpcData()
    {
        return npcData;
    }
    
    
}
