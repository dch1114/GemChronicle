using UnityEngine;

public class BossMeleeAttack : MonoBehaviour
{
    [SerializeField] int damageAmount = 20; // 근접 공격 데미지량
    [SerializeField] float attackRange = 1.5f; // 근접 공격 범위
    [SerializeField] float attackCooldown = 2f; // 공격 쿨다운 시간

    bool canAttack = true;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // 공격 가능 상태이고 플레이어와의 거리가 공격 범위 이내일 때 공격
        if (canAttack && IsPlayerInRange())
        {
            Attack();
        }
    }

    private bool IsPlayerInRange()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, attackRange);

        foreach (Collider2D collider in hitColliders)
        {
            if (collider.CompareTag("Player"))
            {
                return true;
            }
        }

        return false;
    }

    private void Attack()
    {

        // 플레이어에게 데미지 적용
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, attackRange);

        foreach (Collider2D collider in hitColliders)
        {
            if (collider.CompareTag("Player"))
            {
                //collider.GetComponent<PlayerHealth>().TakeDamage(damageAmount);
            }
        }

        // 공격 쿨다운 시작
        canAttack = false;
        Invoke("ResetAttackCooldown", attackCooldown);
    }

    private void ResetAttackCooldown()
    {
        canAttack = true;
    }
}
