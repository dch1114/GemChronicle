using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class SkillPages : MonoBehaviour
{
    public Player player;

    [SerializeField] protected GameObject goSkillInfo;
    [SerializeField] protected Image skillIcon;
    [SerializeField] protected TextMeshProUGUI damageTxt;

    [SerializeField] protected List<Toggle> asdPage;
    [SerializeField] protected List<Sprite> typeSprites;

    [SerializeField] protected List<Sprite> icons;
    [SerializeField] protected List<SkillButton> skillBtns;

    [SerializeField] protected GameObject buyBtn;

    protected int skillInfoIndex = 0;

    public void Start()
    {
        player = GameManager.Instance.player;

        SetSkillBtns();
    }

    private void OnEnable()
    {
        UIManager.Instance.skillPages.ActiveGems();
    }

    private void OnDisable()
    {
        UIManager.Instance.skillPages.OffGems();
    }


    public void SetSkillBtns()
    {
        for (int i = 0; i < skillBtns.Count; i++)
        {
            skillBtns[i].skillInfoData = player.Data.AttackData.GetSkillInfo(i);
            skillBtns[i].icon.sprite = icons[i];
        }

        ShowSkillSettings();
    }

    protected abstract void ShowSkillSettings();

    public abstract void UnlockSkillBtn();
    public int GetASDIndex()
    {
        for (int i = 0; i < asdPage.Count; i++)
        {
            if (asdPage[i].isOn) return i;
        }

        return 0;
    }
}
