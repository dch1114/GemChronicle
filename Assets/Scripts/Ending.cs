using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{
    public GameObject creditPanel; // ũ������ ��� �ִ� �г�

    private bool isCreditShowing = false; // ũ������ ���������� ���θ� ��Ÿ���� ����

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Ending")
        {
            StartCoroutine(ShowEndingCredit());
        }
    }

    IEnumerator ShowEndingCredit()
    {
        // ũ������ ���̵��� ����
        creditPanel.SetActive(true);
        isCreditShowing = true;

        // 20�� ���
        yield return new WaitForSeconds(20f);

        // ũ������ ������
        isCreditShowing = false;

        // �ƹ� Ű�� ���� ������ ���
        while (!Input.anyKeyDown)
        {
            yield return null;
        }

        // �ƹ� Ű�� ������ ��ŸƮ ������ �̵�
        LoadStartScene();
    }

    public void LoadStartScene()
    {
        SceneManager.LoadScene("StartScene");
    }
}
