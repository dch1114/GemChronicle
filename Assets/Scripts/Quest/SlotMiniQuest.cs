using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotMiniQuest : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI qName;
    [SerializeField] TextMeshProUGUI qDescript;
    [SerializeField] TextMeshProUGUI qTask;
    [SerializeField] Button rewardBtn;
    public Action<SlotMiniQuest> rewardCallBackAction;
    Quest quest;
    public GameObject ClearFrame;
    bool isClear = false;

    private void Awake()
    {
        rewardBtn.onClick.AddListener(ClickRewardButton);

    }
    private void OnEnable()
    {
        QuestManager.Instance.OnQuestUpdateCallback += UpdateTask;


    }
    private void OnDisable()
    {
        QuestManager.Instance.OnQuestUpdateCallback -= UpdateTask;

    }
    public void SetSlot(Quest q, QuestData qData)
    {
        quest = q;
        qName.text = qData.Name;
        qDescript.text = qData.Description;
        UpdateTask(quest.QuestId, 0);
    }
    void UpdateTask(int id, int k)
    {
        if(quest.QuestId == id)
        {
            qTask.text = $" {quest.CurrentCount} / {quest.TargetCount}";
        }
        
    }
    private void Update()
    {
        if (quest.IsClearQuest)
        {
            ClearFrame.SetActive(true);
        }
    }
    void ClickRewardButton()
    {
        if (quest.IsClearQuest)
        {
            
            if (rewardCallBackAction != null)
            {
                rewardCallBackAction.Invoke(this);
            }

            QuestManager.Instance.QuestClear(quest.QuestId);

            quest = null;
            rewardCallBackAction = null;
            gameObject.SetActive(false);
        }
        else
        {
           
        }
    }


}
