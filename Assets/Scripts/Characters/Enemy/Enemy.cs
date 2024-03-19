using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    Idle,
    Walking,
    Attacking,
    Dead
}

public class Enemy : MonoBehaviour
{
    [SerializeField] public EnemyStatusData EnemyStatusData;

    protected static readonly int 
}
