using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MagicianSkillPage : SkillPages
{
    [SerializeField] private GameObject goSkillInfo;
    [SerializeField] private Image skillIcon;
    [SerializeField] private TextMeshProUGUI damageTxt;

    [SerializeField] private List<TypeIcon> typeIcons;

    [SerializeField] private List<Sprite> typeSprites;

    [SerializeField] private List<Sprite> icons;
    [SerializeField] private List<SkillButton> skillBtns;

    [SerializeField] private GameObject buyBtn;

    private int skillInfoIndex = 0;
    public override void SetSkillBtns()
    {
        for (int i = 0; i < skillBtns.Count; i++)
        {
            skillBtns[i].skillInfoData = player.Data.AttackData.GetSkillInfo(i);
            skillBtns[i].icon.sprite = icons[i];
        }

        ShowSkillSettings();
    }

    private void ShowSkillSettings()
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
        try
        {
            if (!goSkillInfo.activeSelf) goSkillInfo.SetActive(true);

            SkillInfoData data = _skill.skillInfoData;

            skillIcon.sprite = _skill.icon.sprite;
            damageTxt.text = data.Damage.ToString();
            ShowSkillType(data);
            skillInfoIndex = data.SkillStateIndex;

            buyBtn.SetActive(!skillBtns[skillInfoIndex].skillInfoData.IsUnlocked);
        }
        catch (Exception e)
        {
            Debug.Log(e);
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
