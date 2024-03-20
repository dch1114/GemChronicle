using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class EnemyStatusData : Status, IDamageable
{
    [SerializeField] protected SkillType type; //¶³¾îÆ®¸± Áª ¼Ó¼ºÀ» À§ÇØ
    [SerializeField] protected int walkSpeed;
    [SerializeField] protected float attackRate;

    public SkillType Type { get { return type; } set { type = value; } }
    public int WalkSpeed { get { return walkSpeed; } set {  walkSpeed = value; } }
    public float AttackRate { get { return attackRate; } set { attackRate = value; } }
    public void TakeDamage(int damage)
    {
        if (hp - damage > 0)
        {
            hp -= damage;
        }
        else
        {
            //TODO: »ç¸Á ±¸Çö
        }
    }
}
