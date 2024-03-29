using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
    public SkillInfoData skillInfoData {  get; set; }

    public Image icon;
    public GameObject cover;
    public TextMeshProUGUI tmp;
    public GameObject lockedImage;

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

    public void SetUnlocked()
    {
        skillInfoData.IsUnlocked = true;
        lockedImage.SetActive(false);
        cover.SetActive(false);
    }
}
