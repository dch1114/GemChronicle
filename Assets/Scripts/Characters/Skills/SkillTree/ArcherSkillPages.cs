using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ArcherSkillPages : SkillPages
{
    [SerializeField] private Image typeIcon;
    [SerializeField] private TextMeshProUGUI rangeTxt;

    protected override void ShowSkillSettings()
    {
        int asdIndex = GetASDIndex();
        int index = player.Data.AttackData.AttackSkillStates[asdIndex][0];

        skillInfoIndex = index;

        goSkillInfo.SetActive(false);

        foreach (SkillButton btn in skillBtns)
        {
            btn.SetSkillBtn();
        }
    }

    public override void UnlockSkillBtn()
    {
        int price = skillBtns[skillInfoIndex].skillInfoData.Price;
        //젬으로 교체 필요
        if (skillBtns[skillInfoIndex].CheckCanUnlock() && player.Data.StatusData.IsGoldEnough(price))
        {
            player.Data.StatusData.UseGold(price);
            skillBtns[skillInfoIndex].SetUnlocked();
            buyBtn.SetActive(false);
        }
        else
        {
            Debug.Log("젬이 부족합니다.");
        }
    }

    public void SaveSkillNum()
    {
        int asdIndex = GetASDIndex();

        player.Data.AttackData.AttackSkillStates[asdIndex][0] = skillInfoIndex;

        ShowSkillSettings();
    }

    public void ShowSkillInfo(SkillButton _skill)
    {
        SkillInfoData data = _skill.skillInfoData;

        int settingInfoIndex = GetSettingSkillState();
        SkillInfoData settingData = player.Data.AttackData.GetSkillInfo(settingInfoIndex);

        if(data != null)
        {
            skillIcon.sprite = _skill.icon.sprite;
            string damageInfo = data.Damage.ToString();
            if (data.Damage != settingData.Damage) damageInfo += "<color=green>( +" + settingData.Damage.ToString() + " )</color>";
            string rangeInfo = data.Range.ToString();
            if (data.Range != settingData.Range) rangeInfo += "<color=green>( +" + settingData.Range.ToString() + " )</color>";
            damageTxt.text = damageInfo;
            rangeTxt.text = rangeInfo;
            ShowSkillType(data);

            skillInfoIndex = data.SkillStateIndex;

            buyBtn.SetActive(!skillBtns[skillInfoIndex].skillInfoData.IsUnlocked);
        }
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

    private int GetSettingSkillState()
    {
        int asdIndex = GetASDIndex();

        return player.Data.AttackData.AttackSkillStates[asdIndex][0];
    }
}
