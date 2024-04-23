using Cinemachine;
using System.Collections;
using System.IO;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public CinemachineConfiner2D cine2d;


    public Player player;

    public bool isNew = true;
    public string playerName { get; set; } = "±‚∫ª¿Ã";
    public JobType playerJob { get; set; } = JobType.Warrior;
    public bool isLastBossDead = false;
    public int saveDataID;

    //background test
    public GameObject[] backgrounds;
    //inventory save test
    public Inventory inventory;

    protected override void Awake()
    {
        base.Awake();

        UnityEngine.Rendering.DebugManager.instance.enableRuntimeUI = false;
        if(Instance != null && Instance != this)
        {
            Destroy(Instance);
        } else
        {
            DontDestroyOnLoad(Instance);
        }
    }

    public void SwitchBackground(int _mapIndex)
    {
        foreach (var background in backgrounds)
        {
            background.SetActive(false);
        }

        backgrounds[_mapIndex].SetActive(true);
    }
}
