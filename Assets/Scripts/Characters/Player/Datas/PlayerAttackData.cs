using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AttackInfoData
{
    [field: SerializeField] public string AttackName {  get; private set; }
    [field: SerializeField] public int AttackStateIndex { get; private set; }
    [field: SerializeField] public int Damage { get; private set; }
}

[Serializable]
public class PlayerAttackData
{
    [field:SerializeField] public List<AttackInfoData> AttackInfoDatas { get; private set; }
    public int GetAttackInfoCount() {  return AttackInfoDatas.Count; }
    public AttackInfoData GetAttackInfo(int index) {  return AttackInfoDatas[index]; }
}
