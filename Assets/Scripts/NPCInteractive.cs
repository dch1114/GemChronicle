using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public interface IInteractive
{

    void Talk(int NPCID);
    void Action(GameObject gameObject);
    

}





public class NPCInteractive : MonoBehaviour, IInteractive
{
    public GameObject scanObject;
    public TalkManager talkManager;
    public GameObject talkPanel;
    public bool isAction;
    public int talkIndex;
    public Text talkText;

    public bool isNPC;

    public Image portraitImg;
    public static NPCInteractive instance = null;
    NPCDatabase npcDatabase;

   
  

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

    

    public void Action(GameObject _scanobj)
    {


        scanObject = _scanobj;
        NPCController npcController = scanObject.GetComponent<NPCController>();

        if (npcController != null)
        {
            Talk(npcController.GetNpcData().ID);
        }

        talkPanel.SetActive(isAction);

    }


    public void Talk(int id)
    {

        if (talkIndex >= npcDatabase.GetNPCByKey(id).conversation.Length)
        {
            
            isAction = false;
            talkIndex = 0;
           

            return;
        }
        
        //대화 불러오기
        string talkData = talkManager.GetTalk(id, talkIndex);

        if (talkData == null)
        {
            isAction = false;
            talkIndex = 0;

            return;
        }
        if (talkData != null)
        {
          
            talkText.text = talkData;

            portraitImg.sprite = talkManager.GetPortrait(id);
            

            
        }
        else
        {
         

            talkText.text = talkData;

           
        }

        isAction = true;
        talkIndex++;
    }
}