using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    
    Dictionary<int, Sprite> portraitData;
    public NPCDatabase npcDatabase;
    public Sprite[] portraitArr;
    private void Awake()
    {

        
        portraitData = new Dictionary<int, Sprite>();
      
    }

    void GenerateData()
    {

       

        portraitData.Add(1001, portraitArr[0]);
        portraitData.Add(1101, portraitArr[1]);
        portraitData.Add(1201, portraitArr[2]);
        portraitData.Add(1301, portraitArr[3]);

        // �������� �������� ǥ���� ������ �ֵ��� �ϱ����� NPC ��ȣ�� NCP 1��� 100~1000�� �Ҵ��ϸ� ��������
    }
    public string GetTalk(int _id, int _talkIndex)
    {

        if (_talkIndex == npcDatabase.NPCInfos[_id].conversation.Length)
            return null;
        else
            return npcDatabase.NPCInfos[_id].conversation[_talkIndex];
    }
    public Sprite GetPortrait(int _id, int portraitIndex)
    {
        //return portraitData[id + portraitIndex];
        return portraitData[_id];
    }
    
}



