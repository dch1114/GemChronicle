using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
    public SkillInfoData skillInfoData;
    public Image icon;
    public GameObject cover;
    public TextMeshProUGUI tmp;
    public bool isUnlocked = false;
    public GameObject unlockedImage;

    public void SetUnlocked()
    {
        isUnlocked = true;
        unlockedImage.SetActive(false);
        cover.SetActive(false);
    }
}
