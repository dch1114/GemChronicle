using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDatas : MonoBehaviour, IDamageable
{
    [field: SerializeField] public PlayerGroundData GroundedData { get; private set; }
    [field: SerializeField] public PlayerAirData AirData { get; private set; }
    [field: SerializeField] public PlayerAttackData AttackData { get; private set; }
    [field: SerializeField] public PlayerStatusData StatusData { get; private set; }

    public void TakeDamage(int damage)
    {
        StatusData.TakeDamage(damage);
    }
}
