using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ArcherSkillPages : SkillPages
{
    [SerializeField] private Image typeIcon;
    [SerializeField] private TextMeshProUGUI rangeTxt;
    [SerializeField] private TextMeshProUGUI priceTxt;
    [SerializeField] private GameObject descriptionTxt;

    [SerializeField] private List<SkillTreeButton> skillTreeBtns;

    private SkillTreeButton currentTreeBtn;

    private void OnEnable()
    {
        UIManager.Instance.skillPages.ActiveGems();
        InitializeSkillInfo();
    }

    protected override void ShowSkillSettings()
    {
        int asdIndex = GetASDIndex();
        skillInfoIndex = asdIndex;

        foreach (SkillTreeButton btn in skillTreeBtns)
        {
            btn.SetSkillBtn();
        }

        InitializeSkillInfo();
    }

    public override void UnlockSkillBtn()
    {
        if(currentTreeBtn != null)
        {
            int price = currentTreeBtn.price;

            if(currentTreeBtn.CheckCanUnlock() && player.Data.StatusData.UseGems((ElementType) GetASDIndex(), price))
            {
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

    public void InitializeSkillInfo()
    {
        player = GameManager.Instance.player;
        int asdIndex = GetASDIndex();
        int index = player.Data.AttackData.AttackSkillStates[asdIndex][0];

        skillIcon.sprite = icons[asdIndex];

        SkillInfoData data = player.Data.AttackData.GetSkillInfo(index);

        if (descriptionTxt.activeSelf) descriptionTxt.SetActive(false);

        damageTxt.text = data.Damage.ToString();
        rangeTxt.text = data.Range.ToString();

        ShowSkillType();
    }

    public void ShowSkillInfo(SkillTreeButton _skill)
    {
        currentTreeBtn = _skill;

        InitializeSkillInfo();

        if(!_skill.isUnlocked)
        {
            //switch (_skill.GetSkillType())
            //{
            //    case SkillTreeButton.SkillTreeType.increaseAtk:
            //        damageTxt.text += "<color=green> ( + " + _skill.amount + " )</color>";
            //        break;
            //    case SkillTreeButton.SkillTreeType.increaseRange:
            //        rangeTxt.text += "<color=green> ( + " + _skill.amount + " )</color>";
            //        break;
            //    case SkillTreeButton.SkillTreeType.increaseAtkSphere:
            //        descriptionTxt.SetActive(true);
            //        break;
            //}
        }

        buyBtn.SetActive(!_skill.isUnlocked);
        priceTxt.text = _skill.price > 0 ? _skill.price.ToString() : string.Empty;
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

    private int CalculateIndexNum()
    {
        int index = 0;
        if (skillTreeBtns[1].isUnlocked)
        {
            if (skillTreeBtns[3].isUnlocked)
            {
                if (skillTreeBtns[2].isUnlocked && skillTreeBtns[4].isUnlocked)
                {
                    index = 8;
                } else
                {
                    if (skillTreeBtns[2].isUnlocked) index = 3;
                    else if (skillTreeBtns[4].isUnlocked) index = 7;
                    else index = 6;
                }
            } else
            {
                if (skillTreeBtns[2].isUnlocked) index = 2;
                else index = 1;
            }
        } else
        {
            if (skillTreeBtns[3].isUnlocked)
            {
                if (skillTreeBtns[4].isUnlocked)
                {
                    if (skillTreeBtns[5].isUnlocked)
                    {
                        //TODO : 스킬 3개로 나가기
                    }
                    index = 5;
                } else
                {
                    index = 4;
                }
            }
        }

        return index;
    }
}
