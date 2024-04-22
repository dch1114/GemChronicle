using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteorite : MonoBehaviour
{
    void Start()
    {
        // 3초 뒤에 발사체를 제거하는 코루틴 시작
        StartCoroutine(DestroyBulletAfterDelay(3.0f));
    }

    IEnumerator DestroyBulletAfterDelay(float delay)
    {
        // delay초 만큼 대기
        yield return new WaitForSeconds(delay);

        // 대기 후 발사체 제거
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
