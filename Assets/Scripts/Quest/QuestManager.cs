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


    //퀘스트를 수락하면 딕셔너리에 넣음
    private Dictionary<int, Quest> _ongoingQuests = new();
    //퀘스트를 완료하면 해쉬셋에 넣음
    private HashSet<int> _completeQuests = new();
    //퀘스트 수락 콜백
    public event Action<int> OnQuestStartCallback;
    //퀘스트 카운트 업데이트 콜백
    public event Action<int, int> OnQuestUpdateCallback;
    //퀘스트 완료 콜백
    public event Action<int> OnQuestCompleteCallback;

    private Dictionary<QuestType, List<QuestData>> _subscribeQuests = new();

    /// <summary>
    /// SubscribeQuest는 QuestData 보관 QuestStart는 퀘스트 진행도와 상태체크
    /// </summary>

    //퀘스트 구독
    public void SubscribeQuest(int questId)
    {
        //만약 questID가 이미 진행중이거나 완료 햇다면 중복 구독 방지
        if (IsClear(questId) || IsProgressQuest(questId))
        {
            Debug.Log($"이미 ID:{questId} 퀘스트는 진행중이거나 완료하였습니다");
            return;
        }

        //퀘스트 데이터 불러오기
        var questData = Database.Quest.Get(questId);

        //구독 딕셔너리에 퀘스트타입이 존재하지 않으면 Add 딕셔너리
        if (_subscribeQuests.ContainsKey(questData.Type) == false)
        {
            _subscribeQuests[questData.Type] = new List<QuestData>();
        }

        _subscribeQuests[questData.Type].Add(questData);

        QuestStart(questId);

        Debug.Log($"ID:{questId} 퀘스트 구독 완료");
    }

    //퀘스트 구독 해제
    public void UnsubscribeQuest(int questId)
    {
        
        var questData = Database.Quest.Get(questId);

        if (_subscribeQuests.ContainsKey(questData.Type) == false)
            return;
        //구독 딕셔너리에 퀘스트 타입이 존재할 때만 Remove 딕셔너리
        _subscribeQuests[questData.Type].Remove(questData);
        Debug.Log($"ID:{questId} 퀘스트 구독 해제 완료");
    }
    
    //퀘스트 진행도 업데이트 (QuestType:대분류, target:소분류, count:실행횟수)
    public void NotifyQuest(QuestType type, int target, int count)
    {
        if (_subscribeQuests.ContainsKey(type) == false)
            return;
        //QuestType(대분류) 퀘스트 불러오기
        var filteredQuests = _subscribeQuests[type];
        //딕셔너리를 돌면서 target(소분류)와 목표와 같은 QuestData를 추출하여 List<QuestData>로 뽑아낸다
        List<QuestData> targetQuests = filteredQuests.FindAll(q => q.Target == target);
        //foreach돌면서 quest ID와 동일한 퀘스트만 실행횟수 업데이트
        foreach (var quest in targetQuests)
            QuestUpdate(quest.ID, count);
    }

    void QuestStart(int questId)
    {
        if (_ongoingQuests.ContainsKey(questId))
        {
            Debug.Log($"해당 ID:{questId} 로 이미 퀘스트가 진행중입니다");
            return;
        }
        
        //퀘스트를 생성하고 상태를 Wait에서 Progress로 변경
        var quest = new Quest(questId);
        quest.Start();
        //퀘스트를 진행중 딕셔너리에 추가
        _ongoingQuests.Add(questId, quest);
        //퀘스트를 수락 콜백 액션
        OnQuestStartCallback?.Invoke(questId);
    }

    public void QuestUpdate(int questId, int amount)
    {
        if (_ongoingQuests.ContainsKey(questId) == false)
            return;

        var questData = Database.Quest.Get(questId);

        //실행횟수를 업데이트 하고 누적된 횟수를 리턴받는다
        int currentCount = _ongoingQuests[questId].Update(amount);

        //_ongoingQuests[questId].Update(amount);

        //UI업데이트 용도로 사용할 예정
        OnQuestUpdateCallback?.Invoke(questId, amount);

        //현재 실행횟수가 퀘스트데이타의 목표횟수에 도달하면 퀘스트 클리어
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

        Debug.Log($"ID:{questId} 퀘스트 클리어!");

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
