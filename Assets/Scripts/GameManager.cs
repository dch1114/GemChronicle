using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //public Text talkText;
    //public GameObject scanObject;
    //public GameObject talkPanel;
    //public bool isAction;
    //public TalkManager talkManager;


    //public int talkIndex;
    //public Image portraitImg;
    //public void Action(GameObject _scanobj)
    //{


    //        scanObject = _scanobj;
    //    NPCController NpcData = scanObject.GetComponent<NPCController>();

    //    Talk(NpcData.NPCID, true);


    //   talkPanel.SetActive(isAction);

    //}


    //void Talk(int id, bool isNpc)
    //{
    //    string talkData = talkManager.GetTalk(id, talkIndex);

    //    if (talkData == null)
    //    {
    //        isAction = false;
    //        talkIndex = 0;

    //        return;
    //    }
    //    if (isNpc)
    //    {
    //        talkText.text = talkData;

    //        portraitImg.sprite = talkManager.GetPortrait(id, int.Parse( talkData));
    //        portraitImg.color = new Color(1, 1, 1, 1);
    //    }
    //    else
    //    {
    //        talkText.text = talkData;

    //        portraitImg.color = new Color(1, 1, 1, 0);
    //    }

    //    isAction = true;
    //    talkIndex++;
    //}
}
