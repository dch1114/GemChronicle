using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class EnemyStatusData : Status
{
    [SerializeField] protected SkillType type; //冻绢飘副 联 加己阑 困秦
    [SerializeField] protected int walkSpeed;
    [SerializeField] protected float attackRate;

    public SkillType Type { get { return type; } set { type = value; } }
    public int WalkSpeed { get { return walkSpeed; } set {  walkSpeed = value; } }
    public float AttackRate { get { return attackRate; } set { attackRate = value; } }
}
