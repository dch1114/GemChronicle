using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTreeButton : MonoBehaviour
{
    public enum SkillTreeType
    {
        increaseAtk,
        increaseRange,
        increaseAtkSphere
    }

    [SerializeField] private SkillTreeType treeType;

    public bool isUnlocked = false;
    public int price;
    public int amount;
    public Image icon;

    [SerializeField] private SkillTreeButton prevTree;
    [SerializeField] private GameObject lockedImage;

    public void SetSkillBtn()
    {
        lockedImage.SetActive(!isUnlocked);
    }

    public bool CheckCanUnlock()
    {
        if(prevTree != null)
        {
            if (prevTree.isUnlocked)
            {
                return true;
            }
        }

        return false;
    }

    public void SetUnlocked(int index)
    {
        isUnlocked = true;
        lockedImage.SetActive(false);
    }
}
