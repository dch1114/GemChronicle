using Constants;
using UnityEngine;

public class QuestData
{
    [SerializeField]private int _id;
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private QuestType _type;
    [SerializeField] private int _target;
    [SerializeField] private int _count;
    [SerializeField] private int _exp;
    [SerializeField] private int _gold;

    [SerializeField] private int _reward_1;
    [SerializeField] private int _rewardCount_1;
    [SerializeField] private int _reward_2;
    [SerializeField] private int _rewardCount_2;
    [SerializeField] private int _reward_3;
    [SerializeField] private int _rewardCount_3;

    public int ID => _id;
    public string Name => _name;
    public string Description => _description;
    public QuestType Type => _type;
    public int Target => _target;
    public int Count => _count;
    public int Exp => _exp;
    public int Gold => _gold;

}
