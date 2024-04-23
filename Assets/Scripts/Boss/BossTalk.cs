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

        UIManager.Instance.SetPlayerPortraitImage(talkManager.GetPlayerSprite());
    }

    public void Bosstalk()
    {
       
    }
}
