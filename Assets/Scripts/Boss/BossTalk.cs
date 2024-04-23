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
        TalkData[0] = "�� �� ���ϱ��� ��������."; //����
        TalkData[1] = "�� ���ϱ��� �ϰ� ����";     //�÷��̾�
        TalkData[2] = "��¿������... �ʰ� �̰��.";//����
        TalkData[3] = "�����Ѵ�.";//����
        TalkData[4] = "�߰���";//�÷��̾�

        PortraitId[0] = 1;
        PortraitId[1] = 2;
        PortraitId[2] = 1;
        PortraitId[3] = 1;
        PortraitId[4] = 2;

        UIManager.Instance.SetPlayerPortraitImage(talkManager.GetPlayerSprite());
        UIManager.Instance.SetNpcPortraitImage(talkManager.GetPortrait(8000));
    }

    public void Bosstalk()
    {
       
    }
}
