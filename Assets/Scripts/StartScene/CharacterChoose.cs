using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterChoose : MonoBehaviour
{
    [SerializeField] GameObject firstName;
    [SerializeField] GameObject secondJob;

    [SerializeField] TMP_InputField nameTMP;
    [SerializeField] Toggle[] toggles;

    private string tempName;

    private void OnEnable()
    {
        FirstPhase();
    }

    public void FirstPhase()
    {
        firstName.SetActive(true);
        secondJob.SetActive(false);
    }

    public void SecondPhase()
    {
        tempName = nameTMP.text;

        firstName.SetActive(false);
        secondJob.SetActive(true);
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

    public void StartNewGameBtn()
    {
        GameManager.Instance.isNew = true;
        GameManager.Instance.playerName = tempName;
        GameManager.Instance.playerJob = (JobType) GetJobType();

        StartGame();
    }


    private int GetJobType()
    {
        for(int i = 0; i < toggles.Length; i++)
        {
            if (toggles[i].isOn) return i;
        }

        return 0;
    }

    public void TurnBackSecond()
    {
        FirstPhase();
    }
}
