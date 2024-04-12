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

//TODO: ø¢ºø µ•¿Ã≈Õ∑Œ ¿‘∑¬ « ø‰
[Serializable]
public class SkillInfoData
{
    [field: SerializeField] public int SkillStateIndex { get; private set; }
    [field: SerializeField] public string SkillName { get; private set; }
    [field: SerializeField] public int Damage { get; set; }
    [field: SerializeField] public int Range { get; set; }
    [field: SerializeField] public SkillType SkillType {get; private set;}
    [field:SerializeField] public int Price { get; private set; }
    [field: SerializeField] public bool IsUnlocked { get; set; }
}

[Serializable]
public class PlayerAttackData
{
    [field:SerializeField] public List<SkillInfoData> SkillInfoDatas { get; private set; }
    public int GetSkillInfoCount() {  return SkillInfoDatas.Count; }
    public SkillInfoData GetSkillInfo(int index) {  return SkillInfoDatas[index]; }
    [field: SerializeField] public List<List<int>> AttackSkillStates = new List<List<int>>() { new List<int>() { 0, 0, 0 }, new List<int>() { 0, 0, 0 }, new List<int>() { 0, 0, 0 } };

    [field: SerializeField] public ObjectPool skillPool;
}
