using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WarriorSkillPage : MonoBehaviour
{
    [SerializeField] private GameObject goSkillInfo;
    [SerializeField] private Image skillIcon;
    [SerializeField] private TextMeshProUGUI damageTxt;
    [SerializeField] private Image typeIcon;
    [SerializeField] private TextMeshProUGUI priceTxt;

    [SerializeField] private List<Sprite> typeSprites;
    [SerializeField] private List<Toggle> asdPage;
    [SerializeField] private List<Toggle> comboNumToggle;

    [SerializeField] private List<Sprite> icons;
    [SerializeField] private List<SkillButton> skillBtns;

    [SerializeField] private GameObject buyBtn;

    [SerializeField] Player player;

    private int skillInfoIndex = 0;
    private void Start()
    {
        ShowSkillPage();
    }

    public void ShowSkillPage()
    {
        for(int i = 0; i < skillBtns.Count; i++)
        {
            skillBtns[i].skillInfoData = player.Data.AttackData.GetSkillInfo(i);
            skillBtns[i].icon.sprite = icons[i];
        }

        ShowSkillSettings();
    }

    public void ShowSkillInfo(SkillButton _skill)
    {
        try
        {
            if (!goSkillInfo.activeSelf) goSkillInfo.SetActive(true);

            ClearComboToggles();

            SkillInfoData data = _skill.skillInfoData;

            skillIcon.sprite = _skill.icon.sprite;
            damageTxt.text = data.Damage.ToString();
            ShowSkillType(data);
            priceTxt.text = data.Price.ToString();
            skillInfoIndex = data.SkillStateIndex;

            buyBtn.SetActive(!skillBtns[skillInfoIndex].isUnlocked);
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }

    public void UnlockSkillBtn()
    {
        if(player.Data.StatusData.Gold - skillBtns[skillInfoIndex].skillInfoData.Price >= 0)
        {
            player.Data.StatusData.Gold -= skillBtns[skillInfoIndex].skillInfoData.Price;
            skillBtns[skillInfoIndex].SetUnlocked();
            buyBtn.SetActive(!skillBtns[skillInfoIndex].isUnlocked);

        } else
        {
            Debug.Log("돈이 부족합니다.");
        }
    }

    public void SaveSkillNum()
    {
        int asdIndex = GetASDIndex();
        int comboNumIndex = GetComboNumIndex();

        player.Data.AttackData.AttackSkillStates[asdIndex][comboNumIndex] = skillInfoIndex;
        ShowSkillSettings();
    }

    private void ShowSkillSettings()
    {
        int asdIndex = GetASDIndex();
        List<int> indexs = player.Data.AttackData.AttackSkillStates[asdIndex];
        
        foreach (SkillButton btn in skillBtns)
        {
            if(btn.isUnlocked) btn.cover.SetActive(false);
        }

        skillBtns[indexs[0]].cover.SetActive(true);
        skillBtns[indexs[1]].cover.SetActive(true);
        skillBtns[indexs[2]].cover.SetActive(true);
        skillBtns[indexs[0]].tmp.text = string.Empty;
        skillBtns[indexs[1]].tmp.text = string.Empty;
        skillBtns[indexs[2]].tmp.text = string.Empty;
        skillBtns[indexs[0]].tmp.text += "1";
        skillBtns[indexs[1]].tmp.text += "2";
        skillBtns[indexs[2]].tmp.text += "3";
    }

    private int GetASDIndex()
    {
        for(int i = 0; i < asdPage.Count; i++)
        {
            if (asdPage[i].isOn) return i;
        }

        return 0;
    }

    private int GetComboNumIndex()
    {
        for(int i = 0; i < comboNumToggle.Count; i++)
        {
            if (comboNumToggle[i].isOn) return i;
        }

        return 0;
    }

    private void ShowSkillType(SkillInfoData _skill)
    {
        switch (_skill.SkillType)
        {
            case SkillType.Fire:
                typeIcon.sprite = typeSprites[0];
                break;
            case SkillType.Ice:
                typeIcon.sprite = typeSprites[1];
                break;
            case SkillType.Light:
                typeIcon.sprite = typeSprites[2];
                break;
            default:
                typeIcon.sprite = typeSprites[0];
                break;
        }
    }

    private void ClearComboToggles()
    {
        foreach(Toggle toggle in comboNumToggle)
        {
            toggle.isOn = false;
        }
    }
}
