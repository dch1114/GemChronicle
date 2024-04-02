using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Database : Singleton<Database>
{
    private QuestDB _quest;
    public static QuestDB Quest
    {
        get
        {
            if(Instance._quest == null)
                Instance._quest = new QuestDB();
            return Instance._quest;
        }
    }
}
