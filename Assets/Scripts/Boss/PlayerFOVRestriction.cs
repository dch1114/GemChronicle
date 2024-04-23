using UnityEngine;
using System.Collections;

public class MonsterFOVRestriction : MonoBehaviour
{
    public Renderer[] objectsToHide; // 시야 제한에서 숨길 렌더러 배열
    public float restrictionDuration = 3.0f; // 제한 지속 시간
    public float transitionDuration = 1.0f; // 전환 지속 시간
    public float restrictionInterval = 10.0f; // 시야 제한 주기

    private bool isRestricting = false; // 시야 제한 중 여부
    private Transform player; // 플레이어 위치

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // 플레이어 찾기
        StartCoroutine(PeriodicFOVRestriction()); // 주기적 시야 제한 시작
    }

    IEnumerator PeriodicFOVRestriction()
    {
        while (true)
        {
            yield return new WaitForSeconds(restrictionInterval); // 주기적으로 제한을 시작하기 위해 대기
            StartRestriction(); // 제한 시작
        }
    }

    void StartRestriction()
    {
        if (!isRestricting)
        {
            isRestricting = true;
            StartCoroutine(RestrictFOV()); // 시야 제한 코루틴 시작
        }
    }

    IEnumerator RestrictFOV()
    {
        // 몬스터가 플레이어를 향하도록 회전
        transform.LookAt(player);

        // 시야 제한 시작 시 숨겨야 할 렌더러를 비활성화
        foreach (Renderer renderer in objectsToHide)
        {
            renderer.enabled = false;
        }

        yield return new WaitForSeconds(restrictionDuration); // 제한 지속 시간 대기

        // 시야 제한 종료 시 숨겨야 할 렌더러를 다시 활성화
        foreach (Renderer renderer in objectsToHide)
        {
            renderer.enabled = true;
        }

        isRestricting = false;
    }
}
