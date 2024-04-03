using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

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
    public bool isAction;
    public bool isNPC;
    public bool isShop = false;
    public bool isEndTalk = false;

    public QuestData offerQuestData;

    private QuestTableData questData;
    private TalkTableData doQuest;
    private TalkTableData doingQuest;
    private TalkTableData doneQuest;
    [SerializeField] private Queue<ScriptTableData> scriptTableDatas = new Queue<ScriptTableData>();
    ScriptTableData currentScript;

    bool isLoadScriptData = false;

    int currentStep;
    bool isEndSaying = false;
    public void Init(NPC npc, NPCManager manager)
    {
        npcData = npc;
        npcManager = manager; // NPCManager의 역참조를 받음
        uiManager = UIManager.Instance;
        talkManager = npcManager.talkManager;
        playerinput = GameManager.Instance.player.Input;
        SetNPCInfoData(2000);
    }


    void SetNPCInfoData(int index)
    {
        questData = null;
        doQuest = null;
        doingQuest = null;
        doneQuest = null;
        questData = DataManager.Instance.GetQuestTableData(index);
        doQuest = DataManager.Instance.GetTalkTableData(questData.talk_1);
        doingQuest = DataManager.Instance.GetTalkTableData(questData.talk_2);
        doneQuest = DataManager.Instance.GetTalkTableData(questData.talk_3);

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

        isLoadScriptData = false;

    }
    public void Interact()
    {
        TryTalk();
    }

    //퀘스트가 진행중일 때, 퀘스트를 완료하였을때 상태를 알 수 있는 기능이 있어야 함
    public void TryTalk()
    {
        //만약이 상점 팝업이 열려 있는 상태라면 탭키를 눌렀을 때 현재 선택되어 있는 메뉴를 실행한다
        if (uiManager.IsOpenShowPopup())
        {
            uiManager.RunSelectedMenuButton();
            return;
        }
        //대화 스크립트 데이터가 아직 로드된 것이 없다면
        if (!isLoadScriptData)
        {
            //Debug.Log("대화 스크립트 데이터 로드된 것이 없음");
            for (int i = 0; i < doQuest.scriptId.Length; i++)
            {
                scriptTableDatas.Enqueue(DataManager.Instance.GetScriptTableData(doQuest.scriptId[i]));
            }
            currentStep = 0;
            isLoadScriptData = true;
            isEndSaying = true;
        }

        //NPC와 PLAYER 둘다 더이상 할 대화가 남아있지 않아 대화를 종료해야 한다면
        if (scriptTableDatas.Count <= 0)
        {
            //Debug.Log("NPC와 PLAYER 둘다 더이상 할 대화가 남아있지 않음");
            //Debug.Log("이동초기화전");
            playerinput.OnEnable();
            //Debug.Log("이동초기화후");
            currentStep = 0;
            isLoadScriptData = false;
            //초상화 OFF
            uiManager.PotraitPanelOnOff(false);
            isEndSaying = false;

            if (npcType == NPCType.Shop)
            {
                //플레이어 이동 불가
                playerinput.OnDisable();
                //상점 팝업창 ON
                uiManager.shopChoiceOnOff(true);
            }

            return;
        }

        if (isEndSaying)
        {
            //Debug.Log("말하고 있는것이 끝났음");

            var say = scriptTableDatas.Dequeue();
            currentScript = say;
            isEndSaying = false;
            currentStep = 0;
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
        if (currentScript.speaker == 1000)
        {
            uiManager.ShowNpcPotrait(false);
            uiManager.ShowPlayerPotrait(true);
        }
        else
        {
            uiManager.ShowNpcPotrait(true);
            uiManager.ShowPlayerPotrait(false);
        }

        //Debug.Log("sayingNPC:" + sayingNPC);
        //Debug.Log("currentStep:" + currentStep);
        uiManager.SetTalkMessage(currentScript.dialog[currentStep]);

        currentStep++;

        if (currentStep >= currentScript.dialog.Length)
        {
            isEndSaying = true;
            currentStep = 0;
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
