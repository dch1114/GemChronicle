using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WarriorSkillPage : SkillPages
{
    [SerializeField] private Image typeIcon;
    [SerializeField] private TextMeshProUGUI priceTxt;

    [SerializeField] private List<Toggle> comboNumToggle;

    protected override void ShowSkillSettings()
    {
        int asdIndex = GetASDIndex();
        List<int> indexs = player.Data.AttackData.AttackSkillStates[asdIndex];

        goSkillInfo.SetActive(false);

        foreach (SkillButton btn in skillBtns)
        {
            btn.SetSkillBtn();
        }

        ClearSelected();

        skillBtns[indexs[0]].cover.SetActive(true);
        skillBtns[indexs[0]].selected.SetActive(true);
        skillBtns[indexs[1]].cover.SetActive(true);
        skillBtns[indexs[1]].selected.SetActive(true);
        skillBtns[indexs[2]].cover.SetActive(true);
        skillBtns[indexs[2]].selected.SetActive(true);
        skillBtns[indexs[0]].tmp.text += "1";
        skillBtns[indexs[1]].tmp.text += "2";
        skillBtns[indexs[2]].tmp.text += "3";
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
            skillInfoIndex = data.ID;

            buyBtn.SetActive(!skillBtns[skillInfoIndex].skillInfoData.IsUnlocked);
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }

    public override void UnlockSkillBtn()
    {
        int price = skillBtns[skillInfoIndex].skillInfoData.Price;

        if (skillBtns[skillInfoIndex].CheckCanUnlock() && player.Data.StatusData.IsGoldEnough(price))
        {
            player.Data.StatusData.UseGold(price);
            skillBtns[skillInfoIndex].SetUnlocked();
            buyBtn.SetActive(false);
        } else
        {
            Debug.Log("돈이 부족합니다.");
        }
    }

    public void SaveSkillNum()
    {
        int asdIndex = GetASDIndex();
        int comboNumIndex = GetComboNumIndex();

        if(comboNumIndex < 3)
        {
            player.Data.AttackData.AttackSkillStates[asdIndex][comboNumIndex] = skillInfoIndex;
            ShowSkillSettings();
        }
    }


    private int GetComboNumIndex()
    {
        for(int i = 0; i < comboNumToggle.Count; i++)
        {
            if (comboNumToggle[i].isOn) return i;
        }

        return 4;
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

    private void ClearSelected()
    {
        foreach(SkillButton btn in skillBtns)
        {
            btn.selected.SetActive(false);
        }
    }
}
