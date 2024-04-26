using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterRespawnManager : Singleton<MonsterRespawnManager>
{
    [SerializeField] List<GameObject> monsters = new List<GameObject>();

    public event EventHandler respawnEvent;
    //respawnEvent?.Invoke(); ·Î ½ÇÇà
}
