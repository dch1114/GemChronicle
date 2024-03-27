using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using static UnityEditor.Progress;

//0315 �������̽� ����
public class NPCController : MonoBehaviour, IInteractive
{
    [SerializeField]
    private NPC npcData;
    private NPCManager npcManager; // NPCManager�� �������� ���� �ʵ� �߰�

    TalkManager talkManager;
    PlayerInput playerinput;

    //NpcŸ���� �̸� �����տ��� �����س�
    public NPCType npcType;
    //UiManager�� �̸� ����
    UIManager uiManager;

    public int talkIndex;

    public int talkNpcStep;
    public int talkPlayerStep;

    public bool isAction;
    public bool isNPC;
    public bool isShop = false;
    public bool isEndTalk = false;

    List<string[]> npcMsgList = new List<string[]>();
    List<string[]> playerMsgList = new List<string[]>();
    private bool sayingNPC = true;


    //UiManager�� �̵�
    //public Text talkText;
    //UiManager�� �̵�
    //public Image portraitImg;
    //����
    //public static NPCInteractive instance = null;
    //����
    //public PlayerController playerController;


    public void Init(NPC npc, NPCManager manager)
    {
        npcData = npc;
        npcManager = manager; // NPCManager�� �������� ����
        uiManager = UIManager.Instance;
        talkManager = npcManager.talkManager;
        playerinput = npcManager.playerinput;

        npcMsgList.Add(npcData.npc1);
        npcMsgList.Add(npcData.npc2);
        npcMsgList.Add(npcData.npc3);
        playerMsgList.Add(npcData.player1);
        playerMsgList.Add(npcData.player2);
        playerMsgList.Add(npcData.player3);

        talkIndex = 0;
        talkNpcStep = 0;
        talkPlayerStep = 0;
    }

    public NPC GetNpcData()
    {
        return npcData;
    }
    //���� Npc��ġ���� ��������
    public Vector3 GetPosition()
    {
        return transform.position;
    }
    //NpcŸ�Կ� ���� ��ȭâ ����
    public void OpenUI()
    {
        //��ȭâ ���� ���� �ݱ�(Npc�� �����ִ� �����̰� �÷��̾��� �̵��� ���� ���ϰ���� Npc�� �޶����� ������ �� �޼ҵ�� ������ ������ ��� �˾�â�� �ݰ� ����
        CloseUI();

        if (npcType == NPCType.Npc)
        {
            uiManager.talkBtnOnOff(true);

        }
        else if (npcType == NPCType.Shop)
        {

            uiManager.talkBtnOnOff(true);

        }
        
    }
    //NpcŸ�Կ� ���� ��ȭâ �ݱ�
    public void CloseUI()
    {
        if (npcType == NPCType.Npc)
        {
            uiManager.talkBtnOnOff(false);

        }
        else if (npcType == NPCType.Shop)
        {
            uiManager.talkBtnOnOff(false);
           

        }
        
    }
    public void Interact()
    {

        TryTalk();
        //GameManager gameManager = FindObjectOfType<GameManager>(); // ���� �Ŵ��� ã��

        //if (gameManager != null)
        //{
        //    GameObject player = gameManager.GetPlayer(); // ���� �Ŵ����� ���� �÷��̾� ���
        //    TryTalk();
        //}
    }
    public void TryTalk()
    {
        //������ ���� �˾��� ���� �ִ� ���¶�� ��Ű�� ������ �� ���� ���õǾ� �ִ� �޴��� �����Ѵ�
        if (uiManager.IsOpenShowPopup())
        {
            uiManager.RunSelectedMenuButton();
            return;
        }

        isEndTalk = false;

        if (isEndTalk == true)
        {
            return;
        }


        //Talk(GetNpcData().ID);

        //Npc�ʻ�ȸ �̹��� ����
        uiManager.SetNpcPortraitImage(talkManager.GetPortrait(npcData.ID));
        //Player�ʻ�ȭ �̹��� ����
        uiManager.SetPlayerPortraitImage(talkManager.GetPlayerSprite());
        //�ʻ�ȭ �̹��� ON
        uiManager.PotraitPanelOnOff(true);


        //���̻� ��ȭ ������ ���� ���� �ʾ� ��ȭ�� �����ؾ� �Ѵٸ�
        if (CheckConversationCount())
        {
            //�ʻ�ȭ OFF
            uiManager.PotraitPanelOnOff(false);

            if (npcType == NPCType.Shop)
            {
                //�÷��̾� �̵� �Ұ�
                playerinput.OnDisable();
                //���� �˾�â ON
                uiManager.shopChoiceOnOff(true);
            }

            return;
        }
 
        Talk();

    }



    void Talk()
    {
        ////��ȭ �ҷ�����
        //int msgId = GetNpcData().ID;
        //string talkData = talkManager.GetTalk(msgId, talkIndex);

        //if (talkData == null)
        //{

        //    talkIndex = 0;
        //    isEndTalk = true;
        //    uiManager.SetTalkMessage(talkData);
        //    uiManager.PotraitPanelOnOff(false);
        //    return;
        //}
        //else
        //{

        //    //isAction = true;
        //    uiManager.PotraitPanelOnOff(true);
        //    uiManager.SetTalkMessage(talkData);
        //    uiManager.SetNpcPortraitImage(talkManager.GetPortrait(msgId));
        //    talkIndex++;

        //}
        //uiManager.SetTalkMessage(talkData);
        //talkIndex++;


        
        if (sayingNPC && talkIndex < npcMsgList.Count)
        {

            if (npcMsgList[talkIndex].Length > 0)
            {
                //Npc�� ���ϴ� ���̰� ���� �� �Ҹ��� �ִٸ�
                uiManager.SetTalkMessage(npcMsgList[talkIndex][talkNpcStep]);
                uiManager.ShowNpcPotrait(true);
                uiManager.ShowPlayerPotrait(false);
                talkNpcStep++;

                //��ȭ ������ ���� ���ٸ�
                if (talkNpcStep >= npcMsgList[talkIndex].Length)
                {
                    //Player�� ���� ���� ����
                    sayingNPC = false;
                    //Player�� �̹� �Ҹ��� ���ߴٸ�
                    if (talkPlayerStep >= playerMsgList[talkIndex].Length)
                    {
                        //���� ��ȭ ��������
                        talkIndex++;
                    }
                }
            }
            else
            {
                talkPlayerStep = 0;

                sayingNPC = false;

                Talk();
            }


        }
        //Player�� �� �� �����̰� ������ �� �����Ѵٸ�
        else if (!sayingNPC && talkIndex < playerMsgList.Count)
        {
            
            if (playerMsgList[talkIndex].Length > 0)
            {
                
                uiManager.SetTalkMessage(playerMsgList[talkIndex][talkPlayerStep]);
                uiManager.ShowNpcPotrait(false);
                uiManager.ShowPlayerPotrait(true);

                talkPlayerStep++;

                if (talkPlayerStep >= playerMsgList[talkIndex].Length)
                {
                    //Npc�� ���� ���� ����
                    sayingNPC = true;
                    //Npc�� �̹� �Ҹ��� ���ߴٸ�
                    if (talkNpcStep >= npcMsgList[talkIndex].Length)
                    {
                        //���� ��ȭ ��������
                        talkIndex++;
                    }

                }
            }
            else
            {
                talkNpcStep = 0;

                sayingNPC = false;

                Talk();
            }


        }



    }

    //��ȭ������ �� �ִ��� üũ
    bool CheckConversationCount()
    {
        //NPC�� PLAYER �Ѵ� ���̻� �� ��ȭ�� �������� �ʴٸ�
        if (talkIndex >= npcMsgList.Count && talkIndex >= playerMsgList.Count)
        {
            isEndTalk = true;
            playerinput.OnEnable();
            talkIndex = 0;
            talkNpcStep = 0;
            talkPlayerStep = 0;

            return true;
        }
        return false;

        //if (talkIndex >= npcData.conversation.Length)
        //{
        //    isEndTalk = true;

        //    playerinput.OnEnable();

        //    //uiManager.PotraitPanelOnOff(false);
        //    talkIndex = 0;

        //    //if (npcType == NPCType.Shop)
        //    //{
        //    //    playerinput.OnDisable();
        //    //    //���� �˾�â
        //    //    uiManager.shopChoiceOnOff(true);
        //    //    uiManager.PotraitPanelOnOff(true);
        //    //}

        //    return true;
        //}
        //return false;
    }

    public void Closer()
    {
        UIManager.Instance.talkBtnText.text = npcData.name;
        OpenUI();
    }




    InteractType IInteractive.GetType()
    {
        return InteractType.NPC;
    }
}
