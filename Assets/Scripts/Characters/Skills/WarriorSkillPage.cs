using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
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

    //public void ShowSkillInfo(Skill _skill)
    //{
    //    try
    //    {
    //        if (!goSkillInfo.activeSelf) goSkillInfo.SetActive(true);

    //        skillIcon.sprite = _skill.icon.sprite;
    //        damageTxt.text = _skill.damage.ToString();

    //        switch (_skill.type)
    //        {
    //            case SkillType.Fire:
    //                typeIcon.sprite = typeSprites[0];
    //                break;
    //            case SkillType.Ice:
    //                typeIcon.sprite = typeSprites[1];
    //                break;
    //            case SkillType.Light:
    //                typeIcon.sprite = typeSprites[2];
    //                break;
    //            default:
    //                typeIcon.sprite = typeSprites[0];
    //                break;
    //        }
    //        priceTxt.text = _skill.price.ToString();
    //    } catch(Exception e)
    //    {
    //        Debug.Log(e);
    //    }
    //}

    public void ShowSkillPage()
    {
        
    }
}
