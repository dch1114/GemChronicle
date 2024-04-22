using System.Collections;
using System.IO;
using UnityEngine;

public class StartManager : MonoBehaviour
{
    [SerializeField] private GameObject startText;
    [SerializeField] private GameObject loadErrorMsg;
    [SerializeField] private RectTransform gameTitle;
    [SerializeField] private GameObject gameStart;
    [SerializeField] private GameObject characterChooseGO;

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

    public void StartGame()
    {
        LoadingSceneController.LoadScene("KYW_TestMain");
    }

    public void ContinueGameBtn()
    {
        string path = Path.Combine(Application.dataPath, "playerData.json");

        if (File.Exists(path))
        {
            GameManager.Instance.isNew = false;
            StartGame();
        }
        else
        {
            Debug.Log("저장 파일 없음");
        }
    }

    public void NewGameBtn()
    {
        characterChooseGO.SetActive(true);
        gameStart.SetActive(true);
        gameTitle.gameObject.SetActive(false);
    }

    public void TurnBackBtn()
    {
        gameTitle.gameObject.SetActive(true);
        gameStart.SetActive(true);
        characterChooseGO.SetActive(false);
    }

    public void CreateCharacter()
    {
        StartGame();
    }
}
