using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
    public SkillInfoData skillInfoData {  get; set; }

    public List<SkillButton> prevTrees;

    public Image icon;
    public GameObject cover;
    public TextMeshProUGUI tmp;
    public GameObject lockedImage;
    public GameObject selected;

    public void SetSkillBtn()
    {
        if(skillInfoData != null)
        {
            bool isUnlocked = skillInfoData.IsUnlocked;
            lockedImage.SetActive(!isUnlocked);
            cover.SetActive(!isUnlocked);
            tmp.text = string.Empty;
        }
    }

    public bool CheckCanUnlock()
    {
        if(GameManager.Instance.player.Data.StatusData.JobType != JobType.Warrior)
        {
            foreach (SkillButton btn in prevTrees)
            {
                SkillInfoData data = btn.skillInfoData;

                if (!data.IsUnlocked)
                    return false;
            }
        }

        return true;
    }

    public void SetUnlocked()
    {
        skillInfoData.IsUnlocked = true;
        lockedImage.SetActive(false);
        cover.SetActive(false);
    }
}
