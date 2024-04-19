using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkill : Skill
{
    private void OnEnable()
    {
        CheckType();
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);

        if (damageable != null)
        {
            damageable.TakeDamage(data.Damage);
        }
    }
}
