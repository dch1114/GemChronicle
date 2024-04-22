using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkill : Skill
{
    [SerializeField] bool targetLeft;

    private void OnEnable()
    {
        CheckType();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if (damageable != null)
        {
            damageable.TakeDamage(data.Damage);
        }
    }

    protected override Vector3 GetTargetPosition()
    {
        if(targetLeft)
        {
            return new Vector3(data.Range * -1, 0, 0);
        } else
        {
            return new Vector3(data.Range, 0, 0);
        }
    }
}
