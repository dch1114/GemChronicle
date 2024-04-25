using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{
    public GameObject creditPanel; // 크레딧을 담고 있는 패널

    private bool isCreditShowing = false; // 크레딧이 보여지는지 여부를 나타내는 변수

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Ending")
        {
            StartCoroutine(ShowEndingCredit());
        }
    }

    IEnumerator ShowEndingCredit()
    {
        // 크레딧을 보이도록 설정
        creditPanel.SetActive(true);
        isCreditShowing = true;

        // 20초 대기
        yield return new WaitForSeconds(20f);

        // 크레딧이 끝나면
        isCreditShowing = false;

        // 아무 키나 누를 때까지 대기
        while (!Input.anyKeyDown)
        {
            yield return null;
        }

        // 아무 키나 누르면 스타트 씬으로 이동
        LoadStartScene();
    }

    public void LoadStartScene()
    {
        SceneManager.LoadScene("StartScene");
    }
}
