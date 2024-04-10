using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class QuestDescription : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI questName;
    [SerializeField] private TextMeshProUGUI questDescription;
    [SerializeField] private TextMeshProUGUI questRewardGold;
    [SerializeField] private TextMeshProUGUI questRewardExp;
    [SerializeField] private TextMeshProUGUI questRewardGem;

    public Quest quest { get; set; }
    public QuestData questData { get; set; }

    public virtual void ConfigureQuestUI(Quest q, QuestData qData)
    {
        quest = q;
        questData = qData;
        questName.text = qData.Name;
        questDescription.text = string.Format("{0} ({1})", qData.Description, qData.Count);

        questRewardGold.text = qData.Gold.ToString();
        questRewardExp.text = qData.Exp.ToString();
        questRewardGem.text = "0";
    }

    public int GetQuestId()
    {
        return quest.QuestId;
    }
}
