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

    //���� ��� �ִ� ����Ʈ����Ÿ
    public QuestData currentProgressMainQuestData = null;
    public QuestData currentProgressSubQuestData = null;

    public Quest QuestUnclaimed { get; private set; }

    [SerializeField] PanelQuestUI panelQuestUI;
    [SerializeField] MiniQuest miniQuest;
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

    public IEnumerator InitQuestManager()
    {
        yield return null;
        SetAllQuestUI();
        currentProgressMainQuestData = Database.Quest.Get(startQuestID);
        SubscribeQuest(startQuestID);
    }

    //����Ʈ ����
    public void SubscribeQuest(int questId)
    {
        
       
            if (_completeQuests.Contains(questId))
            {
                Debug.Log($"�̹� ID:{questId} ����Ʈ�� �Ϸ��Ͽ����ϴ�");
                return;
            }

            if (_ongoingQuests.ContainsKey(questId))
            {
                Debug.Log($"�̹� ID:{questId} ����Ʈ�� �������Դϴ�");
                return;
            }

            //����Ʈ ������ �ҷ�����
            var questData = Database.Quest.Get(questId);

            if (questData != null )
            {
                //���� ��ųʸ��� ����ƮŸ���� �������� ������ Add ��ųʸ�
                if (_subscribeQuests.ContainsKey(questData.Type) == false)
                {
                    _subscribeQuests[questData.Type] = new List<QuestData>();
                }

                _subscribeQuests[questData.Type].Add(questData);

                QuestStart(questId);

                Debug.Log($"ID:{questId} ����Ʈ ���� �Ϸ�");
            }
        

       

    }

    //����Ʈ ���� ����
    public void UnsubscribeQuest(int questId)
    {
        
        var questData = Database.Quest.Get(questId);

        if (questData != null)
        {
            if (_subscribeQuests.ContainsKey(questData.Type) == false)
                return;
            //���� ��ųʸ��� ����Ʈ Ÿ���� ������ ���� Remove ��ųʸ�
            _subscribeQuests[questData.Type].Remove(questData);
            Debug.Log($"ID:{questId} ����Ʈ ���� ���� �Ϸ�");
        }

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

        var questData = Database.Quest.Get(questId);

        if (questData != null)
        {
          
                //����Ʈ�� �����ϰ� ���¸� Wait���� Progress�� ����
                var quest = new Quest(questId, questData.Count);
            //����Ʈ�� ������ ��ųʸ��� �߰�
           
                _ongoingQuests.Add(questId, quest);
                Debug.Log($"ID:{questId} ����Ʈ ����");
            if (questId < 3000)
            {
                SetAddProgressQuestUI(quest, questData);
                //����Ʈ�� ���� �ݹ� �׼�
                OnQuestStartCallback?.Invoke(questId);
            }
         
        }


    }

    public void QuestUpdate(int questId, int amount)
    {
        Debug.Log($"ID:{questId} ī��Ʈ ������Ʈ");

        if (_ongoingQuests.ContainsKey(questId) == false)
            return;

        var questData = Database.Quest.Get(questId);

        if (questData != null)
        {
            //����Ƚ���� ������Ʈ �ϰ� ������ Ƚ���� ���Ϲ޴´�
            int currentCount = _ongoingQuests[questId].Update(amount);
            int targetCount = _ongoingQuests[questId].TargetCount;

            //���� ����Ƚ���� ����Ʈ����Ÿ�� ��ǥȽ���� �����ϸ� ����Ʈ Ŭ����
            if (currentCount >= targetCount)
            {
                Debug.Log($"<color=red>����Ʈ�� �Ϸ� ��������</color>");
                _ongoingQuests[questId].IsClearQuest = true;
            }

            //UI������Ʈ �뵵�� ����� ����
            OnQuestUpdateCallback?.Invoke(questId, amount);
        }

    }



    public void QuestClear(int questId)
    {

        if (_ongoingQuests.ContainsKey(questId) == false)
            return;

        if (QuestManager.Instance.GetCurrentQuestData().Finish)
        {
            //Ư�� ���� ǥ����
            //���̵�
            SceneManager.LoadScene("Ending");
            return;
        }

        GameManager.Instance.player.Data.StatusData.GetGems(currentProgressMainQuestData.RwardType, currentProgressMainQuestData.RewardCount_1);

        _ongoingQuests.Remove(questId);

        _completeQuests.Add(questId);

        Debug.Log($"ID:{questId} ����Ʈ Ŭ����!");
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

    //���� ������ ��� ����Ʈ�� UI�� ����
    void SetAllQuestUI()
    {
        var totalQuests = Database.Quest.GetTotalQuest();

        foreach (var questData in totalQuests)
        {
            WaitingQuestDescription newQuest = Instantiate(waitingQuestPrefab, waitingQuestParent);
            //questData.Key�� ID�� �ǹ�
            var quest = new Quest(questData.Key, questData.Value.Count);
            //questData.Value�� QuestData�� �ǹ�
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
