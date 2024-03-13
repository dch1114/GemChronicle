using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class NPCController : MonoBehaviour
{
    [SerializeField]
    private NPC npcData;
    NPCManager npcManager; // NPCManager�� �������� ���� �ʵ� �߰�

    public void Init(NPC npc, NPCManager manager)
    {
        npcData = npc;
        npcManager = manager; // NPCManager�� �������� ����
    }

    public NPC GetNpcData()
    {
        return npcData;
    }
    
    
}
