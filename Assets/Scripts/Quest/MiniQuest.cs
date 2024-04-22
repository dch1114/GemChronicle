using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class MiniQuest : MonoBehaviour
{

    [SerializeField] GameObject panel;
    [SerializeField] Transform parent;
    [SerializeField] Button closeBtn;
    bool bVisible = false;
    Quest quest;
    QuestData questData;
    public List<SlotMiniQuest> slotMiniQuests;
    public SlotMiniQuest miniQuestPrefab;
    private void Awake()
    {
        closeBtn.onClick.AddListener(ToggleButton);
    }

    //미니퀘스트창 활성화/비활성화
    public void ToggleButton() 
    {

        if (bVisible)
        {
            panel.SetActive(false);
            bVisible = false;
        }
        else
        {
            panel.SetActive(true);
            bVisible = true;
        }

      
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
