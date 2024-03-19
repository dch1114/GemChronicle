using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InspectorQuestDescription : QuestDescription
{
    [SerializeField] private TextMeshProUGUI questRewarded;

    public override void ConfigureQuestUI(Quest quest)
    {
        base.ConfigureQuestUI(quest);
        QuestCompleted = quest;
        questRewarded.text = $"-{quest.RewardGold} gold" +
                               $"\n-{quest.RewardExp} exp";
                               //$"\n-{questForLoad.RewardItem.Item.Name} x {questForLoad.RewardItem.Quantity}";
    }

    public void AcceptQuest()
    {
        if (QuestCompleted == null)
        {
            return;
        }

        QuestManager.Instance.AddQuest(QuestCompleted);
        gameObject.SetActive(false);
    }
}
