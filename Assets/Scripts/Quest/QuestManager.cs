using Constants;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Linq;
using UnityEditor.PackageManager.Requests;
using System.Xml.Linq;
using UnityEngine.SceneManagement;

public class QuestManager : Singleton<QuestManager>
{
    [Header("Waiting Quests")]
    [SerializeField] private WaitingQuestDescription waitingQuestPrefab;
    [SerializeField] private Transform waitingQuestParent;
    private QuestData questData;
    [Header("Progress Quests")]
    [SerializeField] private ProgressQuestDescription progressQuestPrefab;
    [SerializeField] private Transform progressQuestParent;

    [Header("Panel Quest Completed")]
    [SerializeField] private Transform completeQuestParent;

    int startQuestID = 2000;

    //현재 들고 있는 퀘스트데이타
    public QuestData currentProgressMainQuestData = null;
    public QuestData currentProgressSubQuestData = null;

    public Quest QuestUnclaimed { get; private set; }

    [SerializeField] PanelQuestUI panelQuestUI;
    [SerializeField] MiniQuest miniQuest;
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

    public IEnumerator InitQuestManager()
    {
        yield return null;
        SetAllQuestUI();
        currentProgressMainQuestData = Database.Quest.Get(startQuestID);
        SubscribeQuest(startQuestID);
    }

    //퀘스트 구독
    public void SubscribeQuest(int questId)
    {
        
       
            if (_completeQuests.Contains(questId))
            {
                Debug.Log($"이미 ID:{questId} 퀘스트는 완료하였습니다");
                return;
            }

            if (_ongoingQuests.ContainsKey(questId))
            {
                Debug.Log($"이미 ID:{questId} 퀘스트는 진행중입니다");
                return;
            }

            //퀘스트 데이터 불러오기
            var questData = Database.Quest.Get(questId);

            if (questData != null )
            {
                //구독 딕셔너리에 퀘스트타입이 존재하지 않으면 Add 딕셔너리
                if (_subscribeQuests.ContainsKey(questData.Type) == false)
                {
                    _subscribeQuests[questData.Type] = new List<QuestData>();
                }

                _subscribeQuests[questData.Type].Add(questData);

                QuestStart(questId);

                Debug.Log($"ID:{questId} 퀘스트 구독 완료");
            }
        

       

    }

    //퀘스트 구독 해제
    public void UnsubscribeQuest(int questId)
    {
        
        var questData = Database.Quest.Get(questId);

        if (questData != null)
        {
            if (_subscribeQuests.ContainsKey(questData.Type) == false)
                return;
            //구독 딕셔너리에 퀘스트 타입이 존재할 때만 Remove 딕셔너리
            _subscribeQuests[questData.Type].Remove(questData);
            Debug.Log($"ID:{questId} 퀘스트 구독 해제 완료");
        }

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

        var questData = Database.Quest.Get(questId);

        if (questData != null)
        {
          
                //퀘스트를 생성하고 상태를 Wait에서 Progress로 변경
                var quest = new Quest(questId, questData.Count);
            //퀘스트를 진행중 딕셔너리에 추가
           
                _ongoingQuests.Add(questId, quest);
                Debug.Log($"ID:{questId} 퀘스트 시작");
            if (questId < 3000)
            {
                SetAddProgressQuestUI(quest, questData);
                //퀘스트를 수락 콜백 액션
                OnQuestStartCallback?.Invoke(questId);
            }
         
        }


    }

    public void QuestUpdate(int questId, int amount)
    {
        Debug.Log($"ID:{questId} 카운트 업데이트");

        if (_ongoingQuests.ContainsKey(questId) == false)
            return;

        var questData = Database.Quest.Get(questId);

        if (questData != null)
        {
            //실행횟수를 업데이트 하고 누적된 횟수를 리턴받는다
            int currentCount = _ongoingQuests[questId].Update(amount);
            int targetCount = _ongoingQuests[questId].TargetCount;

            //현재 실행횟수가 퀘스트데이타의 목표횟수에 도달하면 퀘스트 클리어
            if (currentCount >= targetCount)
            {
                Debug.Log($"<color=red>퀘스트를 완료 받으세요</color>");
                _ongoingQuests[questId].IsClearQuest = true;
            }

            //UI업데이트 용도로 사용할 예정
            OnQuestUpdateCallback?.Invoke(questId, amount);
        }

    }



    public void QuestClear(int questId)
    {

        if (_ongoingQuests.ContainsKey(questId) == false)
            return;

        if (QuestManager.Instance.GetCurrentQuestData().Finish)
        {
            //특정 문구 표시후
            //씬이동
            SceneManager.LoadScene("Ending");
            return;
        }

        GameManager.Instance.player.Data.StatusData.GetGems(currentProgressMainQuestData.RwardType, currentProgressMainQuestData.RewardCount_1);

        _ongoingQuests.Remove(questId);

        _completeQuests.Add(questId);

        Debug.Log($"ID:{questId} 퀘스트 클리어!");
        //SetClearProgressQuestUI(questId);
        OnQuestCompleteCallback?.Invoke(questId);

        QuestCompleteCallBack(questId);


        if (IsContinueQuest())
        {
            SubscribeQuest(currentProgressMainQuestData.ID);
        }
    }

    void QuestCompleteCallBack(int id)
    {
        int questId = id + 1;
        currentProgressMainQuestData = Database.Quest.Get(questId);
    }

    public bool IsClear(int id)
    {
        return _completeQuests.Contains(id);
    }

    public bool IsProgressQuest(int id)
    {
        return _ongoingQuests.ContainsKey(id);

    }

    public bool IsContinueQuest()
    {
        return currentProgressMainQuestData.Continue;
    }
    public bool IsOnTalk()
    {
        return currentProgressMainQuestData.Talk;
    }

    public bool CheckCompareTargetID(int id)
    {
        if (id == currentProgressMainQuestData.Target)
            return true;
        return false;
    }

    public bool IsClearProgressQuest(int questId)
    {
        if (_ongoingQuests[questId].IsClearQuest)
        {
            return true;
        }

        return false;
    }

    public QuestData GetCurrentQuestData()
    {
        return currentProgressMainQuestData;
    }

    //수락 가능한 모든 퀘스트를 UI에 세팅
    void SetAllQuestUI()
    {
        var totalQuests = Database.Quest.GetTotalQuest();

        foreach (var questData in totalQuests)
        {
            WaitingQuestDescription newQuest = Instantiate(waitingQuestPrefab, waitingQuestParent);
            //questData.Key는 ID를 의미
            var quest = new Quest(questData.Key, questData.Value.Count);
            //questData.Value는 QuestData를 의미
            newQuest.ConfigureQuestUI(quest, questData.Value);

            panelQuestUI.AddWaitingQuest(questData.Key, newQuest.gameObject);
        }

    }

    void SetUIRemoveWaitingQuest(int key)
    {
        panelQuestUI.RemoveWaitingQuest(key);

    }


    void SetAddProgressQuestUI(Quest questcompleted, QuestData qData)
    {
        //ProgressQuestDescription newQuest = Instantiate(progressQuestPrefab, progressQuestParent);
        //newQuest.ConfigureQuestUI(questcompleted, qData);
        //panelQuestUI.AddProgressQuest(questcompleted.QuestId, newQuest.gameObject);
        SetUIRemoveWaitingQuest(questcompleted.QuestId);
        miniQuest.SetMiniQuest(questcompleted, qData);
    }

    public void SetClearProgressQuestUI(int key)
    {
        panelQuestUI.RemoveProgressQuest(key);
    }

}
