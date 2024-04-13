using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillType
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
    [field: SerializeField] public SkillType SkillType;
    [field: SerializeField] public int Price;
    [field: SerializeField] public bool IsUnlocked;
}

[Serializable]
public class PlayerAttackData
{
    [field:SerializeField] public List<SkillInfoData> SkillInfoDatas { get; set; }
    public int GetSkillInfoCount() {  return SkillInfoDatas.Count; }
    public SkillInfoData GetSkillInfo(int index) {  return SkillInfoDatas[index]; }
    [field: SerializeField] public List<List<int>> AttackSkillStates = new List<List<int>>() { new List<int>() { 0, 0, 0 }, new List<int>() { 0, 0, 0 }, new List<int>() { 0, 0, 0 } };

    [field: SerializeField] public ObjectPool skillPool;
}
