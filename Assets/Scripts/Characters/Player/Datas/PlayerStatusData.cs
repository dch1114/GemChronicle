using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum JobType
{
    Warrior,
    Archer,
    Magician
}

[Serializable]
public class PlayerStatusData : Status
{
    [SerializeField] protected string name;
    [SerializeField] protected int maxHp;
    [SerializeField] protected int requiredExp;
    [SerializeField] protected int level;
    [SerializeField] protected int gold;
    [SerializeField] protected JobType jobType;
    [SerializeField] protected Dictionary<SkillType, int> gems = new Dictionary<SkillType, int>();

    public string Name { get { return name; } set { name = value; } }
    public int MaxHp { get { return maxHp; } set { maxHp = value; } }
    public int Level {  get { return level; } set { level = value; } }
    public int Gold { get { return gold; } set {  gold = value; } }
    public JobType JobType { get {  return jobType; } set {  jobType = value; } }
    public Dictionary<SkillType, int> Gems { get { return gems; } set { gems = value; } }

    public void InitializeData()
    {
        InitializeGem();
    }

    public void LoadLevelData(LevelData data)
    {
        atk = data.atk;
        def = data.def;
        maxHp = data.maxHp;
        requiredExp = data.requiredExp;
    }

    private void SetStatus()
    {
        //TODO: ���� ������ ����
        Atk = 10;
        Def = 5;
        MaxHp = 100;
        Hp = maxHp;
        Exp = 0;
        Level = 1;
        Gold = 2000;
        Name = "���";
        JobType = JobType.Warrior;
    }

    private void InitializeGem()
    {
        gems.Add(SkillType.Ice, 0);
        gems.Add(SkillType.Fire, 0);
        gems.Add(SkillType.Light, 0);
        gems.Add(SkillType.Dark, 0);
    }

    private bool IsGoldEnough(int _price)
    {
        if (Gold - _price >= 0)
            return true;
        else
            return false;
    }

    public bool UseGold(int _price)
    {
        if (IsGoldEnough(_price))
        {
            gold -= _price;
            return true;
        }
        else
            return false;
    }

    public void TakeDamage(int damage)
    {
        if(hp - damage > 0)
        {
            hp -= damage;
        } else
        {
            OnDie();
        }
    }

    private void OnDie()
    {
        //�������� ������
        
    }

    private bool IsGemEnough(SkillType gemType, int _amount)
    {
        if (gems.ContainsKey(gemType))
        {
            if (Gems[gemType] - _amount >= 0)
                return true;
            else
                return false;
        } else
        {
            InitializeGem();
            return false;
        }
    }

    public void GetGem(SkillType gemType)
    {
        if (gems.ContainsKey(gemType)) gems[gemType]++;
    }

    public void GetGems(SkillType gemType, int amount)
    {
        if (gems.ContainsKey(gemType)) gems[gemType] += amount;
    }

    public bool UseGems(SkillType gemType, int _amount)
    {
        if(IsGemEnough(gemType, _amount))
        {
            Gems[gemType] -= _amount;
            return true;
        } else
        {
            return false;
        }
    }

    public void TakeHeal(int _recovery)
    {
        if (hp + _recovery >= MaxHp)
        {
            hp = MaxHp;
        }
        else
        {
            hp += _recovery;
        }
    }

}
