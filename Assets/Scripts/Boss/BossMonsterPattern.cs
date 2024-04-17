using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonsterPattern : MonoBehaviour
{
    public Transform player;
    public float speed = 5.0f;
    public int attackDamage = 20;
    public float detectionRange = 10.0f;  // 플레이어 감지 범위
    public float attackRange = 4.0f;      // 공격 범위

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
        // 공격 로직 실행, 예를 들어 체력 감소
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