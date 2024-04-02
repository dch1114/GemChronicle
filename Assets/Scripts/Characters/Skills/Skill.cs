using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public Animator anim;

    public SkillInfoData data {  get; set; }
    private Player player;

    private Vector3 leftDirection = new Vector3(2f, 2f, 1f);
    private Vector3 rightDirection = new Vector3(-2f, 2f, 1f);

    private void OnEnable()
    {
        player = GameManager.Instance.player;

        if(player != null )
        {
            SetTransform();
            StartCoroutine(WaitForAnimationEnd());
        }
    }

    IEnumerator WaitForAnimationEnd()
    {
        do
        {
            yield return null;
        } while (anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1);

        gameObject.SetActive(false);
    }

    private void SetTransform()
    {
        Vector3 plus = Vector3.left;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();

        if(damageable != null)
        {
            damageable.TakeDamage(player.Data.StatusData.Atk + data.Damage);
        }
    }
}
