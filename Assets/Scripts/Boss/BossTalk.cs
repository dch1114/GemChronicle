using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class BossTalk : MonoBehaviour
{
    private string[] TalkData;
    private int[] PortraitId;
    //public Sprite[] PortraitImg;
    public TalkManager talkManager;
    public int TalkIndex = 0;
    public Sprite BossPotrait;
    // Start is called before the first frame update
    void Start()
    {

  
    }

    public void Bosstalk(int TalkIndex)
    {

        TalkData = new string[5];
        PortraitId = new int[5];
        TalkData[0] = "�� �� ���ϱ��� ��������.";
        TalkData[1] = "�� ���ϱ��� �ϰ� ����";
        TalkData[2] = "��¿������... �ʰ� �̰��.";
        TalkData[3] = "�����Ѵ�.";
        TalkData[4] = "�߰���";

        PortraitId[0] = 1;
        PortraitId[1] = 2;
        PortraitId[2] = 1;
        PortraitId[3] = 1;
        PortraitId[4] = 2;

        UIManager.Instance.SetPlayerPortraitImage(talkManager.GetPlayerSprite());
        UIManager.Instance.SetNpcPortraitImage(BossPotrait);
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

        return;
    }
}
