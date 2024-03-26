using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
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
    public bool isAction;
    public bool isNPC;
    public bool isShop = false;
    public bool isEndTalk = false;

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
        uiManager = UIManager.instance;
        talkManager = npcManager.talkManager;
        playerinput = npcManager.playerinput;
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

        //더이상 대화 내용이 존재 하지 않아 대화를 종료해야 한다면
        if (CheckConversationCount()) return;

        Talk();

    }

   
    void Talk()
    {
        //대화 불러오기
        int msgId = GetNpcData().ID;
        string talkData = talkManager.GetTalk(msgId, talkIndex);


        if (talkData == null)
        {
  
            talkIndex = 0;
            isEndTalk = true;
            uiManager.SetTalkMessage(talkData);
            uiManager.PotraitPanelOnOff(false);
            return;
        }
        else
        {

            //isAction = true;
            uiManager.PotraitPanelOnOff(true);
            uiManager.SetTalkMessage(talkData);
            uiManager.SetPortraitImage(talkManager.GetPortrait(msgId));
            talkIndex++;

        }

     


    }

    //대화내용이 더 있는지 체크
    bool CheckConversationCount()
    {
        if (talkIndex >= npcData.conversation.Length)
        {
            isEndTalk = true;

            playerinput.OnEnable();

            uiManager.PotraitPanelOnOff(false);
            talkIndex = 0;

            if (npcType == NPCType.Shop)
            {
                playerinput.OnDisable();
                //상점 팝업창
                uiManager.shopChoiceOnOff(true);
                uiManager.PotraitPanelOnOff(true);
            }

            return true;
        }
        return false;
    }

    public void Closer()
    {
        UIManager.instance.talkBtnText.text = npcData.name;
        OpenUI();
    }

    public void Interact()
    {
        GameManager gameManager = FindObjectOfType<GameManager>(); // 게임 매니저 찾기

        if (gameManager != null)
        {
            GameObject player = gameManager.GetPlayer(); // 게임 매니저를 통해 플레이어 얻기
            TryTalk();
        }
    }


    InteractType IInteractive.GetType()
    {
        return InteractType.NPC;
    }
}
