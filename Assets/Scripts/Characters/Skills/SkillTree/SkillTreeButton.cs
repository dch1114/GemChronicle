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
    public int amount;
    public int price;
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
        LevelUpSkill(index);
    }

    public SkillTreeType GetSkillType()
    {
        return treeType;
    }

    private void LevelUpSkill(int index)
    {
        switch(treeType)
        {
            case SkillTreeType.increaseAtk:
                GameManager.Instance.player.Data.AttackData.SkillInfoDatas[index].Damage += amount;
                break;
            case SkillTreeType.increaseRange:
                GameManager.Instance.player.Data.AttackData.SkillInfoDatas[index].Range += amount;
                break;
        }
    }
}
