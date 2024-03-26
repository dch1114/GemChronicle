using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
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
    public bool isAction;
    public bool isNPC;
    public bool isShop = false;
    public bool isEndTalk = false;

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
        uiManager = UIManager.instance;
        talkManager = npcManager.talkManager;
        playerinput = npcManager.playerinput;
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
    public void TryTalk()
    {
        //������ ���� �˾��� ���� �ִ� ���¶�� ��Ű�� ������ �� ���� ���õǾ� �ִ� �޴��� �����Ѵ�
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

        //���̻� ��ȭ ������ ���� ���� �ʾ� ��ȭ�� �����ؾ� �Ѵٸ�
        if (CheckConversationCount()) return;

        Talk();

    }

   
    void Talk()
    {
        //��ȭ �ҷ�����
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

    //��ȭ������ �� �ִ��� üũ
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
                //���� �˾�â
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
        GameManager gameManager = FindObjectOfType<GameManager>(); // ���� �Ŵ��� ã��

        if (gameManager != null)
        {
            GameObject player = gameManager.GetPlayer(); // ���� �Ŵ����� ���� �÷��̾� ���
            TryTalk();
        }
    }


    InteractType IInteractive.GetType()
    {
        return InteractType.NPC;
    }
}
