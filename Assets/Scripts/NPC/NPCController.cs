using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using static UnityEditor.Progress;

//0315 인터페이스 구현
public class NPCController : MonoBehaviour, IInteractive
{
    [SerializeField]
    private NPC npcData;
    private NPCManager npcManager; // NPCManager의 역참조를 받을 필드 추가

    TalkManager talkManager;
    PlayerInput playerinput;

    //Npc타입을 미리 프리팹에서 지정해놈
    public NPCType npcType;
    //UiManager를 미리 참조
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


    //UiManager로 이동
    //public Text talkText;
    //UiManager로 이동
    //public Image portraitImg;
    //제거
    //public static NPCInteractive instance = null;
    //제거
    //public PlayerController playerController;


    public void Init(NPC npc, NPCManager manager)
    {
        npcData = npc;
        npcManager = manager; // NPCManager의 역참조를 받음
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
    //현재 Npc위치값을 가져오기
    public Vector3 GetPosition()
    {
        return transform.position;
    }
    //Npc타입에 따라 대화창 열기
    public void OpenUI()
    {
        //대화창 열기 전에 닫기(Npc가 겹쳐있는 상태이고 플레이어의 이동에 따라 제일가까운 Npc가 달라지기 때문에 이 메소드로 들어오면 무조건 모든 팝업창을 닫고 시작
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
    //Npc타입에 따라 대화창 닫기
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
        //GameManager gameManager = FindObjectOfType<GameManager>(); // 게임 매니저 찾기

        //if (gameManager != null)
        //{
        //    GameObject player = gameManager.GetPlayer(); // 게임 매니저를 통해 플레이어 얻기
        //    TryTalk();
        //}
    }
    public void TryTalk()
    {
        //만약이 상점 팝업이 열려 있는 상태라면 탭키를 눌렀을 때 현재 선택되어 있는 메뉴를 실행한다
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

        //Npc초상회 이미지 세팅
        uiManager.SetNpcPortraitImage(talkManager.GetPortrait(npcData.ID));
        //Player초상화 이미지 세팅
        uiManager.SetPlayerPortraitImage(talkManager.GetPlayerSprite());
        //초상화 이미지 ON
        uiManager.PotraitPanelOnOff(true);

        Talk();

    }

    void Talk()
    {
        while (true)
        {
            //NPC와 PLAYER 둘다 더이상 할 대화가 남아있지 않아 대화를 종료해야 한다면
            if (talkIndex >= npcMsgList.Count && talkIndex >= playerMsgList.Count)
            {
                isEndTalk = true;
                playerinput.OnEnable();
                talkIndex = 0;
                talkNpcStep = 0;
                talkPlayerStep = 0;

                //초상화 OFF
                uiManager.PotraitPanelOnOff(false);

                if (npcType == NPCType.Shop)
                {
                    //플레이어 이동 불가
                    playerinput.OnDisable();
                    //상점 팝업창 ON
                    uiManager.shopChoiceOnOff(true);
                }

                return;
            }

            // 현재 대화 주체가 NPC인지 판별
            bool isNPCSpeaking = (sayingNPC && talkIndex < npcMsgList.Count && talkNpcStep < npcMsgList[talkIndex].Length);

            // 플레이어 대화 메시지가 비어있는지 확인
            bool playerHasMessages = talkIndex < playerMsgList.Count && playerMsgList[talkIndex].Length > 0;

            // 대화 메시지 선택
            string[] currentMessages;
            int currentStep;

            //NPC가 말할때
            if (isNPCSpeaking)
            {
                currentMessages = npcMsgList[talkIndex];
                currentStep = talkNpcStep;
            }
            else if (playerHasMessages)//플레이어가 말할 내용이 있을때
            {
                currentMessages = playerMsgList[talkIndex];
                currentStep = talkPlayerStep;
            }
            else
            {
                // 플레이어가 말할 내용이 없을 때는 다음 대화로 이동
                talkIndex++;
                sayingNPC = true; // 다음 대화는 NPC가 시작
                continue;
            }

            // 대화 메시지 출력
            if (currentStep < currentMessages.Length)
            {
                uiManager.SetTalkMessage(currentMessages[currentStep]);
                uiManager.ShowNpcPotrait(isNPCSpeaking);
                uiManager.ShowPlayerPotrait(!isNPCSpeaking);

                // 대화 스텝 증가
                if (isNPCSpeaking)
                    talkNpcStep++;
                else
                    talkPlayerStep++;
            }

            // 대화가 끝났으면 다음 대화로 이동
            if (currentStep >= currentMessages.Length)
            {
                // 다음 대화 인덱스로 이동
                talkIndex++;

                // 다음 대화 주체 설정
                sayingNPC = !sayingNPC;

                // 대화 스텝 초기화
                if (isNPCSpeaking)
                    talkNpcStep = 0;
                else
                    talkPlayerStep = 0;

                // 다음 대화로 이동
                continue;
            }

            break;

        }

        //if (sayingNPC && talkIndex + 1 < npcMsgList.Count)
        //{

        //    if (npcMsgList[talkIndex].Length > 0)
        //    {
        //        //Npc가 말하는 중이고 아직 더 할말이 있다면
        //        uiManager.SetTalkMessage(npcMsgList[talkIndex][talkNpcStep]);
        //        uiManager.ShowNpcPotrait(true);
        //        uiManager.ShowPlayerPotrait(false);
        //        talkNpcStep++;

        //        //대화 문장이 끝이 났다면
        //        if (talkNpcStep >= npcMsgList[talkIndex].Length)
        //        {
        //            //Player가 말할 차례 설정
        //            sayingNPC = false;
        //            //Player가 이미 할말을 다했다면
        //            if (talkPlayerStep >= playerMsgList[talkIndex].Length || playerMsgList[talkIndex].Length <= 0)
        //            {
        //                //다음 대화 문장으로
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
        ////Player가 말 할 차례이고 문장이 더 존재한다면
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
        //            //Npc가 말할 차례 설정
        //            sayingNPC = true;
        //            //Npc가 이미 할말을 다했다면
        //            if (talkNpcStep >= npcMsgList[talkIndex].Length || npcMsgList[talkIndex].Length <= 0)
        //            {
        //                //다음 대화 문장으로
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

    //대화내용이 더 있는지 체크
    bool CheckConversationCount()
    {
        //NPC와 PLAYER 둘다 더이상 할 대화가 남아있지 않다면
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
        //    //    //상점 팝업창
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
