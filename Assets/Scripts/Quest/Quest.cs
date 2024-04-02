using Constants;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu]
public class Quest
{
    private int _questId;
    private int _questProgress;
    private QuestState _questState;

    public int QuestId
    {
        get => _questId; 
        private set => _questId = value;
    }

    public int QuestProgress
    {
        get => _questProgress;
        private set => _questProgress = value;
    }

    public QuestState QuestState
    {
        get => _questState;
        private set => _questState = value;
    }

    public Quest(int questId)
    {
        _questId = questId;
        _questProgress = 0;
        _questState = QuestState.Waite;
    }

    public Quest(int questId, int questProgress, QuestState questState)
    {
        _questId = questId;
        _questProgress = questProgress;
        _questState = questState;
    }

    public void Start()
    {
        _questState = QuestState.Progress;
    }

    public int Update(int amount)
    {
        _questProgress += amount;
        return _questProgress;
    }

    public void Complete()
    {
        _questState = QuestState.Complete;
    }









    public static Action<Quest> EventQuestCompleted;

    [Header("Info")]
    public string Name;
    public string ID;
    public int TargetQuantity;

    [Header("Descripcion")]
    [TextArea] public string Description;

    [Header("Rewards")]
    public int RewardGold;
    public float RewardExp;
    public QuestRecompenseItem RewardItem;
    public string potalID;
    [HideInInspector] public int CurrentQuantity;
    [HideInInspector] public bool QuestCompletedCheck;

    public void AddProgress(int quantity)
    {
        CurrentQuantity += quantity;
        CheckQuestCompleted();
    }

    private void CheckQuestCompleted()
    {
        if (CurrentQuantity >= TargetQuantity)
        {
            CurrentQuantity = TargetQuantity;
            QuestCompleted();
        }
    }

    private void QuestCompleted()
    {
        if (QuestCompletedCheck)
        {
            return;
        }

        QuestCompletedCheck = true;
        EventQuestCompleted?.Invoke(this);
    }

    private void OnEnable()
    {
        QuestCompletedCheck = false;
        CurrentQuantity = 0;
    }
}

[Serializable]
public class QuestRecompenseItem
{
    public InventoryItem Item;
    public int Quantity;
}
