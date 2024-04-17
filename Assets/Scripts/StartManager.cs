using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    [SerializeField] private GameObject startText;
    [SerializeField] private RectTransform gameTitle;
    [SerializeField] private GameObject gameStart;
    [SerializeField] private GameObject loadErrorMsg;


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
        while (Vector3.Distance(gameTitle.anchoredPosition3D, titleTargetPosition) > 0.01f)
        {
            gameTitle.anchoredPosition3D = Vector3.MoveTowards(gameTitle.anchoredPosition3D, titleTargetPosition, titleMoveSpeed * Time.deltaTime);
            yield return null;
        }

        gameStarted = true;

        if (startText != null)
        {
            startText.SetActive(false);
        }

        if (gameStart != null)
        {
            gameStart.SetActive(true);
        }
    }

    public void StartNewGame()
    {

    }

    public void ContinueGame()
    {

    }
}
