using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneController : MonoBehaviour
{
    static string nextScene;

    [SerializeField] Image progressBar;
    [SerializeField] TextMeshProUGUI tipText;

    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadSceneProcess());
    }

    IEnumerator LoadSceneProcess()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(nextScene);
        asyncOperation.allowSceneActivation = false;
        ShowRandomTip();

        float timer = 0f;
        while (!asyncOperation.isDone)
        {
            yield return null;

            if(asyncOperation.progress < 0.7f)
            {
                progressBar.fillAmount = asyncOperation.progress;
            }
            else // ����ũ �ε�
            {
                timer += Time.unscaledDeltaTime;
                progressBar.fillAmount = Mathf.Lerp(0.7f, 1f, timer);
                if (progressBar.fillAmount >= 1f)
                {
                    asyncOperation.allowSceneActivation = true;
                    yield break;
                }
            }
        }

    }

    private void ShowRandomTip()
    {
        // ���⼭ ���� �� �����͸� �����ͼ� �����ϰ� �ϳ��� �����Ͽ� ǥ���մϴ�.
        string[] tips = {
            "Tip 1: ���� �� 1��°.",
            "Tip 2: ���� �� 2��°.",
            "Tip 3: ���� �� 3��°.",
            // Add more tips as needed
        };

        int randomIndex = Random.Range(0, tips.Length);
        tipText.text = tips[randomIndex];
    }
}
