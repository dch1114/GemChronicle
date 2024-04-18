using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meteor : MonoBehaviour
{
    [SerializeField]
    private GameObject poop;

    void Start()
    {
        StartCoroutine(CreatepoopRoutine());
    }

    void Update()
    {

    }

    IEnumerator CreatepoopRoutine()
    {
        while (true)
        {
            CreatePoop();
            yield return new WaitForSeconds(1);
        }
    }

    private void CreatePoop()
    {
        Vector3 pos = new Vector3(0, 6, 0);
        Instantiate(poop, pos, Quaternion.identity);
    }
}
