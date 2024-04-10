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


    public Quest(int questId, int questProgress)
    {
        _questId = questId;
        targetCount = questProgress;
    }

    public int Update(int amount)
    {
        targetCount += amount;
        return targetCount;
    }

}

[Serializable]
public class QuestRecompenseItem
{
    public InventoryItem Item;
    public int Quantity;
}
