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
        talkData.Add(1001, new string[] { "안녕 ? :0", "난 NPC1이야 :0" });
        talkData.Add(1101, new string[] { "안녕:0", "잘부탁해:0" });
        talkData.Add(1201, new string[] { "난 NPC3란다:0" });
        talkData.Add(1301, new string[] { "하이:0", "난 npc4야:0" });

        portraitData.Add(1001, portraitArr[0]);
        portraitData.Add(1101, portraitArr[1]);
        portraitData.Add(1201, portraitArr[2]);
        portraitData.Add(1301, portraitArr[3]);

        // 실전에선 여러가지 표정을 넣을수 있도록 하기위해 NPC 번호를 NCP 1명당 100~1000씩 할당하면 좋을듯함
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


// 엑셀을 이용해서 하거나
