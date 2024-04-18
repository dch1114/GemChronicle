using Constants;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    private int _questId;
    private int targetCount;
    private int currentCount;
    private bool bClear;
    public QuestRecompenseItem RewardItem;

    public int QuestId
    {
        get => _questId; 
        private set => _questId = value;
    }

    public int TargetCount
    {
        get => targetCount;
        private set => targetCount = value;
    }
    public int CurrentCount
    {
        get => currentCount;
        private set => currentCount = value;
    }

    public bool IsClearQuest
    {
        get => bClear;
        set => bClear = value;
    }

    public Quest(int questId, int questProgress)
    {
        _questId = questId;
        targetCount = questProgress;
    }

    public int Update(int amount)
    {
        currentCount += amount;

        if (currentCount > targetCount)
            currentCount = targetCount;
        return currentCount;
    }

}

[Serializable]
public class QuestRecompenseItem
{
    public InventoryItem Item;
    public int Quantity;
}
