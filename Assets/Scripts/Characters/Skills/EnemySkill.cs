using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class EnemySkill : Skill
{
    [SerializeField] bool startAnglePoint;

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
        if(GameManager.Instance.player.transform.position.x < transform.position.x)
        {
            Flip(true);
            return new Vector3(data.Range * -1, 0, 0);
        } else
        {
            Flip(false);
            return new Vector3(data.Range, 0, 0);
        }
    }

    private void Flip(bool isLeft)
    {
        if (isLeft)
            transform.localScale = new Vector3(1f, 1f, 1f);
        else
            transform.localScale = new Vector3(-1f, 1f, 1f);
    }

    protected override float SetStartAngle()
    {
        if (startAnglePoint) return 0f;
        else return 180f;
    }
}
