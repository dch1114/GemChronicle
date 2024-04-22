using UnityEngine;

public class BossMeleeAttack : MonoBehaviour
{
    [SerializeField] int damageAmount = 20; // ���� ���� ��������
    [SerializeField] float attackRange = 1.5f; // ���� ���� ����
    [SerializeField] float attackCooldown = 2f; // ���� ��ٿ� �ð�

    bool canAttack = true;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // ���� ���� �����̰� �÷��̾���� �Ÿ��� ���� ���� �̳��� �� ����
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

        // �÷��̾�� ������ ����
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, attackRange);

        foreach (Collider2D collider in hitColliders)
        {
            if (collider.CompareTag("Player"))
            {
                //collider.GetComponent<PlayerHealth>().TakeDamage(damageAmount);
            }
        }

        // ���� ��ٿ� ����
        canAttack = false;
        Invoke("ResetAttackCooldown", attackCooldown);
    }

    private void ResetAttackCooldown()
    {
        canAttack = true;
    }
}
