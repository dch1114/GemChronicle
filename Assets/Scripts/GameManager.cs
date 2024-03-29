using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    //public static GameManager instance;

    public Player player;
    public GameObject Player;
    //protected override void Awake()
    //{
    //    instance = this;
    //}

    public GameObject GetPlayer()
    {
        return Player;
    }
}
