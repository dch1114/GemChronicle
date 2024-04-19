using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossMonsterPattern : MonoBehaviour
{

    public Transform player;
    public int health = 5000;
    public float speed = 5.0f;
    public int attackDamage = 20;
    public float detectionRange = 10.0f;  // �÷��̾� ���� ����
    public float attackRange = 4.0f;      // ���� ����
    public GameObject AttackPrefab;
    public SpriteRenderer BossRender;

    private Transform target;
    private Collider2D collider;
    private Rigidbody2D rigid;

    private void Awake()
    {
        collider = GetComponent<Collider2D>();
        rigid = GetComponent<Rigidbody2D>();
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
        while (true) // ���� ������ �ڷ�ƾ ����
        {
            Fire();
            yield return new WaitForSeconds(3f); // ���� �߻���� 3�� ���� ���
        }
    }

    void Fire()
    {
        GameObject bullet = Instantiate(AttackPrefab, transform.position, transform.rotation);
        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
        Vector2 direction = (target.position - transform.position).normalized;
        rigid.AddForce(direction * 500);
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
                    // �÷��̾ ���� �̵�
                    Vector3 direction = (player.position - transform.position).normalized;
                    transform.position += direction * speed * Time.deltaTime;
                }
                else
                {
                    // ���� ���� ���� ������ ����
                    AttackPlayer();
                }
            }
        }
    }

    void AttackPlayer()
    {
        // ���� ���� ����,  ü�� ����
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

    public void TakeDamage(int damageAmount) // ����� �޴� �Լ�
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