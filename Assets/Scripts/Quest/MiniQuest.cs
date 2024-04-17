using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class MiniQuest : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI qName;
    [SerializeField] TextMeshProUGUI qDescript;
    [SerializeField] TextMeshProUGUI qTask;
    [SerializeField] GameObject panel;
    [SerializeField] Button btn;
    bool bVisible = false;
    Quest quest;
    QuestData questData;
    bool bActiveQuest = false;
    

    private void Awake()
    {
        btn.onClick.AddListener(ToggleButton);
    }

    private void OnEnable()
    {
        QuestManager.Instance.OnQuestUpdateCallback += UpdateTask;
        QuestManager.Instance.OnQuestCompleteCallback += CompleteQuest;

    }
    public void ToggleButton() 
    {
        if (!bActiveQuest) return;

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

    public void SetMiniQuestPanel(Quest q, QuestData qData)
    {
        quest = q;
        questData = qData;
        bActiveQuest = true;
        ToggleButton();
        qName.text = qData.Name;
        qDescript.text = qData.Description;
        UpdateTask(quest.QuestId, 0);
    }
    void UpdateTask(int id,int k)
    {
        if (id == quest.QuestId)
        {
            qTask.text = $" {quest.CurrentCount} / {quest.TargetCount}";
        }

    }
    void CompleteQuest(int id)
    {
        if (id == quest.QuestId)
        {
            panel.SetActive(false);
            bVisible = false;
            bActiveQuest = false;

        }
    }

}
