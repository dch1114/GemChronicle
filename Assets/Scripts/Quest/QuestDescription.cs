using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class QuestDescription : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI questName;
    [SerializeField] private TextMeshProUGUI questDescription;

    public Quest QuestCompleted { get; set; }

    public virtual void ConfigureQuestUI(Quest quest)
    {
        QuestCompleted = quest;
        questName.text = quest.Name;
        questDescription.text = quest.Description;
    }
}
