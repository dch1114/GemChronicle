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

    [SerializeField] private int _reward_1;
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
    public int Gold => _gold;
    public bool Continue => _continue;
    public bool Talk => _talk;
    public bool Finish => _finish;

    public int RewardCount_1 => _rewardCount_1;


    public int UnLockPotalIndex => _unlockpotalindex;

    private List<Reward> _rewardList;

    public List<Reward> RewardList
    {
        get
        {
            if (_rewardList == null)
            {
                _rewardList = new List<Reward>();

                CheckReward(_reward_1, _rewardCount_1);
                CheckReward(_reward_2, _rewardCount_2);
                CheckReward(_reward_3, _rewardCount_3);
            }

            return _rewardList;
        }
    }

    private void CheckReward(int rewardId, int rewardCount)
    {
        if (rewardId != 0)
            _rewardList.Add(new Reward(rewardId, rewardCount));
    }
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
