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


    private void Start()
    {
        npcDatabase = DataManager.instance.npcDatabase;
        GenerateData();
    }

    void GenerateData()
    {

        portraitData.Add(1001, portraitArr[0]);
        portraitData.Add(1101, portraitArr[1]);
        portraitData.Add(1201, portraitArr[2]);
        portraitData.Add(1301, portraitArr[3]);

        // 실전에선 여러가지 표정을 넣을수 있도록 하기위해 NPC 번호를 NCP 1명당 100~1000씩 할당하면 좋을듯함
    }
    public string GetTalk(int _id, int _talkIndex)
    {
        Debug.Log("_id : " + _id);

        Debug.Log("_talkIndex : " + _talkIndex);
        //if (_talkIndex == npcDatabase.NPCInfos[_id].conversation.Length)
        //    return null;
        //else
        //    return npcDatabase.NPCInfos[_id].conversation[_talkIndex];
        if (_talkIndex == npcDatabase.GetNPCByKey(_id).conversation.Length)
            return null;
        else 
            return npcDatabase.GetNPCByKey(_id).conversation[_talkIndex];

    }
    public Sprite GetPortrait(int _id)
    {
        return portraitData[_id];
    }
    
}



