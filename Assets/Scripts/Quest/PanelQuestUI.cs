using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelQuestUI : MonoBehaviour
{
    public Dictionary<int, GameObject> waitingDic = new Dictionary<int, GameObject>();
    public Dictionary<int, GameObject> progressDic = new Dictionary<int, GameObject>();


    public void AddWaitingQuest(int id, GameObject wait)
    {
        waitingDic.Add(id,wait);
    }

    public void RemoveWaitingQuest(int id)
    {
        waitingDic[id].SetActive(false);
        waitingDic.Remove(id);
    }


    public void AddProgressQuest(int id, GameObject progress)
    {
        progressDic.Add(id,progress);

    }

    public void RemoveProgressQuest(int id)
    {
        progressDic[id].SetActive(false);

        progressDic.Remove(id);
    }



}
