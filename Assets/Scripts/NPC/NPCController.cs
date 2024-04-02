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

    public QuestData offerQuestData;

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

    // 대화를 통해 퀘스트를 제안하고 수락할 때 호출되는 메서드
    public void OfferQuestToPlayer()
    {
        // 플레이어 퀘스트 매니저에게 퀘스트를 제안
        PlayerQuestManager.instance.OfferQuest(offerQuestData);
    }

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
    }

    public void TryTalk()
    {
        //만약이 상점 팝업이 열려 있는 상태라면 탭키를 눌렀을 때 현재 선택되어 있는 메뉴를 실행한다
        if (uiManager.IsOpenShowPopup())
        {
            uiManager.RunSelectedMenuButton();
            return;
        }
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
                //Debug.Log("NPC와 PLAYER 둘다 더이상 할 대화가 남아있지 않음");
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

                break;
            }

            // 플레이어 대화 메시지가 비어있는지 확인
            bool playerHasMessages = talkIndex < playerMsgList.Count && playerMsgList[talkIndex].Length > 0;
            // NPC 대화 메시지가 비어있는지 확인
            bool npcHasMessages = talkIndex < npcMsgList.Count && npcMsgList[talkIndex].Length > 0;
            //Debug.Log("PLAYER 대화 메시지가 있는가? -> " + playerHasMessages);
            //Debug.Log("NPC 대화 메시지가 있는가? -> " + npcHasMessages);

            // 대화 메시지 선택
            string[] currentMessages;
            int currentStep;

            if (sayingNPC && npcHasMessages)//NPC의 대화메세지가 존재하고 말하는중일 때
            {
                currentMessages = npcMsgList[talkIndex];
                currentStep = talkNpcStep;
            }
            else if (!sayingNPC && playerHasMessages)//PLAYER의 대화메세지가 존재하고 말하는중일 때
            {
                currentMessages = playerMsgList[talkIndex];
                currentStep = talkPlayerStep;
            }
            else//NPC 또는 PLAYER의 대화메세지가 없을때
            {
                //Debug.Log("NPC + PLAYER 둘중 하나의 대화메세지가 없음");
                sayingNPC = !sayingNPC;
                continue;
            }

            //Debug.Log("sayingNPC:" + sayingNPC);
            //Debug.Log("currentStep:" + currentStep);
            // 대화 메시지 출력
            if (currentStep < currentMessages.Length)
            {
                //Text출력 및 초상화UI 업데이트
                uiManager.SetTalkMessage(currentMessages[currentStep]);
                Debug.Log(currentMessages[currentStep]);
                uiManager.ShowNpcPotrait(sayingNPC);
                uiManager.ShowPlayerPotrait(!sayingNPC);

                
                if (sayingNPC)
                {
                    talkNpcStep++;

                    // NPC가 모든 문장을 출력한 경우 대화 스텝 초기화
                    if (talkNpcStep >= npcMsgList[talkIndex].Length)
                    {
                        talkNpcStep = 0;


                        // 플레이어한테 다음 대화내용이 있다면
                        if (playerHasMessages)
                        {
                            sayingNPC = false;
                        }
                        else//플레이어한테 다음 대화내용이 없다면
                        {
                            // 대화 인덱스 증가
                            talkIndex++;
                        }
                    }
                }
                else
                {
                    talkPlayerStep++;
                    //Debug.Log("talkPlayerStep" + talkPlayerStep);
                    //Debug.Log("playerMsgList[talkIndex].Length" + playerMsgList[talkIndex].Length);
                    // 플레이어가 모든 문장을 출력한 경우 대화 스텝 초기화
                    if (talkPlayerStep >= playerMsgList[talkIndex].Length)
                    {
                        Debug.Log("문단종료");
                        talkPlayerStep = 0;
                        sayingNPC = true;
                        talkIndex++;

                    }
                }

            }
            
            // 대화 처리 후 종료
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
