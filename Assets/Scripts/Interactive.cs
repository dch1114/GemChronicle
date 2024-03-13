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





public class Interactive : MonoBehaviour, IInteractive
{
    public GameObject scanObject;
    public TalkManager talkManager;
    public GameObject talkPanel;
    public GameObject potraitPanel;
    public bool isAction;
    public int talkIndex;
    public Text talkText;

    public bool isNPC;

    public Image portraitImg;
    public static Interactive instance = null;
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
            potraitPanel.SetActive(false);

            return;
        }
        
        //��ȭ �ҷ�����
        string talkData = talkManager.GetTalk(id, talkIndex);

        if (talkData == null)
        {
            isAction = false;
            talkIndex = 0;

            return;
        }
        if (talkData != null)
        {
            potraitPanel.SetActive(true);
            talkText.text = talkData;

            portraitImg.sprite = talkManager.GetPortrait(id);
            

            portraitImg.color = new Color(1, 1, 1, 1);
        }
        else
        {
            potraitPanel.SetActive(false);

            talkText.text = talkData;

            portraitImg.color = new Color(1, 1, 1, 0);
        }

        isAction = true;
        talkIndex++;
    }
}