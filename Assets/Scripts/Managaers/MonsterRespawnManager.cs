using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterRespawnManager : Singleton<MonsterRespawnManager>
{
    public event Action RespawnMonsters;
    
    public void OnRespawn()
    {
        RespawnMonsters?.Invoke();
    }
}
