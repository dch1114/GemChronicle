using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public Animator anim;

    private void OnEnable()
    {
        transform.position = GameManager.instance.player.transform.position;
        StartCoroutine(WaitForAnimationEnd());
    }

    IEnumerator WaitForAnimationEnd()
    {
        do
        {
            yield return null;
        } while (anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1);

        gameObject.SetActive(false);
    }
}
