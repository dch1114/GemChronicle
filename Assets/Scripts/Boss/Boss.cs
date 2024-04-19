using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour, IDamageable
{
    [SerializeField]
    private GameObject projectilePrefab; // �߻�ü ������
    [SerializeField]
    private float bossAppearPoint = 2.5f;
    [SerializeField]
    private int numberOfProjectiles = 10; // �߻��� ������Ÿ�� ��
    [SerializeField]
    private float projectileSpeed = 5.0f; // �߻�ü�� �ӵ�

    //����
    [SerializeField] public EnemyStatusData EnemyStatusData;

    private EnemyState state;

    [SerializeField] private List<GameObject> gems;

    private void Awake()
    {
    }

    private void Start()
    {
        StartCoroutine(Phase01());
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

    private IEnumerator Phase01()
    {
        while (true)
        {
            StartCircleFire(); // ���� ��� ����
            float delay = Random.Range(5f, 10f); // 5�ʿ��� 10�� ������ ���� ����
            yield return new WaitForSeconds(delay); // ���� �߻���� ���
        }
    }

    private void StartCircleFire()
    {
        float angleStep = 360f / numberOfProjectiles;
        float angle = 0f;

        for (int i = 0; i < numberOfProjectiles; i++)
        {
            // ������ ���� �߻�ü�� ������ ���
            float projectileDirX = Mathf.Sin((angle * Mathf.PI) / 180);
            float projectileDirY = Mathf.Cos((angle * Mathf.PI) / 180);

            Vector3 projectileVector = new Vector3(projectileDirX, projectileDirY, 0);
            Vector3 projectileMoveDirection = projectileVector.normalized * projectileSpeed;

            // �߻�ü ���� �� �ӵ� ����
            GameObject tmpProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            tmpProjectile.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileMoveDirection.x, projectileMoveDirection.y);

            angle += angleStep;
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
