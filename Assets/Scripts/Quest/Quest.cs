using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Quest : ScriptableObject
{
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
