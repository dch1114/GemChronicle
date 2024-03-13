using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public DataManager dataManager;

    private void Awake()
    {
        instance = this;
        dataManager = GetComponentInChildren<DataManager>();
    }
}
