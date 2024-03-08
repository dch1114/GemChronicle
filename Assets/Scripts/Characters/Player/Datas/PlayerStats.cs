using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [field: SerializeField] public PlayerGroundData GroundedData { get; private set; }
    [field: SerializeField] public PlayerAirData AirData { get; private set; }
}
