using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Tilemaps;
using UnityEngine;

public enum EnemyState
{
    Idle,
    Walking,
    Attacking,
    Dead
}

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] public EnemyStatusData EnemyStatusData;
    [SerializeField] public EnemyAnimationData EnemyAnimationData;

    private Rigidbody2D rigid;
    private Animator animator;

    private EnemyState state;

    private bool isLeft = true;
    private bool foundEnemy = false;
    private bool canAttack = true;
    private Vector3 nextMove = Vector3.left;
    private Vector3 distance = new Vector3(1.5f, 0, 0);
    private Vector3 leftDirection;
    private Vector3 rightDirection;

    private IDamageable playerCollider;
    private void Awake()
    {
        EnemyAnimationData.Initialize();

        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        leftDirection = transform.localScale;
        rightDirection = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);

        Invoke("Think", 5);
    }

    void FixedUpdate()
    {
        CheckPlayer();

        switch (state)
        {
            case EnemyState.Idle:
                UpdateIdle();
                break;
            case EnemyState.Walking:
                UpdateWalking();
                break;
            case EnemyState.Attacking:
                UpdateAttacking();
                break;
            case EnemyState.Dead:
                break;
        }
    }

    private void UpdateIdle()
    {
        if(foundEnemy)
        {
            SetState(EnemyState.Attacking);
        } else
        {
            if(nextMove.x != 0)
            {
                SetState(EnemyState.Walking);
            }
        }
    } 

    private void UpdateWalking()
    {
        NextGround();

        if (foundEnemy)
        {
            SetState(EnemyState.Attacking);
        }
        else
        {
            Move(nextMove * Time.deltaTime);
            CheckDirection();
            Look();
        }
    }

    private void UpdateAttacking()
    {
        if(foundEnemy && canAttack)
        {
            if (playerCollider != null)
            {
                playerCollider.TakeDamage(EnemyStatusData.Atk);
            }
            canAttack = false;
            StartCoroutine("WaitAttackCoolTime", EnemyStatusData.AttackRate);
        } else
        {
            SetState(EnemyState.Idle);
        }
    }

    protected void SetState(EnemyState newState)
    {
        state = newState;

        switch(newState)
        {
            case EnemyState.Idle:
                animator.SetBool(EnemyAnimationData.WalkParameterHash, false);
                animator.SetBool(EnemyAnimationData.AttackParameterHash, false);
                break;
            case EnemyState.Walking:
                animator.SetBool(EnemyAnimationData.WalkParameterHash, true);
                break;
            case EnemyState.Attacking:
                animator.SetBool(EnemyAnimationData.AttackParameterHash, true);
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

    private void Move(Vector3 _speed)
    {
        transform.Translate(_speed);
    }

    private void NextGround()
    {
        Vector2 frontVec = new Vector2(transform.position.x + nextMove.x * 0.4f, transform.position.y);
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
        Debug.DrawRay(transform.position + distance, Vector3.left, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(transform.position + distance, Vector3.left, 3f, LayerMask.GetMask("Player"));
        if(rayHit.collider == null)
        {
            foundEnemy = false;
        } else
        {
            playerCollider = rayHit.collider.gameObject.GetComponent<IDamageable>();
            foundEnemy = true;
        }
    }

    void Think()
    {
        nextMove.x = Random.Range(-1, 2);
        SetState(EnemyState.Idle);

        float nextThinkTime = Random.Range(2f, 5f);
        Invoke("Think", nextThinkTime);
    }

    private void Look()
    {
        transform.localScale = isLeft ? leftDirection : rightDirection;
    }

    private void CheckDirection()
    {
        if (nextMove.x > 0) isLeft = false;
        else isLeft = true;
    }

    public void TakeDamage(int damage)
    {
        if (EnemyStatusData.Hp - damage > 0)
        {
            EnemyStatusData.Hp -= damage;
        }
        else
        {
            //TODO: »ç¸Á ±¸Çö
            SetState(EnemyState.Dead);
        }
    }
}
