using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public DataManager dataManager;
    public GameObject player;
    private void Awake()
    {
        instance = this;
        dataManager = GetComponentInChildren<DataManager>();
    }

    public GameObject GetPlayer()
    {
        return player;
    }
}
