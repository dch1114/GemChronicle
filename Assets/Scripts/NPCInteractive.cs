using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public interface INPCInteractive
{
    void AddNPCList();
    void RemoveNPCList();

    void Talk(int NPCID);
    void Action(GameObject gameObject);
    int NPCID { get; set; }

}





public class NPCInteractive : MonoBehaviour, INPCInteractive
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
    public static NPCInteractive instance = null;
    NPCDatabase npcDatabase;

    public int NPCID { get; set; }
    List<int> NPCList = new List<int>();

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
        public void AddNPCList()
    {
        NPCList.Add(NPCID);
        Debug.Log(NPCList);
    }


    void Start()
    {
        npcDatabase = DataManager.instance.npcDatabase;
    }

    public void RemoveNPCList()
    {
        NPCList.Remove(NPCID);
        Debug.Log(NPCList);
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
            Debug.Log("대화 종료");
            isAction = false;
            talkIndex = 0;
            potraitPanel.SetActive(false);

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
            potraitPanel.SetActive(true);
            talkText.text = talkData;

            portraitImg.sprite = talkManager.GetPortrait(id);
            Debug.Log("Portrait complete");

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