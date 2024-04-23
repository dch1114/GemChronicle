using UnityEngine;
using System.Collections;

public class MonsterFOVRestriction : MonoBehaviour
{
    public Renderer[] objectsToHide; // �þ� ���ѿ��� ���� ������ �迭
    public float restrictionDuration = 3.0f; // ���� ���� �ð�
    public float transitionDuration = 1.0f; // ��ȯ ���� �ð�
    public float restrictionInterval = 10.0f; // �þ� ���� �ֱ�

    private bool isRestricting = false; // �þ� ���� �� ����
    private Transform player; // �÷��̾� ��ġ

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // �÷��̾� ã��
        StartCoroutine(PeriodicFOVRestriction()); // �ֱ��� �þ� ���� ����
    }

    IEnumerator PeriodicFOVRestriction()
    {
        while (true)
        {
            yield return new WaitForSeconds(restrictionInterval); // �ֱ������� ������ �����ϱ� ���� ���
            StartRestriction(); // ���� ����
        }
    }

    void StartRestriction()
    {
        if (!isRestricting)
        {
            isRestricting = true;
            StartCoroutine(RestrictFOV()); // �þ� ���� �ڷ�ƾ ����
        }
    }

    IEnumerator RestrictFOV()
    {
        // ���Ͱ� �÷��̾ ���ϵ��� ȸ��
        transform.LookAt(player);

        // �þ� ���� ���� �� ���ܾ� �� �������� ��Ȱ��ȭ
        foreach (Renderer renderer in objectsToHide)
        {
            renderer.enabled = false;
        }

        yield return new WaitForSeconds(restrictionDuration); // ���� ���� �ð� ���

        // �þ� ���� ���� �� ���ܾ� �� �������� �ٽ� Ȱ��ȭ
        foreach (Renderer renderer in objectsToHide)
        {
            renderer.enabled = true;
        }

        isRestricting = false;
    }
}
