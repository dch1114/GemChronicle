using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum JobType
{
    Warrior,
    Archer,
    Magician
}

public class PlayerStatusData : Status
{
    [SerializeField] protected int exp;
    [SerializeField] protected int level;
    [SerializeField] protected int gold;
    [SerializeField] protected string name;
    [SerializeField] protected JobType jobType;

    public int Exp {  get { return exp; } set {  exp = value; } }
    public int Level {  get { return level; } set { level = value; } }
    public int Gold { get { return gold; } set {  gold = value; } }
    public string Name { get { return name; } set { name = value; } }
    public JobType JobType { get {  return jobType; } set {  jobType = value; } }

    private void Start()
    {
        SetStatus();
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
}
