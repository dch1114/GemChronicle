using System.Collections;
using UnityEngine;

public class StartManager : MonoBehaviour
{
    [SerializeField] private GameObject startText;
    [SerializeField] private GameObject loadErrorMsg;
    public RectTransform gameTitle;
    public GameObject gameStart;
    public GameObject CharacterChoosPrefab;


    private bool gameStarted = false;
    private Vector3 titleTargetPosition;
    private float titleMoveSpeed = 250.0f;

    private void Start()
    {
        titleTargetPosition = gameTitle.anchoredPosition3D + new Vector3(0f, 270f, 0f);
    }

    private void Update()
    {
        if (!gameStarted && Input.anyKeyDown)
        {
            StartCoroutine(PressAnyButton());
        }
    }

    private IEnumerator PressAnyButton()
    {
        if (startText != null)
        {
            startText.SetActive(false);
        }

        while (Vector3.Distance(gameTitle.anchoredPosition3D, titleTargetPosition) > 0.01f)
        {
            gameTitle.anchoredPosition3D = Vector3.MoveTowards(gameTitle.anchoredPosition3D, titleTargetPosition, titleMoveSpeed * Time.deltaTime);
            yield return null;
        }

        if (gameStart != null)
        {
            gameStart.SetActive(true);
        }

        gameStarted = true;
    }

    public void StartNewGame()
    {
        LoadingSceneController.LoadScene("KSH");
    }

    public void ContinueGame()
    {

    }

    //public IEnumerator LoadScene()
    //{
    //    yield return null;

    //    AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("KSH");

    //    Debug.Log("Pro : " + asyncOperation.progress);

    //    while (asyncOperation.isDone)
    //    {
    //        progressText.text = "Loading " + (asyncOperation.progress * 100) + "%";

    //        yield return null;
    //    }
    //}
}
