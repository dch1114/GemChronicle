using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ArcherSkillPages : SkillPages
{
    [SerializeField] private Image typeIcon;
    [SerializeField] private TextMeshProUGUI rangeTxt;
    [SerializeField] private GameObject descriptionTxt;

    [SerializeField] private List<SkillTreeButton> skillTreeBtns;

    private SkillTreeButton currentTreeBtn;

    protected override void ShowSkillSettings()
    {
        int asdIndex = GetASDIndex();
        skillInfoIndex = asdIndex;

        foreach (SkillTreeButton btn in skillTreeBtns)
        {
            btn.SetSkillBtn();
        }
    }

    public override void UnlockSkillBtn()
    {
        if(currentTreeBtn != null)
        {
            int price = currentTreeBtn.price;

            if(currentTreeBtn.CheckCanUnlock() && player.Data.StatusData.IsGoldEnough(price))
            {
                player.Data.StatusData.UseGold(price);
                currentTreeBtn.SetUnlocked(GetASDIndex());
                buyBtn.SetActive(false);
                ShowSkillInfo(currentTreeBtn);
            }
        }
    }

    public void SaveSkillNum()
    {
        int asdIndex = GetASDIndex();

        player.Data.AttackData.AttackSkillStates[asdIndex][0] = skillInfoIndex;

        ShowSkillSettings();
    }

    public void ShowSkillInfo(SkillTreeButton _skill)
    {
        currentTreeBtn = _skill;

        int asdIndex = GetASDIndex();
        int index = player.Data.AttackData.AttackSkillStates[asdIndex][0];

        SkillInfoData data = player.Data.AttackData.GetSkillInfo(index);

        if (descriptionTxt.activeSelf) descriptionTxt.SetActive(false);

        string damageStr = data.Damage.ToString();
        string rangeStr = data.Range.ToString();
        ShowSkillType();

        if(!_skill.isUnlocked)
        {
            switch (_skill.GetSkillType())
            {
                case SkillTreeButton.SkillTreeType.increaseAtk:
                    damageStr += "<color=green> ( + " + _skill.amount + " )</color>";
                    break;
                case SkillTreeButton.SkillTreeType.increaseRange:
                    rangeStr += "<color=green> ( + " + _skill.amount + " )</color>";
                    break;
                case SkillTreeButton.SkillTreeType.increaseAtkSphere:
                    descriptionTxt.SetActive(true);
                    break;
            }
        }

        buyBtn.SetActive(!_skill.isUnlocked);
        damageTxt.text = damageStr;
        rangeTxt.text = rangeStr;
    }

    private void ShowSkillType()
    {
        switch (GetASDIndex())
        {
            case 0:
                typeIcon.sprite = typeSprites[0];
                break;
            case 1:
                typeIcon.sprite = typeSprites[1];
                break;
            case 2:
                typeIcon.sprite = typeSprites[2];
                break;
            default:
                typeIcon.sprite = typeSprites[0];
                break;
        }
    }
}
