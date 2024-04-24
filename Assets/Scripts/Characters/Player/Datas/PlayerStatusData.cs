using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Drawing.Inspector.PropertyDrawers;
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
    public int RequiredExp { get { return requiredExp; } set { requiredExp = value; } }
    public int Level {  get { return level; } set { level = value; } }
    public int Gold { get { return gold; } set {  gold = value; } }
    public JobType JobType { get {  return jobType; } set {  jobType = value; } }
    public Dictionary<SkillType, int> Gems { get { return gems; } set { gems = value; } }

    public void LoadLevelData(LevelData data)
    {
        atk = data.atk;
        def = data.def;
        maxHp = data.maxHp;
        requiredExp = data.requiredExp;

        UIManager.Instance.playerUI.UpdateStatus();
    }

    private void SetStatus()
    {
        //TODO: Maybe on GameStart
        Atk = 10;
        Def = 5;
        MaxHp = 100;
        Hp = maxHp;
        Exp = 0;
        Level = 1;
        Gold = 2000;
        Name = "Hello";
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
            UIManager.Instance.playerUI.UpdateGold();
            return true;
        }
        else
            return false;
    }

    public void GetGold(int _price)
    {
        gold += _price;
        UIManager.Instance.playerUI.UpdateGold();
    }

    public void TakeDamage(int damage)
    {
        float realDamage = damage * 1.2f - def * 0.2f;

        if (hp - realDamage > 0)
        {
            if (realDamage > 0)
            {
                hp -= (int)Math.Floor(realDamage);
                UIManager.Instance.playerUI.UpdateHp();
            }
            else
            {
                hp -= (int)Math.Floor(realDamage); // HP 감소
                UIManager.Instance.playerUI.UpdateHp(); // UI 업데이트
                //TODO: SHOW DAMAGE 0
            }
        }
        else
        {
            hp = 0;
            OnDie();
        }
        UIManager.Instance.playerUI.UpdateHp();
    }


    private void OnDie()
    {
        //State Change
        PlayerStateMachine stateMachine = GameManager.Instance.player.GetStateMachine();
        stateMachine.ChangeState(stateMachine.DieState);
        UIManager.Instance.diePanelUI.ActiveDiePanel();
    }

    public void GetExp(int _amount)
    {
        if (exp + _amount >= requiredExp)
        {
            exp = exp + _amount - requiredExp;
            LevelUP();
        } else
        {
            exp += _amount;
        }

        UIManager.Instance.playerUI.UpdateExp();
    }

    private void LevelUP()
    {
        level++;
        PlayerDataManager.Instance.SetPlayerLevel();

        Debug.Log(level);
        Inventory.Instance.InventoryUIController.UpdateStatus();
        Debug.Log(atk);
        //레벨 2개 건너뛰는거 체크
        GetExp(0);
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

    public void GetGems(SkillType gemType, int amount)
    {
        if (gems.ContainsKey(gemType)) gems[gemType] += amount;
        UIManager.Instance.skillPages.UpdateGems();
    }

    public bool UseGems(SkillType gemType, int _amount)
    {
        if(IsGemEnough(gemType, _amount))
        {
            Gems[gemType] -= _amount;
            UIManager.Instance.skillPages.UpdateGems();
            return true;
        } else
        {
            UIManager.Instance.alertPanelUI.ShowAlert("젬이 부족합니다.");
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

        UIManager.Instance.playerUI.UpdateHp();
    }

    public void HealFull()
    {
        hp = maxHp;

        UIManager.Instance.playerUI.UpdateHp();
    }

}
