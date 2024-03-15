using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class QuestMain : MonoBehaviour
{
    public UIQuestDirector director;

    void Start()
    {
        //DataViewManager.instance.LoadQuestData();

        DataManagers.instance.LoadData<QuestData>();
        DataManagers.instance.LoadData<RewardItemData>();

        var dicQuestData = DataManagers.instance.GetDataDic<QuestData>();
        foreach ( var data in dicQuestData.Values)
        {
            Debug.LogFormat("{0}\t{1}\t{2}",data.id, data.name, string.Format(data.goal_desc, data.goal_val));
        }

        var dicRewardItemData = DataManagers.instance.GetDataDic<RewardItemData>();
        foreach (var data in dicRewardItemData.Values)
        {
            Debug.LogFormat("{0}\t{1}", data.id, data.name);
        }

        this.director.Init();
    }
}
