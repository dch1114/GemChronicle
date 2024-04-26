using Constants;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class QuestData
{
    [SerializeField] private int _id;
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private QuestType _type;
    [SerializeField] private int _target;
    [SerializeField] private int _count;
    [SerializeField] private int _exp;
    [SerializeField] private int _gold;
    [SerializeField] private bool _continue;
    [SerializeField] private bool _talk;
    [SerializeField] private bool _finish;
    [SerializeField] private ElementType _reward_1;
    [SerializeField] private int _rewardCount_1;
    [SerializeField] private int _reward_2;
    [SerializeField] private int _rewardCount_2;
    [SerializeField] private int _reward_3;
    [SerializeField] private int _rewardCount_3;

    [SerializeField] private int _unlockpotalindex;

    public int ID => _id;
    public string Name => _name;
    public string Description => _description;
    public QuestType Type => _type;
    public int Target => _target;
    public int Count => _count;
    public int Exp => _exp;
    public ElementType rewardgem => _reward_1;
    public int Gold => _gold;
    public int Gem => _rewardCount_1;
    public bool Continue => _continue;
    public bool Talk => _talk;
    public bool Finish => _finish;



    public int UnLockPotalIndex => _unlockpotalindex;



}

public class Reward
{
    public int _rewardId { get; }
    public int _rewardCount { get; }

    public Reward(int rewardId, int rewardCount)
    {
        _rewardId = rewardId;
        _rewardCount = rewardCount;
    }
}
