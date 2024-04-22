using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossMonsterPattern : MonoBehaviour
{

    public Transform player;
    public int health = 100;
    public float speed = 5.0f;
    public int attackDamage = 20;
    public float detectionRange = 10.0f;  // 플레이어 감지 범위
    public float attackRange = 4.0f;      // 공격 범위
    public GameObject AttackPrefab;
    public SpriteRenderer BossRender;

    private Transform target;
    private Collider2D collider;
    private Rigidbody2D rigid;
    private Animator animator;

    private void Awake()
    {
        collider = GetComponent<Collider2D>();
        rigid = GetComponent<Rigidbody2D>();
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
        while (true) // 무한 루프로 코루틴 실행
        {
            Fire();
            yield return new WaitForSeconds(3f); // 다음 발사까지 3초 동안 대기
        }
    }

    void Fire()
    {
        GameObject bullet = Instantiate(AttackPrefab, transform.position, transform.rotation);
        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
        Vector2 direction = (target.position - transform.position).normalized;
        rigid.AddForce(direction * 500);

        // 애니메이션 재생
        if (animator != null)
        {
            animator.SetTrigger("Shoot"); // Shoot 트리거를 설정하여 애니메이션을 재생합니다.
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
                    // 플레이어를 향해 이동
                    Vector3 direction = (player.position - transform.position).normalized;
                    transform.position += direction * speed * Time.deltaTime;
                }
                else
                {
                    // 공격 범위 내에 있으면 공격
                    AttackPlayer();
                }
            }
        }
    }

    void AttackPlayer()
    {
        // 공격 로직 실행,  체력 감소
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

    public void TakeDamage(int damageAmount) // 대미지 받는 함수
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