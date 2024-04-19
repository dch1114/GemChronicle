using System.Collections;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField]
    private GameObject projectilePrefab; // �߻�ü ������
    [SerializeField]
    private float bossAppearPoint = 2.5f;
    [SerializeField]
    private int numberOfProjectiles = 10; // �߻��� ������Ÿ�� ��
    [SerializeField]
    private float projectileSpeed = 5.0f; // �߻�ü�� �ӵ�

    private void Awake()
    {
    }

    private void Start()
    {
        StartCoroutine(Phase01());
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

    public void OnDie()
    {
        // ���� �ı� ��ƼŬ ����
        //GameObject clone = Instantiate(transform.position, Quaternion.identity);
        // ���� ������Ʈ ����
        Destroy(gameObject);
    }
}
