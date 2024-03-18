using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class EnemyStatusData : Status, IDamageable
{
    [SerializeField] protected SkillType type; //¶³¾îÆ®¸± Áª ¼Ó¼ºÀ» À§ÇØ

    public SkillType Type { get { return type; } set { type = value; } }

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
