using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    
    Dictionary<int, Sprite> portraitData;
    public NPCDatabase npcDatabase;

    public Sprite[] portraitArr;

    public Sprite playerSprite;

    private void Awake()
    {
        portraitData = new Dictionary<int, Sprite>();
    }


    public void InitTalkManager()
    {
        npcDatabase = DataManager.Instance.npcDatabase;
        GenerateData();
    }

    void GenerateData()
    {

        portraitData.Add(2000, portraitArr[0]);
        portraitData.Add(3001, portraitArr[1]);
        portraitData.Add(3002, portraitArr[2]);
        portraitData.Add(3003, portraitArr[3]);


    }
    //public string GetTalk(int _id, int _talkIndex)
    //{
       
       
    //    if (_talkIndex == npcDatabase.GetNPCByKey(_id).conversation.Length)
    //        return null;
    //    else 
    //        return npcDatabase.GetNPCByKey(_id).conversation[_talkIndex];

    //}

    public NPC NPCDataInfo(int id)
    {
        return npcDatabase.GetNPCByKey(id);
    }


    public Sprite GetPortrait(int _id)
    {
        return portraitData[_id];
    }
    public Sprite GetPlayerSprite()
    {
        return playerSprite;
    }
    
}



