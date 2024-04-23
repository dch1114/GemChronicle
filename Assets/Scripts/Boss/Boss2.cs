using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2 : MonoBehaviour, IDamageable
{
    [SerializeField]
    private GameObject projectilePrefab; // 발사체 프리팹
    [SerializeField]
    private float bossAppearPoint = 2.5f;
    [SerializeField]
    private int numberOfProjectiles = 10; // 발사할 프로젝타일 수
    [SerializeField]
    private float projectileSpeed = 5.0f; // 발사체의 속도

    //스탯
    [SerializeField] public EnemyStatusData EnemyStatusData;

    [SerializeField] private List<GameObject> gems;

    public Transform player;

    private EnemyState state;

    private void Awake()
    {
    }

    private void Start()
    {
        StartCoroutine(StartSpiralFire());
    }

    void FixedUpdate()
    {
        switch (state)
        {
            case EnemyState.Idle:
                break;
            case EnemyState.Dead:
                OnDie();
                break;
        }
    }

    private IEnumerator StartSpiralFire()
    {
        float circleRadius = 1.0f; // 원의 반지름
        float angle = 0f;
        float angleStep = 360f / numberOfProjectiles;

        while (true)
        {
            for (int i = 0; i < numberOfProjectiles; i++)
            {
                // 각도에 따라 발사체의 방향을 계산
                float projectileDirX = Mathf.Sin((angle * Mathf.PI) / 180);
                float projectileDirY = Mathf.Cos((angle * Mathf.PI) / 180);

                Vector3 projectileVector = new Vector3(projectileDirX, projectileDirY, 0);
                Vector3 projectileMoveDirection = projectileVector.normalized * projectileSpeed;

                // 발사체 생성 및 속도 설정
                GameObject tmpProjectile = Instantiate(projectilePrefab, transform.position + projectileVector.normalized * circleRadius, Quaternion.identity);
                tmpProjectile.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileMoveDirection.x, projectileMoveDirection.y);

                angle += angleStep;
                yield return new WaitForSeconds(0.1f); // 발사 간격
            }
        }
    }

    protected void SetState(EnemyState newState)
    {
        state = newState;

        switch (newState)
        {
            case EnemyState.Idle:
                break;
            case EnemyState.Dead:
                //animator.SetTrigger(EnemyAnimationData.DieParameterHash);
                break;
        }
    }

    public void OnDie()
    {
        EnemyStatusData.Hp = 0;
        SetState(EnemyState.Dead);
        SpawnGems();
        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        if (EnemyStatusData.Hp - damage > 0)
        {
            EnemyStatusData.Hp -= damage;
        }
        else
        {
            SetState(EnemyState.Dead);
        }
    }

    private void SpawnGems()
    {
        int amount = RandomAmount();

        for (int i = 0; i < amount; i++)
        {
            SpawnTypeGem();
        }
    }

    private int RandomAmount()
    {
        int amount = Random.Range(10, 20);

        return amount;
    }

    private void SpawnTypeGem()
    {
        Instantiate(gems[3]).transform.position = gameObject.transform.position; ;
    }
}
