using UnityEngine;
using System.Collections.Generic;
using System;
using Newtonsoft.Json;
using System.IO;
using System.Collections;

[System.Serializable]
public class Quest
{
    public int ID;
    public string Name;
    public int Gold;
    public int Exp;
    public string Description;
}

public class QuestInstance
{
    int no;
    public Quest quest;
}

[System.Serializable]
public class questDatabase
{
    public string questDataPath;
    public List<Quest> QuestInfos;
    public Dictionary<int, Quest> questDic = new();


    void Start()
    {
        LoadQuestData();
    }

    void LoadQuestData()
    {
        // JSON ���� �б�
        string json;
        using (StreamReader reader = new StreamReader(questDataPath))
        {
            json = reader.ReadToEnd();
        }

        // JSON �Ľ��Ͽ� ��ųʸ��� ����
        List<Quest> questList = JsonConvert.DeserializeObject<List<Quest>>(json);
        questDic = new Dictionary<int, Quest>();

        // �� ����Ʈ�� ��ųʸ��� �߰�
        foreach (Quest quest in questList)
        {
            questDic.Add(quest.ID, quest);
        }
    }
}