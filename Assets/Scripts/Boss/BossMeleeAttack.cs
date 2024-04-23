using UnityEngine;

public class BossMeleeAttack : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float attackRange = 2f;
    public float attackRate = 1f;
    public int damage = 10;

    private Transform target;
    private float nextAttackTime = 0f;

    public Animator ani;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (target == null)
            return;

        transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        float distanceToPlayer = Vector2.Distance(transform.position, target.position);
        if (distanceToPlayer < attackRange && Time.time >= nextAttackTime)
        {
            Attack();
            nextAttackTime = Time.time + 1f / attackRate;
        }
    }

    void Attack()
    {
        ani.SetTrigger("Attack");
    }
}