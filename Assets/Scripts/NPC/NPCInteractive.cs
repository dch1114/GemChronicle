using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public interface IInteractive  
{


    void touch(GameObject _player); //�̸� �ٲٱ� , �ŰԺ��� �÷��̾�� �ٲٱ�

    void Talk(int __id);// ���ֱ� ���
}



public class NPCInteractive : MonoBehaviour, IInteractive
{
    public GameObject scanObject;
    public TalkManager talkManager;
    
    public bool isAction;
    public int talkIndex;
    public Text talkText;

    public bool isNPC;

    public Image portraitImg;
    public static NPCInteractive instance = null;
    NPCDatabase npcDatabase;
    public bool isShop = false;

    public PlayerController playerController;

    public bool isEndTalk = false;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
           
        }
        else
        {
            if (instance != this)
                Destroy(this.gameObject);
        }
    }
    


    void Start()
    {
        npcDatabase = DataManager.instance.npcDatabase;
    }

    

    public void touch(GameObject _scanobj)
    {
        scanObject = _scanobj;
        NPCController npcController = scanObject.GetComponent<NPCController>();

        //������ ���� �˾��� ���� �ִ� ���¶�� ��Ű�� ������ �� ���� ���õǾ� �ִ� �޴��� �����Ѵ�
        if (UIManager.instance.IsOpenShowPopup())
        {
            UIManager.instance.RunSelectedMenuButton();
            return;
        }

        isEndTalk = false;
        playerController.isTalking = true;

        if (isEndTalk == true)
        {
            playerController.isTalking = false;
            return;
        } 



       

        if (npcController != null)
        {
            Talk(npcController.GetNpcData().ID);
        }
       

        UIManager.instance.PotraitPanelOnOff(isAction);

    }


    public void Talk(int __id)
    {

        //���̻� ��ȭ ������ ���� ���� �ʾ� ��ȭ�� �����ؾ� �Ѵٸ�
        if (talkIndex >= npcDatabase.GetNPCByKey(__id).conversation.Length)
        {
            isEndTalk = true;
            playerController.isTalking = false;

           
            isAction = false;
            talkIndex = 0;
            
           if (__id == 1301)
           {
                //���� �˾�â
                UIManager.instance.shopChoiceOnOff(true);
                isAction = true;
                playerController.isTalking = true;
                
            }

            return;
        }
        
        //��ȭ �ҷ�����
        string talkData = talkManager.GetTalk(__id, talkIndex);

        if (talkData == null)
        {
            //Time.timeScale = 1;
            isAction = false;
            talkIndex = 0;
            isEndTalk = true;
            playerController.isTalking = false;

            talkText.text = talkData;
            return;
        }
        else
        {
            talkText.text = talkData;
            portraitImg.sprite = talkManager.GetPortrait(__id);
            isAction = true;
            talkIndex++;

        }
       


    }


}

