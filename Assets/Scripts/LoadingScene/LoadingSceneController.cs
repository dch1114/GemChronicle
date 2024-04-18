using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneController : MonoBehaviour
{
    static string nextScene;

    [SerializeField] Image progressBar;

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

        float timer = 0f;
        while (!asyncOperation.isDone)
        {
            yield return null;

            if(asyncOperation.progress < 0.7f)
            {
                progressBar.fillAmount = asyncOperation.progress;
            }
            else // 페이크 로딩
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
}
