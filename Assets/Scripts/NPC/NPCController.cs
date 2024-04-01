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

    public QuestData offerQuestData;

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

    // ��ȭ�� ���� ����Ʈ�� �����ϰ� ������ �� ȣ��Ǵ� �޼���
    public void OfferQuestToPlayer()
    {
        // �÷��̾� ����Ʈ �Ŵ������� ����Ʈ�� ����
        PlayerQuestManager.instance.OfferQuest(offerQuestData);
    }

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
    }

    public void TryTalk()
    {
        //������ ���� �˾��� ���� �ִ� ���¶�� ��Ű�� ������ �� ���� ���õǾ� �ִ� �޴��� �����Ѵ�
        if (uiManager.IsOpenShowPopup())
        {
            uiManager.RunSelectedMenuButton();
            return;
        }
        //Npc�ʻ�ȸ �̹��� ����
        uiManager.SetNpcPortraitImage(talkManager.GetPortrait(npcData.ID));
        //Player�ʻ�ȭ �̹��� ����
        uiManager.SetPlayerPortraitImage(talkManager.GetPlayerSprite());
        //�ʻ�ȭ �̹��� ON
        uiManager.PotraitPanelOnOff(true);

        Talk();

    }

    void Talk()
    {
        while (true)
        {
            //NPC�� PLAYER �Ѵ� ���̻� �� ��ȭ�� �������� �ʾ� ��ȭ�� �����ؾ� �Ѵٸ�
            if (talkIndex >= npcMsgList.Count && talkIndex >= playerMsgList.Count)
            {
                //Debug.Log("NPC�� PLAYER �Ѵ� ���̻� �� ��ȭ�� �������� ����");
                playerinput.OnEnable();
                talkIndex = 0;
                talkNpcStep = 0;
                talkPlayerStep = 0;

                //�ʻ�ȭ OFF
                uiManager.PotraitPanelOnOff(false);

                if (npcType == NPCType.Shop)
                {
                    //�÷��̾� �̵� �Ұ�
                    playerinput.OnDisable();
                    //���� �˾�â ON
                    uiManager.shopChoiceOnOff(true);
                }

                break;
            }

            // �÷��̾� ��ȭ �޽����� ����ִ��� Ȯ��
            bool playerHasMessages = talkIndex < playerMsgList.Count && playerMsgList[talkIndex].Length > 0;
            // NPC ��ȭ �޽����� ����ִ��� Ȯ��
            bool npcHasMessages = talkIndex < npcMsgList.Count && npcMsgList[talkIndex].Length > 0;
            //Debug.Log("PLAYER ��ȭ �޽����� �ִ°�? -> " + playerHasMessages);
            //Debug.Log("NPC ��ȭ �޽����� �ִ°�? -> " + npcHasMessages);

            // ��ȭ �޽��� ����
            string[] currentMessages;
            int currentStep;

            if (sayingNPC && npcHasMessages)//NPC�� ��ȭ�޼����� �����ϰ� ���ϴ����� ��
            {
                currentMessages = npcMsgList[talkIndex];
                currentStep = talkNpcStep;
            }
            else if (!sayingNPC && playerHasMessages)//PLAYER�� ��ȭ�޼����� �����ϰ� ���ϴ����� ��
            {
                currentMessages = playerMsgList[talkIndex];
                currentStep = talkPlayerStep;
            }
            else//NPC �Ǵ� PLAYER�� ��ȭ�޼����� ������
            {
                //Debug.Log("NPC + PLAYER ���� �ϳ��� ��ȭ�޼����� ����");
                sayingNPC = !sayingNPC;
                continue;
            }

            //Debug.Log("sayingNPC:" + sayingNPC);
            //Debug.Log("currentStep:" + currentStep);
            // ��ȭ �޽��� ���
            if (currentStep < currentMessages.Length)
            {
                //Text��� �� �ʻ�ȭUI ������Ʈ
                uiManager.SetTalkMessage(currentMessages[currentStep]);
                Debug.Log(currentMessages[currentStep]);
                uiManager.ShowNpcPotrait(sayingNPC);
                uiManager.ShowPlayerPotrait(!sayingNPC);

                
                if (sayingNPC)
                {
                    talkNpcStep++;

                    // NPC�� ��� ������ ����� ��� ��ȭ ���� �ʱ�ȭ
                    if (talkNpcStep >= npcMsgList[talkIndex].Length)
                    {
                        talkNpcStep = 0;


                        // �÷��̾����� ���� ��ȭ������ �ִٸ�
                        if (playerHasMessages)
                        {
                            sayingNPC = false;
                        }
                        else//�÷��̾����� ���� ��ȭ������ ���ٸ�
                        {
                            // ��ȭ �ε��� ����
                            talkIndex++;
                        }
                    }
                }
                else
                {
                    talkPlayerStep++;
                    //Debug.Log("talkPlayerStep" + talkPlayerStep);
                    //Debug.Log("playerMsgList[talkIndex].Length" + playerMsgList[talkIndex].Length);
                    // �÷��̾ ��� ������ ����� ��� ��ȭ ���� �ʱ�ȭ
                    if (talkPlayerStep >= playerMsgList[talkIndex].Length)
                    {
                        Debug.Log("��������");
                        talkPlayerStep = 0;
                        sayingNPC = true;
                        talkIndex++;

                    }
                }

            }
            
            // ��ȭ ó�� �� ����
            break;
        }
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
