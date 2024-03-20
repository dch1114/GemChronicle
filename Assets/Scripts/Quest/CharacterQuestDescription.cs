using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterQuestDescription : QuestDescription
{
    [SerializeField] private TextMeshProUGUI Objectivetask;
    [SerializeField] private TextMeshProUGUI rewardGold;
    [SerializeField] private TextMeshProUGUI rewardExp;

    [Header("Item")]
    [SerializeField] private Image rewardItemIcon;
    [SerializeField] private TextMeshProUGUI rewardItemQuantity;

    private void Update()
    {
        if (QuestCompleted.QuestCompletedCheck)
        {
            return;
        }

        Objectivetask.text = $"{QuestCompleted.CurrentQuantity}/{QuestCompleted.TargetQuantity}";
    }

    public override void ConfigureQuestUI(Quest quest)
    {
        base.ConfigureQuestUI(quest);
        rewardGold.text = quest.RewardGold.ToString();
        rewardExp.text = quest.RewardExp.ToString();
        Objectivetask.text = $"{quest.CurrentQuantity}/{quest.TargetQuantity}";

        //rewardItemIcon.sprite = quest.RewardItem.Item.Icon;
        //rewardItemQuantity.text = quest.RewardItem.Quantity.ToString();
    }

    private void QuestCompletedResponse(Quest questCompleted)
    {
        if(questCompleted.ID == QuestCompleted.ID)
        {
            Objectivetask.text = $"{QuestCompleted.CurrentQuantity}/{QuestCompleted.TargetQuantity}";
            gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        if (QuestCompleted.QuestCompletedCheck)
        {
            gameObject.SetActive(false);
        }

        Quest.EventQuestCompleted += QuestCompletedResponse;
    }

    private void OnDisable()
    {
        Quest.EventQuestCompleted -= QuestCompletedResponse;
    }
}
