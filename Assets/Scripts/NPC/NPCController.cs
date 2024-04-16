using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

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
    public bool isAction;
    public bool isNPC;
    public bool isShop = false;
    public bool isEndTalk = false;

    int currentStep;
    bool isLoadScriptData = false;
    bool isEndSaying = false;

    //���� ��� �ִ� ����Ʈ����Ÿ
    public QuestData offerQuestData;

    //��ȭ ���� ���̺� ����Ÿ
    private QuestTableData questData;
    private TalkTableData doQuest;
    private TalkTableData doingQuest;
    private TalkTableData doneQuest;
    [SerializeField] private Queue<ScriptTableData> scriptTableDatas = new Queue<ScriptTableData>();
    ScriptTableData currentScript;

    
    public void Init(NPC npc, NPCManager manager)
    {
        npcData = npc;
        npcManager = manager; // NPCManager�� �������� ����
        uiManager = UIManager.Instance;
        talkManager = npcManager.talkManager;
        playerinput = GameManager.Instance.player.Input;

        //���� NPC�� �����̶�� ù����Ʈ �� ��ȭ�� �Ҵ��Ѵ�.
        if (npcType == NPCType.Teacher)
        {
            offerQuestData = Database.Quest.Get(npcData.ID);
            QuestManager.Instance.OnQuestCompleteCallback += QuestCompleteCallBack;
        }

        SetNPCInfoData(npcData.ID);
    }

    void QuestCompleteCallBack(int id)
    {
        int questId = id + 1;
        offerQuestData = Database.Quest.Get(questId);
        SetNPCInfoData(questId);
    }

    void SetNPCInfoData(int index)
    {
        Debug.Log(index);
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
        else if (npcType == NPCType.Teacher)
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
        else if (npcType == NPCType.Teacher)
        {
            uiManager.talkBtnOnOff(false);
        }
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
            //Debug.Log("��ȭ ��ũ��Ʈ ������ �ε�� ���� ����");

            int[] scriptIds;

            if (QuestManager.Instance.IsClear(offerQuestData.ID))
            {
                scriptIds = doneQuest.scriptId;
            }
            else if (QuestManager.Instance.IsProgressQuest(offerQuestData.ID))
            {
                scriptIds = doingQuest.scriptId;
            }
            else
            {
                scriptIds = doQuest.scriptId;
            }

            AddScriptsToQueue(scriptIds);

            currentStep = 0;
            isLoadScriptData = true;
            isEndSaying = true;


            //if (QuestManager.Instance.IsClear(offerQuestData.ID))
            //{
            //    currentquestId++;
            //    SetNPCInfoData(currentquestId);
            //    //������ �� ����Ʈ�� Ŭ�����ߴٸ�
            //    for (int i = 0; i < doneQuest.scriptId.Length; i++)
            //    {
            //        scriptTableDatas.Enqueue(DataManager.Instance.GetScriptTableData(doneQuest.scriptId[i]));
            //    }
            //}
            //else if (QuestManager.Instance.IsProgressQuest(offerQuestData.ID))
            //{
            //    //������ �� ����Ʈ�� ���� �������̶��
            //    for (int i = 0; i < doingQuest.scriptId.Length; i++)
            //    {
            //        scriptTableDatas.Enqueue(DataManager.Instance.GetScriptTableData(doingQuest.scriptId[i]));
            //    }
            //}
            //else
            //{
            //    for (int i = 0; i < doQuest.scriptId.Length; i++)
            //    {
            //        scriptTableDatas.Enqueue(DataManager.Instance.GetScriptTableData(doQuest.scriptId[i]));
            //    }
            //}

            //currentStep = 0;
            //isLoadScriptData = true;
            //isEndSaying = true;
        }

        //NPC�� PLAYER �Ѵ� ���̻� �� ��ȭ�� �������� �ʾ� ��ȭ�� �����ؾ� �Ѵٸ�
        if (scriptTableDatas.Count <= 0)
        {
            //Debug.Log("NPC�� PLAYER �Ѵ� ���̻� �� ��ȭ�� �������� ����");
            //Debug.Log("�̵��ʱ�ȭ��");
            playerinput.OnEnable();
            //Debug.Log("�̵��ʱ�ȭ��");
            currentStep = 0;
            isLoadScriptData = false;
            //�ʻ�ȭ OFF
            uiManager.PotraitPanelOnOff(false);
            isEndSaying = false;

            //����Ʈ �ޱ�
            //���� NPC�� �����̶�� ����Ʈ ������ �õ��Ѵ�.
            if (npcType == NPCType.Teacher)
            {
                QuestManager.Instance.SubscribeQuest(offerQuestData.ID);
            }

            if (npcType == NPCType.Shop)
            {
                //�÷��̾� �̵� �Ұ�
                playerinput.OnDisable();
                //���� �˾�â ON
                uiManager.shopChoiceOnOff(true);
            }

            return;
        }

        if (isEndSaying)
        {
            //Debug.Log("���ϰ� �ִ°��� ������");

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
