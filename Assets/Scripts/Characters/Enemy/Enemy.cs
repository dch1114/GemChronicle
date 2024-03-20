using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    Idle,
    Walking,
    Attacking,
    Dead
}

public class Enemy : MonoBehaviour
{
    [SerializeField] public EnemyStatusData EnemyStatusData;
    [SerializeField] public EnemyAnimationData EnemyAnimationData;

    private Rigidbody2D rigid;
    private Animator animator;

    private int nextMove;
    private bool isLeft = true;
    private bool foundEnemy = false;
    private bool canAttack = true;

    private void Awake()
    {
        EnemyAnimationData.Initialize();

        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        Invoke("Think", 5);
    }

    void FixedUpdate()
    {
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);
        if (nextMove != 0)
        {
            SetState(EnemyState.Walking);
        }

        if ((nextMove > 0 && isLeft) || (nextMove < 0 && !isLeft))
        {
            Flip();
        }

        NextGround();

        CheckPlayer();
    }

    private void UpdateIdle()
    {

    } 

    private void UpdateWalking()
    {
        if(foundEnemy)
        {
            SetState(EnemyState.Attacking);
        } else
        {
            Move();
        }
    }

    private void UpdateAttacking()
    {
        if(foundEnemy && canAttack)
        {
            canAttack = false;
            SetState(EnemyState.Attacking);
            Invoke("WaitAttackCoolTime", EnemyStatusData.AttackRate);
        } else
        {
            SetState(EnemyState.Idle);
        }
    }

    protected void SetState(EnemyState newState)
    {
        switch(newState)
        {
            case EnemyState.Idle:
                animator.SetBool(EnemyAnimationData.WalkParameterHash, false);
                break;
            case EnemyState.Walking:
                animator.SetBool(EnemyAnimationData.WalkParameterHash, true);
                break;
            case EnemyState.Attacking:
                animator.SetTrigger(EnemyAnimationData.AttackParameterHash);
                break;
            case EnemyState.Dead:
                animator.SetTrigger(EnemyAnimationData.DieParameterHash);
                break;
        }
    }
    private void WaitAttackCoolTime()
    {
        canAttack = true;
    }

    private void Move()
    {

    }

    private void NextGround()
    {
        Vector2 frontVec = new Vector2(transform.position.x + nextMove * 0.4f, transform.position.y);
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Ground"));
        if (rayHit.collider == null)
        {
            nextMove = nextMove * -1;
            CancelInvoke();
            Invoke("Think", 2);
        }
    }

    private void CheckPlayer()
    {
        Vector3 direction = Vector3.left;
        Debug.DrawRay(transform.position, direction, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(transform.position, Vector3.left, 2f, LayerMask.GetMask("Player"));
        if(rayHit.collider == null)
        {
            foundEnemy = false;
        } else
        {
            foundEnemy = true;
        }
        Debug.Log(foundEnemy);
    }

    // Àç±Í ÇÔ¼ö
    void Think()
    {
        nextMove = Random.Range(-1, 2);
        SetState(EnemyState.Idle);

        float nextThinkTime = Random.Range(2f, 5f);
        Invoke("Think", nextThinkTime);
    }

    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;

        transform.localScale = scale;
        isLeft = !isLeft;
    }
}
