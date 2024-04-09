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

        if(player != null)
        {
            SetTransform();
            CheckType();
        }
    }

    private void CheckType()
    {
        switch(player.Data.StatusData.JobType)
        {
            case JobType.Archer:
                StartCoroutine(ShootSkill());
                break;
            default:
                StartCoroutine(WaitForAnimationEnd());
                break;
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

    IEnumerator ShootSkill()
    {
        if(data != null)
        {
            Vector3 originalPosition = transform.position;
            Vector3 targetPosition = originalPosition + (player.Controller.isLeft ? new Vector3(data.Range * -1, 0, 0) : new Vector3(data.Range, 0, 0));

            do
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * 7f);
                yield return null;
            } while (transform.position != targetPosition);

            gameObject.SetActive(false);
        }
    }

    private void SetTransform()
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();

        if(damageable != null)
        {
            damageable.TakeDamage(player.Data.StatusData.Atk + data.Damage);
            gameObject.SetActive(false);
        }
    }
}
