using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class NPCController : MonoBehaviour
{
    [SerializeField]
    private NPC npcData;
    NPCManager npcManager; // NPCManager의 역참조를 받을 필드 추가

    public void Init(NPC npc, NPCManager manager)
    {
        npcData = npc;
        npcManager = manager; // NPCManager의 역참조를 받음
    }

    public NPC GetNpcData()
    {
        return npcData;
    }
    
    
}
