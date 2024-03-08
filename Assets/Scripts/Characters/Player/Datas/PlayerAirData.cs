using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerAirData
{
    [field: Header("JumpData")]
    [field: SerializeField] public float JumpForce { get; private set; } = 4f;

}