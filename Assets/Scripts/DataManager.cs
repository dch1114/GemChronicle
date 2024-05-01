using System.Collections;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    public NPCDatabase npcDatabase;
    public ItemDatabase itemDatabase;
    private Shop shop;

    public TalkManager talkManager;
    public QuestManager questManager;

    void Start()
    {
        shop = FindObjectOfType<Shop>();
        TextAsset NPCjsonFile = Resources.Load<TextAsset>("JSON/NPCData");
        TextAsset jsonFile = Resources.Load<TextAsset>("JSON/Item_Data");
        if (NPCjsonFile != null)
        {
            string NPCjson = NPCjsonFile.text;

            npcDatabase = JsonUtility.FromJson<NPCDatabase>(NPCjson);
            npcDatabase.Initialize();
        }
        else
        {
            Debug.Log("NPC JSON NULL");
        }

        if (jsonFile != null)
        {
            string json = jsonFile.text;
            itemDatabase = JsonUtility.FromJson<ItemDatabase>(json);
            itemDatabase.Initialize();
        }
        else
        {
            Debug.Log("ITEM JSON NULL");
        }

        StartCoroutine(InitManagers());

    }


    IEnumerator InitManagers()
    {
        yield return StartCoroutine(InitManagersCoroutine());
        yield return StartCoroutine(questManager.InitQuestManager());
    }

    IEnumerator InitManagersCoroutine()
    {
        yield return null;
        if (shop != null) shop.SetShopItem();
        NPCManager.Instance.InitNPCManager();
        if (talkManager != null) talkManager.InitTalkManager();
    }


    public QuestTableData GetQuestTableData(int id)
    {
        if (npcDatabase.questTableDic.ContainsKey(id))
            return npcDatabase.questTableDic[id];
        return null;
    }

    public TalkTableData GetTalkTableData(int id)
    {
        if (npcDatabase.talkTableDic.ContainsKey(id))
            return npcDatabase.talkTableDic[id];
        return null;
    }

    public ScriptTableData GetScriptTableData(int id)
    {
        if (npcDatabase.dialogTableDic.ContainsKey(id))
            return npcDatabase.dialogTableDic[id];
        return null;
    }


}
