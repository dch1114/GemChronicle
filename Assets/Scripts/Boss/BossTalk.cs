using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class BossTalk : MonoBehaviour
{
    public string[] TalkData;
    public int[] PortraitId;
    //public Sprite[] PortraitImg;
    TalkManager talkManager;
    public int TalkIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        TalkData[0] = "와 너 강하구나 내가졌다."; //보스
        TalkData[1] = "넌 약하구나 니가 졌어";     //플레이어
        TalkData[2] = "어쩔수없지... 너가 이겼다.";//보스
        TalkData[3] = "축하한다.";//보스
        TalkData[4] = "잘가라";//플레이어

        PortraitId[0] = 1;
        PortraitId[1] = 2;
        PortraitId[2] = 1;
        PortraitId[3] = 1;
        PortraitId[4] = 2;

        UIManager.Instance.SetPlayerPortraitImage(talkManager.GetPlayerSprite());
        UIManager.Instance.SetNpcPortraitImage(talkManager.GetPortrait(8000));
    }

    public void Bosstalk(int TalkIndex)
    {
        if (TalkIndex < TalkData.Length)
        {
    
            UIManager.Instance.SetTalkMessage(TalkData[TalkIndex]);
            UIManager.Instance.PotraitPanelOnOff(true);
            if (PortraitId[TalkIndex] == 1)
            {
                UIManager.Instance.ShowNpcPotrait(true);
                UIManager.Instance.ShowPlayerPotrait(false);
            }
            else
            {
                UIManager.Instance.ShowNpcPotrait(false);
                UIManager.Instance.ShowPlayerPotrait(true);
            }
        }
        else
        {
            UIManager.Instance.PotraitPanelOnOff(false);
        }
    }
}
