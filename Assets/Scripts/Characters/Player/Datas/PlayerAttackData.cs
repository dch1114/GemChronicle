using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackSchoolType
{
    Ice,
    Fire,
    Light
}

//TODO: ø¢ºø µ•¿Ã≈Õ∑Œ ¿‘∑¬ « ø‰
[Serializable]
public class SkillInfoData
{
    [field: SerializeField] public int AttackStateIndex { get; private set; }
    [field: SerializeField] public string AttackName { get; private set; }
    [field: SerializeField] public int Damage { get; private set; }
    [field: SerializeField] public AttackSchoolType SchoolType {get; private set;}
    [field:SerializeField] public int Price { get; private set; }
}

[Serializable]
public class PlayerAttackData
{
    [field:SerializeField] public List<SkillInfoData> SkillInfoDatas { get; private set; }
    public int GetSkillInfoCount() {  return SkillInfoDatas.Count; }
    public SkillInfoData Get(int index) {  return SkillInfoDatas[index]; }
}
