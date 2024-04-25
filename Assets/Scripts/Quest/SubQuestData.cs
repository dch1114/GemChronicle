using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class SubQuestData : ScriptableObject
{
    public string questName;
    public int monsterTargetID;
    public int targetKillCount;
    public int exp;
    public int gold;
    public SkillType skillType;
    public int skillRewardAmount;
}
