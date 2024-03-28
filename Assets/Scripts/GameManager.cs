using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public static GameManager instance;

    public Player player;

    private void Awake()
    {
        instance = this;
    }
}
