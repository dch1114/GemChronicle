using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MagicianSkillPage : SkillPages
{
    [SerializeField] private List<TypeIcon> typeIcons;

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

        ClearSelected();

        skillBtns[index].selected.SetActive(true);
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

        if(data != null)
        {
            if (!goSkillInfo.activeSelf) goSkillInfo.SetActive(true);

            skillIcon.sprite = _skill.icon.sprite;
            damageTxt.text = data.Damage.ToString();
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
                typeIcons[0].SetTypeIcon(typeSprites[0], _skill.Price);
                typeIcons[1].SetTypeIcon(typeSprites[1], 0);
                typeIcons[2].SetTypeIcon(typeSprites[2], 0);
                break;
            case SkillType.Ice:
                typeIcons[0].SetTypeIcon(typeSprites[0], 0);
                typeIcons[1].SetTypeIcon(typeSprites[1], _skill.Price);
                typeIcons[2].SetTypeIcon(typeSprites[2], 0);
                break;
            case SkillType.Light:
                typeIcons[0].SetTypeIcon(typeSprites[0], 0);
                typeIcons[1].SetTypeIcon(typeSprites[1], 0);
                typeIcons[2].SetTypeIcon(typeSprites[2], _skill.Price);
                break;
            case SkillType.IceFire:
                typeIcons[0].SetTypeIcon(typeSprites[0], _skill.Price);
                typeIcons[1].SetTypeIcon(typeSprites[1], _skill.Price);
                typeIcons[2].SetTypeIcon(typeSprites[2], 0);
                break;
            case SkillType.FireLight:
                typeIcons[0].SetTypeIcon(typeSprites[0], 0);
                typeIcons[1].SetTypeIcon(typeSprites[1], _skill.Price);
                typeIcons[2].SetTypeIcon(typeSprites[2], _skill.Price);
                break;
            default:
                break;
        }
    }

    private void ClearSelected()
    {
        foreach (SkillButton btn in skillBtns)
        {
            btn.selected.SetActive(false);
        }
    }
}
