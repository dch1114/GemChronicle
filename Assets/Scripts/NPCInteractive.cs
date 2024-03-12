using System.Collections;
using System.Collections.Generic;
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
    public bool isAction;
    public int talkIndex;
    public Text talkText;

    public bool isNPC;

    public Image portraitImg;
    public static NPCInteractive instance = null;


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
            Talk(npcController.NPCID);
        }

        talkPanel.SetActive(isAction);

    }


    public void Talk(int id)
    {
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

            portraitImg.sprite = talkManager.GetPortrait(id, int.Parse(talkData));
            portraitImg.color = new Color(1, 1, 1, 1);
        }
        else
        {
            talkText.text = talkData;

            portraitImg.color = new Color(1, 1, 1, 0);
        }

        isAction = true;
        talkIndex++;
    }
}