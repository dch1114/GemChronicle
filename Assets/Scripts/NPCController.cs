using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class NPCController : MonoBehaviour
{
    [SerializeField]
    private NPC npcData;
    //public int NPCID;

    public void SetNpcData(NPC npc)  //�ʱ�ȭ�� ���ְ� �Ŵ����� npc�� �ʱ�ȭ�ϸ鼭 �ڱ⸦ �������ϸ鼭 ������
    { 
        npcData = npc;
    }

    public NPC GetNpcData()
    {
        return npcData;
    }
    
    
}
