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
    [SerializeField] protected int exp;
    [SerializeField] protected int level;
    [SerializeField] protected int gold;
    [SerializeField] protected JobType jobType;

    public string Name { get { return name; } set { name = value; } }
    public int Exp {  get { return exp; } set {  exp = value; } }
    public int Level {  get { return level; } set { level = value; } }
    public int Gold { get { return gold; } set {  gold = value; } }
    public JobType JobType { get {  return jobType; } set {  jobType = value; } }

    private void Start()
    {
        SetStatus();
    }

    private void SetStatus()
    {
        //TODO: ø¢ºø µ•¿Ã≈Õ ø¨µø
        Atk = 10;
        Def = 5;
        MaxHp = 100;
        Hp = maxHp;
        Exp = 0;
        Level = 1;
        Gold = 2000;
        Name = "«Ô∑Œ";
        JobType = JobType.Warrior;
    }

    private bool IsGoldEnough(int _price)
    {
        if (Gold - _price >= 0)
            return true;
        else
            return false;
    }

    public void UseGold(int _price)
    {
        if (IsGoldEnough(_price))
            gold -= _price;
        else
            Debug.Log("∞ÒµÂ∞° ∫Œ¡∑«’¥œ¥Ÿ");
    }
}
