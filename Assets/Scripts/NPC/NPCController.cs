using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour, IInteractive
{
    [SerializeField]
    private NPC npcData;
    private NPCManager npcManager; // NPCManager의 역참조를 받을 필드 추가

    public SubQuestDataSheet subQuest;

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
    public GameObject friend;
    int currentStep;
    bool isLoadScriptData = false;
    bool isEndSaying = false;

    int currentNpcID;

    //대화 관련 테이블 데이타
    private QuestTableData questData;
    private TalkTableData doQuest;
    private TalkTableData doingQuest;
    private TalkTableData doneQuest;
    [SerializeField] private Queue<ScriptTableData> scriptTableDatas = new Queue<ScriptTableData>();
    ScriptTableData currentScript;

    private void OnEnable()
    {
        QuestManager.Instance.OnQuestCompleteCallback += SetNPCInfoData;
    }

    public void Init(NPC npc, NPCManager manager)
    {
        npcData = npc;
        npcManager = manager; // NPCManager의 역참조를 받음
        uiManager = UIManager.Instance;
        talkManager = npcManager.talkManager;
        currentNpcID = QuestManager.Instance.currentProgressMainQuestData.ID;

        questData = DataManager.Instance.GetQuestTableData(npcData.ID);
        doQuest = DataManager.Instance.GetTalkTableData(questData.talk_1);
        doingQuest = DataManager.Instance.GetTalkTableData(questData.talk_2);
        doneQuest = DataManager.Instance.GetTalkTableData(questData.talk_3);
    }

    void SetNPCInfoData(int index)
    {
        if (index == currentNpcID && DataManager.Instance.GetQuestTableData(currentNpcID) != null)
        {
            currentNpcID = index + 1;

            if (npcType == NPCType.Teacher)
            {
                questData = DataManager.Instance.GetQuestTableData(currentNpcID);

                if (questData != null)
                {
                    doQuest = DataManager.Instance.GetTalkTableData(questData.talk_1);
                    doingQuest = DataManager.Instance.GetTalkTableData(questData.talk_2);
                    doneQuest = DataManager.Instance.GetTalkTableData(questData.talk_3);
                }

            }
        }

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

        uiManager.talkBtnOnOff(true);

        //if (npcType == NPCType.Npc)
        //{
        //    uiManager.talkBtnOnOff(true);

        //}
        //else if (npcType == NPCType.Shop)
        //{

        //    uiManager.talkBtnOnOff(true);

        //}
        //else if (npcType == NPCType.Teacher)
        //{
        //    uiManager.talkBtnOnOff(true);
        //}

    }
    //Npc타입에 따라 대화창 닫기
    public void CloseUI()
    {
        uiManager.talkBtnOnOff(false);

        //if (npcType == NPCType.Npc)
        //{
        //    uiManager.talkBtnOnOff(false);

        //}
        //else if (npcType == NPCType.Shop)
        //{
        //    uiManager.talkBtnOnOff(false);
        //}
        //else if (npcType == NPCType.Teacher)
        //{
        //    uiManager.talkBtnOnOff(false);
        //}
        isLoadScriptData = false;

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
        //대화 스크립트 데이터가 아직 로드된 것이 없다면
        if (!isLoadScriptData)
        {
   

            int[] scriptIds;
            if (npcType == NPCType.SubNpc)
            {
                if (QuestManager.Instance.IsClear(2))
                {
                    scriptIds = doneQuest.scriptId;


                }
                else if (QuestManager.Instance.IsProgressQuest(2))
                {
                    scriptIds = doingQuest.scriptId;

                }
                else
                {
                    scriptIds = doQuest.scriptId;

                }
            }
            else
            {
                if (QuestManager.Instance.IsClear(currentNpcID))
                {
                    scriptIds = doneQuest.scriptId;


                }
                else if (QuestManager.Instance.IsProgressQuest(currentNpcID))
                {
                    scriptIds = doingQuest.scriptId;

                }
                else
                {
                    scriptIds = doQuest.scriptId;

                }
            }
            

            AddScriptsToQueue(scriptIds);

            currentStep = 0;
            isLoadScriptData = true;
            isEndSaying = true;

        }

        //NPC와 PLAYER 둘다 더이상 할 대화가 남아있지 않아 대화를 종료해야 한다면
        if (scriptTableDatas.Count <= 0)
        {
            //서브 퀘스트가 존재하는 NPC라면
            if (npcType == NPCType.SubNpc)
            {
                QuestManager.Instance.SubscribeQuest((int)npcType);
            }

            else
            {
                if (npcType == NPCType.Teacher || npcType == NPCType.Friend || npcType == NPCType.Diary)
                {

                    QuestManager.Instance.SubscribeQuest(currentNpcID);
                }

                else
                {

                }

                if (QuestManager.Instance.IsProgressQuest(currentNpcID) && QuestManager.Instance.CheckCompareTargetID(npcData.ID))
                {
                    QuestManager.Instance.NotifyQuest(Constants.QuestType.TalkNpc, npcData.ID, 1);
                    if (npcData.ID == 3000)
                    {
                        Destroy(friend);
                    }
                }
            }

            SetPlayerInput();

         
            playerinput.OnEnable();
 
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
            if (npcType == NPCType.Healer)
            {
                playerinput.OnDisable();
                uiManager.HealChoiceOnOff(true);
            }
            if (npcType == NPCType.Diary)
            {

                UIManager.Instance.OnOffDiary(true);
            }


            return;
        }

        if (isEndSaying)
        {
    

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

    private void SetPlayerInput()
    {
        if (playerinput == null)
        {
            playerinput = GameManager.Instance.player.Input;
        }
    }


    void AddScriptsToQueue(int[] scriptIds)
    {
        foreach (int scriptId in scriptIds)
        {
            scriptTableDatas.Enqueue(DataManager.Instance.GetScriptTableData(scriptId));
        }
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
