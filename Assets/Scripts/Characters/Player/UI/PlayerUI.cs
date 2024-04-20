using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [Header("Stat Bar")]
    [SerializeField] Image hpBar;
    [SerializeField] Image expBar;

    [Header("Slider")]
    [SerializeField] Slider BGMSlider;
    [SerializeField] Slider SFXSlider;

    [Header("Text")]
    [SerializeField] TextMeshProUGUI hpTxt;
    [SerializeField] TextMeshProUGUI expTxt;
    [SerializeField] TextMeshProUGUI levelTxt;
    [SerializeField] TextMeshProUGUI goldTxt;
    [SerializeField] TextMeshProUGUI alertTxt;

    [Header("Panel")]
    [SerializeField] GameObject soundSetting;
    [SerializeField] GameObject alertPanel;

    PlayerStatusData playerData;

    private void Start()
    {
        SetPlayerData();
        UpdateStatus();
    }

    private void SetPlayerData()
    {
        playerData = GameManager.Instance.player.Data.StatusData;
    }

    public void UpdateStatus()
    {
        UpdateHp();
        UpdateExp();
        UpdateLevel();
        UpdateGold();
    }

    public void UpdateHp()
    {
        if (playerData != null)
        {
            if(playerData.MaxHp > 0)
            {
                hpBar.fillAmount = (float) playerData.Hp / (float) playerData.MaxHp;
                Debug.Log(playerData.Hp / playerData.MaxHp);
                hpTxt.text = playerData.Hp + " / " + playerData.MaxHp;
            }
        }
        else
        {
            SetPlayerData();
            UpdateHp();
        }
    }

    public void UpdateExp()
    {
        if (playerData != null)
        {
            if(playerData.RequiredExp > 0)
            {
                expBar.fillAmount = playerData.Exp / playerData.RequiredExp;
                Debug.Log(expBar.fillAmount);
                expTxt.text = playerData.Exp + " / " + playerData.RequiredExp;
            }
        }
        else
        {
            SetPlayerData();
            UpdateExp();
        }
    }

    public void UpdateLevel()
    {
        if (playerData != null)
        {
            levelTxt.text = playerData.Level.ToString();
        }
        else
        {
            SetPlayerData();
            UpdateLevel();
        }
    }

    public void UpdateGold()
    {
        goldTxt.text = playerData.Gold.ToString();
    }

    public void OnOffSoundSetting()
    {
        bool current = soundSetting.activeSelf;

        soundSetting.SetActive(!current);
    }

    public void UpdateBGMVolume()
    {
        if(SoundManager.instance != null)
            SoundManager.instance.SetMusicVolume(BGMSlider);
    }

    public void UpdateSFXVolume()
    {
        if (SoundManager.instance != null)
            SoundManager.instance.SetMusicVolume(SFXSlider);
    }

    public void SaveBtn()
    {
        if(PlayerDataManager.Instance != null)
            PlayerDataManager.Instance.SavePlayerDataToJson();
    }

    public void ExitBtn()
    {
        if(PlayerDataManager.Instance != null)
        {
            if(PlayerDataManager.Instance.IsCurrentDataSaved())
            {
                //����Ǿ������� ������
                alertTxt.text = "������ �����Ͻðڽ��ϱ�?";
            } else
            {
                alertTxt.text = "������ �����Ͻðڽ��ϱ�?\n(�� �������� �ʾҽ��ϴ�.)";
            }
            alertPanel.SetActive(true);
        }
    }

    public void GameExit()
    {
        Application.Quit();
    }
}
