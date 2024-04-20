using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : Skill
{
    private Player player;

    private Vector3 leftDirection = new Vector3(2f, 2f, 1f);
    private Vector3 rightDirection = new Vector3(-2f, 2f, 1f);

    private void OnEnable()
    {
        player = GameManager.Instance.player;

        if (player != null)
        {
            SetTransform();
            CheckType();
        }
    }

    protected override void SetTransform()
    {
        Vector3 plus = new Vector3(-0.5f, 0, 0);
        if (!player.Controller.isLeft)
        {
            plus.x *= -1f;
            Flip(false);
        }
        else Flip(true);
        transform.position = player.transform.position + plus;
    }

    private void Flip(bool isLeft)
    {
        transform.localScale = isLeft ? leftDirection : rightDirection;
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);

        if (damageable != null)
        {
            SoundManager.Instance.PlayClip(SoundManager.Instance.attackSound); //test
            damageable.TakeDamage(player.Data.StatusData.Atk + data.Damage);
            if(data.Range > 0) gameObject.SetActive(false);
        }
    }

    protected override Vector3 GetTargetPosition()
    {
        return player.Controller.isLeft ? new Vector3(data.Range * -1, 0, 0) : new Vector3(data.Range, 0, 0);
    }
}
