using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class MiniQuest : MonoBehaviour
{

    [SerializeField] GameObject panel;
    [SerializeField] Transform parent;

    bool bVisible = false;
    Quest quest;
    QuestData questData;
    public List<SlotMiniQuest> slotMiniQuests;
    public SlotMiniQuest miniQuestPrefab;
    //미니퀘스트창 활성화/비활성화
    public void ToggleButton() 
    {

        if (bVisible)
        {
            panel.SetActive(false);
        }
        else
        {
            panel.SetActive(true);

        }

        bVisible = !bVisible;
    }

    public void SetMiniQuest(Quest questcompleted, QuestData qData)
    {
        SlotMiniQuest miniQuest = Instantiate(miniQuestPrefab, parent);
        miniQuest.SetSlot(questcompleted, qData);
        miniQuest.rewardCallBackAction = RemoveList;
        slotMiniQuests.Add(miniQuest);
    }

    void RemoveList(SlotMiniQuest slot)
    {
        slotMiniQuests.Remove(slot);
    }


}
