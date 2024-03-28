using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Playables;
using UnityEngine;

[Serializable]
public class Status
{
    [SerializeField] protected int atk;
    [SerializeField] protected int def;
    [SerializeField] protected int hp;
    [SerializeField] protected int exp;

    public int Atk { get { return atk; } set { atk = value; } }
    public int Def { get { return def; } set { def = value; } }
    public int Hp { get { return hp; } set {  hp = value; } }
    public int Exp { get { return exp; } set { exp = value; } }

    //test
    public int GetStatus(StatusType _statusType)
    {
        switch (_statusType)
        {
            case StatusType.Attack:
                return Atk;
            case StatusType.Defense:
                return Def;
            case StatusType.MaxHp:
                return Hp;
            default:
                return 0;
        }
    }
}

public enum StatusType
{
    Attack,
    Defense,
    MaxHp
}
