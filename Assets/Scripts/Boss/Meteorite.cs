using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteorite : MonoBehaviour
{
    [SerializeField] int attack;

    void Start()
    {
        StartCoroutine(DestroyBulletAfterDelay(3.0f));
    }

    IEnumerator DestroyBulletAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        PlayerDatas pData = null;
        if (collision.gameObject.tag == "Player")
        {
            pData = collision.gameObject.GetComponent<PlayerDatas>();

            if (pData != null)
            {
                pData.TakeDamage(attack);
                Debug.Log("colli2");
            }
        }
    }
}
