using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class BossTalk : MonoBehaviour
{
    public string[] TalkData;
    public int[] PortraitId;
    public Sprite[] PortraitImg;
    TalkManager talkManager;
    // Start is called before the first frame update
    void Start()
    {
        TalkData[0] = "와 너 강하구나 내가졌다."; //보스
        TalkData[1] = "넌 약하구나 니가 졌어";     //플레이어
        TalkData[2] = "어쩔수없지... 너가 이겼다.";//보스
        TalkData[3] = "축하한다.";//보스
        TalkData[4] = "잘가라";//플레이어

        UIManager.Instance.SetPlayerPortraitImage(talkManager.GetPlayerSprite());
    }

    public void Bosstalk()
    {
       
    }
}
