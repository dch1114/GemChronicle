using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MeteoriteSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject alertLinePrefab;
    [SerializeField]
    private GameObject meteoritePrefab;
    [SerializeField]
    private float minSpawnTime = 1.0f;
    [SerializeField]
    private float maxSpawnTime = 4.0f;
    [SerializeField] private Transform bossPosition;
    private float limitMinX = -10.0f;
    private float limitMaxX = 10.0f;
    private float spawnHeightY = 10.0f;

    private bool bossAlive = true;
    private Boss2 boss2;

    private void Awake()
    {
        boss2 = FindObjectOfType<Boss2>();
        StartCoroutine(SpawnMeteorite());
    }

    private IEnumerator SpawnMeteorite()
    {
        while (bossAlive)
        {
            float positionX = Random.Range(limitMinX, limitMaxX);

            GameObject alertLineClone = Instantiate(alertLinePrefab, bossPosition.position + new Vector3(positionX, 0, 0), Quaternion.identity);

            yield return new WaitForSeconds(1.0f);

            Destroy(alertLineClone);

            Vector3 meteoritePosition = bossPosition.position + new Vector3(positionX, spawnHeightY, 0);
            Instantiate(meteoritePrefab, meteoritePosition, Quaternion.identity);

            float spawnTime = Random.Range(minSpawnTime, maxSpawnTime);

            yield return new WaitForSeconds(spawnTime);
        }
    }

    public void BossDied()
    {
        bossAlive = false;
    }
}
