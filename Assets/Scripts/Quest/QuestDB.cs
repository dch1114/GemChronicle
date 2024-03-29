using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestDB
{
    private Dictionary<int, QuestData> _quest = new();

    public QuestDB()
    {
        var res = Resources.Load<QuestDataSheet>("DB/QuestSO");
        var questSo = Object.Instantiate(res);
        var sheet1 = questSo.Sheet1;

        if (sheet1 == null || sheet1.Count <= 0)
            return;

        var entityCount = sheet1.Count;
        for (int i = 0; i < entityCount; i++)
        {
            var quest = sheet1[i];

            if(_quest.ContainsKey(quest.ID))
                _quest[quest.ID] = quest;
            else
                _quest.Add(quest.ID, quest);
        }
    }

    public QuestData Get(int id)
    {
        if (_quest.ContainsKey(id))
            return _quest[id];

        return null;
    }

    public IEnumerator Obenumerator()
    {
        return _quest.GetEnumerator();
    }
}
