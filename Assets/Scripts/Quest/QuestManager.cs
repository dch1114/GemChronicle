using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : Singleton<QuestManager>
{
    //[Header("Character")]
    //[SerializeField] private Character character;

    [Header("Quests")]
    [SerializeField] private Quest[] questionAvailable;

    [Header("Inspector Quests")]
    [SerializeField] private InspectorQuestDescription inspectorQuestPrefab;
    [SerializeField] private Transform InspectorQuestContainer;

    [Header("Character Quests")]
    [SerializeField] private CharacterQuestDescription characterQuestPrefab;
    [SerializeField] private Transform characterQuestContainer;

    [Header("Panel Quest Completed")]
    [SerializeField] private GameObject PanelQuestCompleted;
    [SerializeField] private TextMeshProUGUI questName;
    [SerializeField] private TextMeshProUGUI questRecompenseGold;
    [SerializeField] private TextMeshProUGUI questRecompenseExp;
    [SerializeField] private TextMeshProUGUI questRecompenseItemQuantity;
    [SerializeField] private Image questquestRecompenseItemIcon;

    public Quest QuestUnclaimed { get; private set; }

    private void Start()
    {
        LoadingQuestInspector();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            AddProgress("Kill10", 1);
            AddProgress("Kill25", 1);
            AddProgress("Kill50", 1);
        }
    }

    private void LoadingQuestInspector()
    {
        for (int i = 0; i < questionAvailable.Length; i++)
        {
            InspectorQuestDescription newQuest = Instantiate(inspectorQuestPrefab, InspectorQuestContainer);
            newQuest.ConfigureQuestUI(questionAvailable[i]);
        }
    }

    private void AddQuestToComplete(Quest questcompleted)
    {
        CharacterQuestDescription newQuest = Instantiate(characterQuestPrefab, characterQuestContainer);
        newQuest.ConfigureQuestUI(questcompleted);
    }

    public void AddQuest(Quest questcompleted)
    {
        AddQuestToComplete(questcompleted);
    }

    public void ReclamarRecompensa()
    {
        if (QuestUnclaimed == null)
        {
            return;
        }

        GoldManager.Instance.AddGold(QuestUnclaimed.RewardGold);
        //Character.CharacterExperience.AddExperience(QuestUnclaimed.RewardExp);
        //Inventory.Instance.AddItem(QuestUnclaimed.RewardItem.Item, QuestUnclaimed.RewardItem.Quantity);
        PanelQuestCompleted.SetActive(false);
        QuestUnclaimed = null;
    }

    public void AddProgress(string questID, int quantity)
    {
        Quest questforUpdate = QuestExists(questID);
        questforUpdate.AddProgress(quantity);
    }

    private Quest QuestExists(string questID)
    {
        for (int i = 0; i < questionAvailable.Length; i++)
        {
            if (questionAvailable[i].ID == questID)
            {
                return questionAvailable[i];
            }
        }
        return null;
    }

    private void ShowQuestCompleted(Quest questCompleted)
    {
        PanelQuestCompleted.SetActive(true);
        questName.text = questCompleted.Name;
        questRecompenseGold.text = questCompleted.RewardGold.ToString();
        questRecompenseExp.text = questCompleted.RewardExp.ToString();
        questRecompenseItemQuantity.text = questCompleted.RewardItem.Quantity.ToString();
        //questquestRecompenseItemIcon.sprite = questCompleted.RewardItem.Item.Icon;
    }

    private void QuestCompletedResponse(Quest questCompleted)
    {
        QuestUnclaimed = QuestExists(questCompleted.ID);
        if (QuestUnclaimed != null)
        {
            ShowQuestCompleted(QuestUnclaimed);
        }
    }

    private void OnEnable()
    {
        Quest.EventQuestCompleted += QuestCompletedResponse;
    }

    private void OnDisable()
    {
        Quest.EventQuestCompleted -= QuestCompletedResponse;
    }
}
