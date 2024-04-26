using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
        switch (data.SkillType)
        {
            case SkillType.Casting:
                StartCoroutine(WaitForAnimationEnd());
                break;
            case SkillType.Shoot:
                StartCoroutine(ShootSkill());
                break;
            case SkillType.Whirl:
                StartCoroutine(WhirlingSkill());
                break;
        }
    }

    //제자리 시전 스킬
    IEnumerator WaitForAnimationEnd()
    {
        do
        {
            yield return null;
        } while (anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1);

        gameObject.SetActive(false);
    }

    //발사형 스킬
    IEnumerator ShootSkill()
    {
        if (data != null)
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

    //주위 회전형 스킬
    IEnumerator WhirlingSkill()
    {
        Vector3 centerPoint = new Vector3(0, 0, 0);
        float radius = 1f;
        float angularSpeed = 180f;
        float angle = SetStartAngle();

        while (true)
        {
            angle += angularSpeed * Time.deltaTime;

            if (angle >= 360f)
            {
                angle -= 360f;
            }

            float x = centerPoint.x + radius * Mathf.Cos(angle * Mathf.Deg2Rad);
            float y = centerPoint.y + radius * Mathf.Sin(angle * Mathf.Deg2Rad);

            transform.localPosition = new Vector3(x, y, transform.position.z);
            yield return null;
        }
    }

    protected virtual float SetStartAngle() { return 0f; }

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
