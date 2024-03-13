using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Status
{
    [SerializeField] protected int atk;
    [SerializeField] protected int def;
    [SerializeField] protected int maxHp;
    [SerializeField] protected int hp;

    public int Atk { get { return atk; } set { atk = value; } }
    public int Def { get { return def; } set { def = value; } }
    public int MaxHp { get {  return maxHp; } set {  maxHp = value; } }
    public int Hp { get { return hp; } set {  hp = value; } }
}
