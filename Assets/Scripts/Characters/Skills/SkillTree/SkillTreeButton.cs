using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTreeButton : MonoBehaviour
{
    public enum SkillType
    {
        DecreaseCool,
        increaseAtk,
        increaseAtkSphere
    }

    [SerializeField] private SkillType skillType;
    public bool isUnlocked = false;
    [SerializeField] private int amount;
    [SerializeField] private int price;
    [SerializeField] private GameObject prevTree;
    [SerializeField] private GameObject lockedImage;
}
