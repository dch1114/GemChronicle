using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossMonsterPattern2 : MonoBehaviour
{

    public Transform player;
    public int health = 100;
    public float speed = 5.0f;
    public int attackDamage = 20;
    public float detectionRange = 10f;  // 플레이어 감지 범위
    public float attackRange = 4.0f;      // 공격 범위
    public SpriteRenderer BossRender;

    private Transform target;
    private Collider2D collider;
    //private Rigidbody2D rigid;
    private Animator animator;

    private void Awake()
    {
        collider = GetComponent<Collider2D>();
        //rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        Invoke("Think", 2);
        StartCoroutine(FireContinuously());
    }

    void Update()
    {
        DetectAndAttackPlayer();
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            Fire();
            yield return new WaitForSeconds(3f);
        }
    }

    void Fire()
    {
        Vector2 direction = (target.position - transform.position).normalized;
        //rigid.AddForce(direction * 500);

        if (animator != null)
        {
            animator.SetTrigger("Shoot");
        }
    }

    void DetectAndAttackPlayer()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer <= detectionRange)
            {
                if (distanceToPlayer > attackRange)
                {
                    Vector3 direction = (player.position - transform.position).normalized;
                    transform.position += direction * speed * Time.deltaTime;
                }
                else
                {
                    AttackPlayer();
                }
            }
        }
    }

    void AttackPlayer()
    {
        Player playerComponent = player.GetComponent<Player>();
        if (playerComponent != null)
        {
            //playerComponent.TakeDamage(attackDamage);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (player != null)
            {
                //player.TakeDamage(attackDamage);
            }
        }
    }

    public void TakeDamage(int damageAmount)
    {

        health -= damageAmount;
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        BossRender = transform.GetComponentInChildren<SpriteRenderer>();
        speed = 0;
        collider.enabled = false;
        Color color = BossRender.color;
        color.a = 0.3f;
        BossRender.color = color;
        Destroy(gameObject, 1);

        Time.timeScale = 0.1f;
    }
}