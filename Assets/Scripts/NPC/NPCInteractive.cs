using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public interface IInteractive  
{


    void touch(GameObject _player); //이름 바꾸기 , 매게변수 플레이어로 바꾸기

    void Talk(int __id);// 없애길 고려
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

        //만약이 상점 팝업이 열려 있는 상태라면 탭키를 눌렀을 때 현재 선택되어 있는 메뉴를 실행한다
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

        //더이상 대화 내용이 존재 하지 않아 대화를 종료해야 한다면
        if (talkIndex >= npcDatabase.GetNPCByKey(__id).conversation.Length)
        {
            isEndTalk = true;
            playerController.isTalking = false;

           
            isAction = false;
            talkIndex = 0;
            
           if (__id == 1301)
           {
                //상점 팝업창
                UIManager.instance.shopChoiceOnOff(true);
                isAction = true;
                playerController.isTalking = true;
                
            }

            return;
        }
        
        //대화 불러오기
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

