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

    [SerializeField] private List<GameObject> gems;

    public Transform player;
    public bool isFlipped = false;

    private EnemyState state;

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

    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x < player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
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
