using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour, IInteractive
{
    [SerializeField]
    private NPC npcData;
    private NPCManager npcManager; // NPCManager�� �������� ���� �ʵ� �߰�

    public SubQuestDataSheet subQuest;

    TalkManager talkManager;
    PlayerInput playerinput;

    //NpcŸ���� �̸� �����տ��� �����س�
    public NPCType npcType;
    //UiManager�� �̸� ����
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

    //��ȭ ���� ���̺� ����Ÿ
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
        npcManager = manager; // NPCManager�� �������� ����
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
    //NpcŸ�Կ� ���� ��ȭâ �ݱ�
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
        //������ ���� �˾��� ���� �ִ� ���¶�� ��Ű�� ������ �� ���� ���õǾ� �ִ� �޴��� �����Ѵ�
        if (uiManager.IsOpenShowPopup())
        {
            uiManager.RunSelectedMenuButton();
            return;
        }
        //��ȭ ��ũ��Ʈ �����Ͱ� ���� �ε�� ���� ���ٸ�
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

        //NPC�� PLAYER �Ѵ� ���̻� �� ��ȭ�� �������� �ʾ� ��ȭ�� �����ؾ� �Ѵٸ�
        if (scriptTableDatas.Count <= 0)
        {
            //���� ����Ʈ�� �����ϴ� NPC���
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
            //�ʻ�ȭ OFF
            uiManager.PotraitPanelOnOff(false);
            isEndSaying = false;

            if (npcType == NPCType.Shop)
            {
                //�÷��̾� �̵� �Ұ�
                playerinput.OnDisable();
                //���� �˾�â ON
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


        //Npc�ʻ�ȸ �̹��� ����
        uiManager.SetNpcPortraitImage(talkManager.GetPortrait(npcData.ID));
        //Player�ʻ�ȭ �̹��� ����
        uiManager.SetPlayerPortraitImage(talkManager.GetPlayerSprite());
        //�ʻ�ȭ �̹��� ON
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
