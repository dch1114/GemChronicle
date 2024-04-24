using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public enum EnemyState
{
    Idle,
    Walking,
    Attacking,
    Dead
}

public enum EnemyType
{
    Boss,
    Field
}

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] public EnemyStatusData EnemyStatusData;
    [SerializeField] public EnemyAnimationData EnemyAnimationData;

    private Rigidbody2D rigid;
    private Animator animator;
    private EnemyState state;
    [SerializeField] private EnemyType enemyType;
    [SerializeField] private SkillType skillType;

    [SerializeField] private GameObject gem;

    private bool isLeft = true;
    private bool foundEnemy = false;
    private bool canAttack = true;
    private Vector3 nextMove = Vector3.left;
    private Vector3 distance = new Vector3(1.5f, 0, 0);
    private Vector3 leftDirection;
    private Vector3 rightDirection;

    [SerializeField] private ObjectPool skillPool;

    private void Awake()
    {
        EnemyAnimationData.Initialize();

        //rigid = GetComponent<Rigidbody2D>();
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
            SpawnSkill();
            canAttack = false;
            StartCoroutine(WaitAttackCoolTime(EnemyStatusData.AttackRate));
        } else
        {
            SetState(EnemyState.Idle);
        }
    }

    private void SpawnSkill()
    {
        GameObject go = skillPool.SpawnFromPool("0");
        GameObject go2 = skillPool.SpawnFromPool("1");
        go.transform.position = gameObject.transform.position;
        if (go2 != null)
        {
            go2.transform.position = gameObject.transform.position;
            go2.SetActive(true);
        }
        go.SetActive(true);

    }

    private void OnDie()
    {
        QuestManager.Instance.NotifyQuest(Constants.QuestType.KillBoss, EnemyStatusData.MonsterId, 1);
        EnemyStatusData.Hp = 0;
        SetState(EnemyState.Dead);
        SpawnGems();
        gameObject.SetActive(false);
        GameManager.Instance.player.Data.StatusData.GetExp(EnemyStatusData.Exp);
       
    }

    private void SpawnGems()
    {
        int amount = RandomAmount();

        for(int i = 0; i < amount; i++)
        {
            Instantiate(gem).transform.position = gameObject.transform.position;
        }
    }

    private int RandomAmount()
    {
        int amount = 0;
        switch (enemyType)
        {
            case EnemyType.Boss:
                amount = Random.Range(10, 20);
                break;
            case EnemyType.Field:
                amount = Random.Range(1, 5);
                break;
        }

        return amount;
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
    IEnumerator WaitAttackCoolTime(float _coolTime)
    {
        yield return new WaitForSeconds(_coolTime);
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
        SoundManager.Instance.PlayAttackClip();
        float realDamage = damage * 1.2f - EnemyStatusData.Def * 0.2f;
        if (EnemyStatusData.Hp - realDamage > 0)
        {
            EnemyStatusData.Hp -= (int) realDamage;
        }
        else
        {
            //TODO: »ç¸Á ±¸Çö
            OnDie();
        }
    }
}
