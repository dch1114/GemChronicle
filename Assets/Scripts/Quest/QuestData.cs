using System.Collections;
using System.Collections.Generic;

public class QuestData : RawData
{
    //public int id;
    public string name;
    public string sprite_name;
    public int goal_val;
    public string goal_desc;
    public int reward_item_id;
    public int reward_item_amount;
    public string questName;
    public int[] npcId;

    public QuestData(string name, int[] npc)
    {
        questName = name;
        npcId = npc;
    }
}