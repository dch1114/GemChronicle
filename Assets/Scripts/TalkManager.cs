using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    Dictionary<int, Sprite> portraitData;

    public Sprite[] portraitArr;
    private void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        portraitData = new Dictionary<int, Sprite>();
        GenerateData();
    }

    void GenerateData()
    {
        talkData.Add(1001, new string[] { "�ȳ� ? :0", "�� NPC1�̾� :0" });
        talkData.Add(1101, new string[] { "�ȳ�:0", "�ߺ�Ź��:0" });
        talkData.Add(1201, new string[] { "�� NPC3����:0" });
        talkData.Add(1301, new string[] { "����:0", "�� npc4��:0" });

        portraitData.Add(1001, portraitArr[0]);
        portraitData.Add(1101, portraitArr[1]);
        portraitData.Add(1201, portraitArr[2]);
        portraitData.Add(1301, portraitArr[3]);

        // �������� �������� ǥ���� ������ �ֵ��� �ϱ����� NPC ��ȣ�� NCP 1��� 100~1000�� �Ҵ��ϸ� ��������
    }
    public string GetTalk(int _id, int _talkIndex)
    {
        if (_talkIndex == talkData[_id].Length)
            return null;
        else
            return talkData[_id][_talkIndex];
    }
    public Sprite GetPortrait(int _id, int portraitIndex)
    {
        //return portraitData[id + portraitIndex];
        return portraitData[_id];
    }
}


// ������ �̿��ؼ� �ϰų�
