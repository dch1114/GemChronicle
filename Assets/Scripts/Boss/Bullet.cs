using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    void Start()
    {
        // 3�� �ڿ� �߻�ü�� �����ϴ� �ڷ�ƾ ����
        StartCoroutine(DestroyBulletAfterDelay(3.0f));
    }

    IEnumerator DestroyBulletAfterDelay(float delay)
    {
        // delay�� ��ŭ ���
        yield return new WaitForSeconds(delay);

        // ��� �� �߻�ü ����
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
