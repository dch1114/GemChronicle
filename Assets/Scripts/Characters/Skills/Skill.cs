using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SkillType
{
    Ice,
    Fire,
    Light
}

public class Skill : MonoBehaviour
{
    public int index;
    public SkillType type;
    public int damage;
    public int price;
    public Image icon;
}
