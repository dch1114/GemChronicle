using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressQuestDescription : QuestDescription
{
    [SerializeField] private TextMeshProUGUI taskCount;

    Button getButton;
    GameObject btnObject;

    private void Awake()
    {
        getButton = GetComponentInChildren<Button>();
        getButton.onClick.AddListener(ClickGetButton);
        btnObject = getButton.gameObject;
        btnObject.SetActive(false);
    }

    private void OnEnable()
    {
        QuestManager.Instance.OnQuestCompleteCallback += ShowGetButton;
    }

    private void Update()
    {
        taskCount.text = $"{quest.CurrentCount}/{quest.TargetCount}";
    }

    public override void ConfigureQuestUI(Quest quest, QuestData qData)
    {
        base.ConfigureQuestUI(quest, qData);
    }

    void ClickGetButton()
    {
        
        
        
        QuestManager.Instance.SetClearProgressQuestUI(questData.ID);

    }

    void ShowGetButton(int id)
    {
        btnObject.SetActive(true);
    }
}
