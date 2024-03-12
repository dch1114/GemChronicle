using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    Dictionary<int, Sprite> portraitData;
    public NPCDatabase npcDatabase;
    public Sprite[] portraitArr;
    private void Awake()
    {
        
        talkData = new Dictionary<int, string[]>();
        portraitData = new Dictionary<int, Sprite>();
        
    }

    void GenerateData()
    {

        talkData.Add(1001, npcDatabase.GetNPCByKey(1001).conversation);
        talkData.Add(1101, npcDatabase.GetNPCByKey(1101).conversation);
        talkData.Add(1201, npcDatabase.GetNPCByKey(1201).conversation);
        talkData.Add(1301, npcDatabase.GetNPCByKey(1301).conversation);

        portraitData.Add(1001, portraitArr[0]);
        portraitData.Add(1101, portraitArr[1]);
        portraitData.Add(1201, portraitArr[2]);
        portraitData.Add(1301, portraitArr[3]);

        // 실전에선 여러가지 표정을 넣을수 있도록 하기위해 NPC 번호를 NCP 1명당 100~1000씩 할당하면 좋을듯함
    }
    public string GetTalk(int _id, int _talkIndex)
    {
        GenerateData();
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


// 엑셀을 이용해서 하거나
