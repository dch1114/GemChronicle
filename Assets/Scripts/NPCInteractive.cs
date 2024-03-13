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
   
  

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
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

        if (npcController != null)
        {
            Talk(npcController.GetNpcData().ID);
        }
        Debug.Log("test" + isAction);
        UIManager.instance.talkPanelOnOff(isAction);

    }


    public void Talk(int __id)
    {

        if (talkIndex >= npcDatabase.GetNPCByKey(__id).conversation.Length)
        {
            
            isAction = false;
            talkIndex = 0;
           if (__id == 1301)
            {
                UIManager.instance.shopChoiceOnOff(true);
                isAction = true;
            }

            return;
        }
        
        //대화 불러오기
        string talkData = talkManager.GetTalk(__id, talkIndex);

        if (talkData == null)
        {
            isAction = false;
            talkIndex = 0;

            return;
        }
        if (talkData != null)
        {
          
            talkText.text = talkData;

            portraitImg.sprite = talkManager.GetPortrait(__id);
            

            
        }
        else
        {
         

            talkText.text = talkData;

           
        }

        isAction = true;
        talkIndex++;
    }
}

