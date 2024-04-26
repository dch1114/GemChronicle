using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ElementType
{
    Ice,
    Fire,
    Light,
    Dark,
    IceFire,
    FireLight
}

[Serializable]
public class SkillInfoData
{
    [field: SerializeField] public int ID;
    [field: SerializeField] public string SkillName;
    [field: SerializeField] public int Damage;
    [field: SerializeField] public int Range;
    [field: SerializeField] public ElementType ElementType;
    [field: SerializeField] public int Price;
    [field: SerializeField] public bool IsUnlocked;
}

[Serializable]
public class PlayerAttackData
{
    [field:SerializeField] public List<SkillInfoData> SkillInfoDatas { get; set; }
    public int GetSkillInfoCount() {  return SkillInfoDatas.Count; }
    public SkillInfoData GetSkillInfo(int index) {  return SkillInfoDatas[index]; }
    [field: SerializeField] public List<List<int>> AttackSkillStates;

    [field: SerializeField] public ObjectPool skillPool;
}
