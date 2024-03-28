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
<<<<<<< Updated upstream

        isEndTalk = false;

        if (isEndTalk == true)
        {
            return;
        }


        //Talk(GetNpcData().ID);

=======
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
                isEndTalk = true;
=======
                //Debug.Log("NPC�� PLAYER �Ѵ� ���̻� �� ��ȭ�� �������� ����");
>>>>>>> Stashed changes
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

<<<<<<< Updated upstream
                return;
            }

            // ���� ��ȭ ��ü�� NPC���� �Ǻ�
            bool isNPCSpeaking = (sayingNPC && talkIndex < npcMsgList.Count && talkNpcStep < npcMsgList[talkIndex].Length);

            // �÷��̾� ��ȭ �޽����� ����ִ��� Ȯ��
            bool playerHasMessages = talkIndex < playerMsgList.Count && playerMsgList[talkIndex].Length > 0;

            // ��ȭ �޽��� ����
            string[] currentMessages;
            int currentStep;

            //NPC�� ���Ҷ�
            if (isNPCSpeaking)
            {
                currentMessages = npcMsgList[talkIndex];
                currentStep = talkNpcStep;
            }
            else if (playerHasMessages)//�÷��̾ ���� ������ ������
            {
                currentMessages = playerMsgList[talkIndex];
                currentStep = talkPlayerStep;
            }
            else
            {
                // �÷��̾ ���� ������ ���� ���� ���� ��ȭ�� �̵�
                talkIndex++;
                sayingNPC = true; // ���� ��ȭ�� NPC�� ����
                continue;
            }

            // ��ȭ �޽��� ���
            if (currentStep < currentMessages.Length)
            {
                uiManager.SetTalkMessage(currentMessages[currentStep]);
                uiManager.ShowNpcPotrait(isNPCSpeaking);
                uiManager.ShowPlayerPotrait(!isNPCSpeaking);

                // ��ȭ ���� ����
                if (isNPCSpeaking)
                    talkNpcStep++;
                else
                    talkPlayerStep++;
            }

            // ��ȭ�� �������� ���� ��ȭ�� �̵�
            if (currentStep >= currentMessages.Length)
            {
                // ���� ��ȭ �ε����� �̵�
                talkIndex++;

                // ���� ��ȭ ��ü ����
                sayingNPC = !sayingNPC;

                // ��ȭ ���� �ʱ�ȭ
                if (isNPCSpeaking)
                    talkNpcStep = 0;
                else
                    talkPlayerStep = 0;

                // ���� ��ȭ�� �̵�
                continue;
            }

            break;

        }

        //if (sayingNPC && talkIndex + 1 < npcMsgList.Count)
        //{

        //    if (npcMsgList[talkIndex].Length > 0)
        //    {
        //        //Npc�� ���ϴ� ���̰� ���� �� �Ҹ��� �ִٸ�
        //        uiManager.SetTalkMessage(npcMsgList[talkIndex][talkNpcStep]);
        //        uiManager.ShowNpcPotrait(true);
        //        uiManager.ShowPlayerPotrait(false);
        //        talkNpcStep++;

        //        //��ȭ ������ ���� ���ٸ�
        //        if (talkNpcStep >= npcMsgList[talkIndex].Length)
        //        {
        //            //Player�� ���� ���� ����
        //            sayingNPC = false;
        //            //Player�� �̹� �Ҹ��� ���ߴٸ�
        //            if (talkPlayerStep >= playerMsgList[talkIndex].Length || playerMsgList[talkIndex].Length <= 0)
        //            {
        //                //���� ��ȭ ��������
        //                talkIndex++;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        talkPlayerStep = 0;

        //        sayingNPC = false;

        //        Talk();
        //    }


        //}
        ////Player�� �� �� �����̰� ������ �� �����Ѵٸ�
        //else if (!sayingNPC && talkIndex + 1 < playerMsgList.Count)
        //{

        //    if (playerMsgList[talkIndex].Length > 0)
        //    {

        //        uiManager.SetTalkMessage(playerMsgList[talkIndex][talkPlayerStep]);
        //        uiManager.ShowNpcPotrait(false);
        //        uiManager.ShowPlayerPotrait(true);

        //        talkPlayerStep++;

        //        if (talkPlayerStep >= playerMsgList[talkIndex].Length)
        //        {
        //            //Npc�� ���� ���� ����
        //            sayingNPC = true;
        //            //Npc�� �̹� �Ҹ��� ���ߴٸ�
        //            if (talkNpcStep >= npcMsgList[talkIndex].Length || npcMsgList[talkIndex].Length <= 0)
        //            {
        //                //���� ��ȭ ��������
        //                talkIndex++;
        //            }

        //        }
        //    }
        //    else
        //    {
        //        talkNpcStep = 0;

        //        sayingNPC = false;

        //        Talk();
        //    }


        //}
        Debug.Log("talkIndex:" + talkIndex);


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
=======
                break;
            }

            // �÷��̾� ��ȭ �޽����� ����ִ��� Ȯ��
            bool playerHasMessages = talkIndex < playerMsgList.Count && playerMsgList[talkIndex].Length > 0;
            // NPC ��ȭ �޽����� ����ִ��� Ȯ��
            bool npcHasMessages = talkIndex < npcMsgList.Count && npcMsgList[talkIndex].Length > 0;
            //Debug.Log("PLAYER ��ȭ �޽����� �ִ°�? -> " + playerHasMessages);
            //Debug.Log("NPC ��ȭ �޽����� �ִ°�? -> " + npcHasMessages);
>>>>>>> Stashed changes

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

<<<<<<< Updated upstream



=======
>>>>>>> Stashed changes
    InteractType IInteractive.GetType()
    {
        return InteractType.NPC;
    }
}
