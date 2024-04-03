using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

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
        npcManager = manager; // NPCManager�� �������� ����
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

        isLoadScriptData = false;

    }
    public void Interact()
    {
        TryTalk();
    }

    //����Ʈ�� �������� ��, ����Ʈ�� �Ϸ��Ͽ����� ���¸� �� �� �ִ� ����� �־�� ��
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
            for (int i = 0; i < doQuest.scriptId.Length; i++)
            {
                scriptTableDatas.Enqueue(DataManager.Instance.GetScriptTableData(doQuest.scriptId[i]));
            }
            currentStep = 0;
            isLoadScriptData = true;
            isEndSaying = true;
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
