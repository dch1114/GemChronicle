using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonsterPattern : MonoBehaviour
{
    public Transform player;
    public float speed = 5.0f;
    public int attackDamage = 20;
    public float detectionRange = 10.0f;  // �÷��̾� ���� ����
    public float attackRange = 4.0f;      // ���� ����

    private void Update()
    {
        DetectAndAttackPlayer();
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
        // ���� ���� ����, ���� ��� ü�� ����
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
}