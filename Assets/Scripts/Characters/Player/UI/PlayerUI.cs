using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [Header("Slider")]
    [SerializeField] Slider hpSlider;
    [SerializeField] Slider expSlider;

    [Header("Text")]
    [SerializeField] TextMeshProUGUI hpTxt;
    [SerializeField] TextMeshProUGUI expTxt;
    [SerializeField] TextMeshProUGUI levelTxt;
    [SerializeField] TextMeshProUGUI goldTxt;

    [SerializeField] GameObject soundSetting;

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
                hpSlider.value = playerData.Hp / playerData.MaxHp;
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
                expSlider.value = playerData.Exp / playerData.RequiredExp;
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
}
