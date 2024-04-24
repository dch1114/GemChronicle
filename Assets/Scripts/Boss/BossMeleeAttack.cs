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
        if (QuestManager.Instance.bossAction == true)
        {
            if (target == null)
                return;

            Vector3 directionToPlayer = target.position - transform.position;
            float directionX = Mathf.Sign(directionToPlayer.x);

            transform.position += Vector3.right * directionX * moveSpeed * Time.deltaTime;

            if (directionX > 0)
                transform.localScale = new Vector3(-1, 1, 1);
            else
                transform.localScale = new Vector3(1, 1, 1);

            float distanceToPlayer = Vector2.Distance(transform.position, target.position);
            if (distanceToPlayer < attackRange && Time.time >= nextAttackTime)
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
       
    }

    void Attack()
    {
        ani.SetTrigger("Attack");

        Boss2 boss = GetComponent<Boss2>();
        if (boss != null)
        {
            boss.TakeDamage(damage);
            Debug.Log("dam");
        }
    }
}