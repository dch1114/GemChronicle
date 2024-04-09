using Constants;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : Singleton<QuestManager>
{
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


    //����Ʈ�� �����ϸ� ��ųʸ��� ����
    private Dictionary<int, Quest> _ongoingQuests = new();
    //����Ʈ�� �Ϸ��ϸ� �ؽ��¿� ����
    private HashSet<int> _completeQuests = new();
    //����Ʈ ���� �ݹ�
    public event Action<int> OnQuestStartCallback;
    //����Ʈ ī��Ʈ ������Ʈ �ݹ�
    public event Action<int, int> OnQuestUpdateCallback;
    //����Ʈ �Ϸ� �ݹ�
    public event Action<int> OnQuestCompleteCallback;

    private Dictionary<QuestType, List<QuestData>> _subscribeQuests = new();

    /// <summary>
    /// SubscribeQuest�� QuestData ���� QuestStart�� ����Ʈ ���൵�� ����üũ
    /// </summary>

    //����Ʈ ����
    public void SubscribeQuest(int questId)
    {
        //���� questID�� �̹� �������̰ų� �Ϸ� �޴ٸ� �ߺ� ���� ����
        if (IsClear(questId) || IsProgressQuest(questId))
        {
            Debug.Log($"�̹� ID:{questId} ����Ʈ�� �������̰ų� �Ϸ��Ͽ����ϴ�");
            return;
        }

        //����Ʈ ������ �ҷ�����
        var questData = Database.Quest.Get(questId);

        //���� ��ųʸ��� ����ƮŸ���� �������� ������ Add ��ųʸ�
        if (_subscribeQuests.ContainsKey(questData.Type) == false)
        {
            _subscribeQuests[questData.Type] = new List<QuestData>();
        }

        _subscribeQuests[questData.Type].Add(questData);

        QuestStart(questId);

        Debug.Log($"ID:{questId} ����Ʈ ���� �Ϸ�");
    }

    //����Ʈ ���� ����
    public void UnsubscribeQuest(int questId)
    {
        
        var questData = Database.Quest.Get(questId);

        if (_subscribeQuests.ContainsKey(questData.Type) == false)
            return;
        //���� ��ųʸ��� ����Ʈ Ÿ���� ������ ���� Remove ��ųʸ�
        _subscribeQuests[questData.Type].Remove(questData);
        Debug.Log($"ID:{questId} ����Ʈ ���� ���� �Ϸ�");
    }
    
    //����Ʈ ���൵ ������Ʈ (QuestType:��з�, target:�Һз�, count:����Ƚ��)
    public void NotifyQuest(QuestType type, int target, int count)
    {
        if (_subscribeQuests.ContainsKey(type) == false)
            return;
        //QuestType(��з�) ����Ʈ �ҷ�����
        var filteredQuests = _subscribeQuests[type];
        //��ųʸ��� ���鼭 target(�Һз�)�� ��ǥ�� ���� QuestData�� �����Ͽ� List<QuestData>�� �̾Ƴ���
        List<QuestData> targetQuests = filteredQuests.FindAll(q => q.Target == target);
        //foreach���鼭 quest ID�� ������ ����Ʈ�� ����Ƚ�� ������Ʈ
        foreach (var quest in targetQuests)
            QuestUpdate(quest.ID, count);
    }

    void QuestStart(int questId)
    {
        if (_ongoingQuests.ContainsKey(questId))
        {
            Debug.Log($"�ش� ID:{questId} �� �̹� ����Ʈ�� �������Դϴ�");
            return;
        }
        
        //����Ʈ�� �����ϰ� ���¸� Wait���� Progress�� ����
        var quest = new Quest(questId);
        quest.Start();
        //����Ʈ�� ������ ��ųʸ��� �߰�
        _ongoingQuests.Add(questId, quest);
        //����Ʈ�� ���� �ݹ� �׼�
        OnQuestStartCallback?.Invoke(questId);
    }

    public void QuestUpdate(int questId, int amount)
    {
        if (_ongoingQuests.ContainsKey(questId) == false)
            return;

        var questData = Database.Quest.Get(questId);

        //����Ƚ���� ������Ʈ �ϰ� ������ Ƚ���� ���Ϲ޴´�
        int currentCount = _ongoingQuests[questId].Update(amount);

        //_ongoingQuests[questId].Update(amount);

        //UI������Ʈ �뵵�� ����� ����
        OnQuestUpdateCallback?.Invoke(questId, amount);

        //���� ����Ƚ���� ����Ʈ����Ÿ�� ��ǥȽ���� �����ϸ� ����Ʈ Ŭ����
        if (currentCount >= questData.Count)
            QuestClear(questId);
    }

    public void QuestClear(int questId)
    {
        if (_ongoingQuests.ContainsKey(questId) == false)
            return;

        _ongoingQuests[questId].Complete();
        _ongoingQuests.Remove(questId);

        _completeQuests.Add(questId);

        Debug.Log($"ID:{questId} ����Ʈ Ŭ����!");

        OnQuestCompleteCallback?.Invoke(questId);
    }

    public bool IsClear(int questId)
    {
        return _completeQuests.Contains(questId);
    }

    public bool IsProgressQuest(int questId)
    {
        return _ongoingQuests.ContainsKey(questId);
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


    private void ShowQuestCompleted(Quest questCompleted)
    {
        PanelQuestCompleted.SetActive(true);
        questName.text = questCompleted.Name;
        questRecompenseGold.text = questCompleted.RewardGold.ToString();
        questRecompenseExp.text = questCompleted.RewardExp.ToString();
        questRecompenseItemQuantity.text = questCompleted.RewardItem.Quantity.ToString();
        //questquestRecompenseItemIcon.sprite = questCompleted.RewardItem.Item.Icon;
    }

}
