using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public Animator anim;

    [SerializeField]
    public SkillInfoData data;

    protected IDamageable damageable;

    protected void CheckType()
    {
        if(data.Range > 0)
        {
            StartCoroutine(ShootSkill());
        } else
        {
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

    IEnumerator ShootSkill()
    {
        if(data != null)
        {
            Vector3 originalPosition = transform.position;
            Vector3 targetPosition = originalPosition + GetTargetPosition();

            do
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * 7f);
                yield return null;
            } while (transform.position != targetPosition);

            gameObject.SetActive(false);
        }
    }

    protected virtual void SetTransform() { }

    protected virtual Vector3 GetTargetPosition() { return Vector3.left; }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        damageable = collision.gameObject.GetComponent<IDamageable>();
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        damageable = collision.gameObject.GetComponent<IDamageable>();
    }

    //test
    //protected virtual void PlayAttackSound()
    //{
    //    SoundManager.PlayClip(attackSound);
    //}
}
