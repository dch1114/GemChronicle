using Cinemachine;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public CinemachineConfiner2D cine2d;

    public Player player;
    public int saveDataID;

    //background test
    public GameObject[] backgrounds;

    public void SwitchBackground(int _mapIndex)
    {
        foreach (var background in backgrounds)
        {
            background.SetActive(false);
        }

        backgrounds[_mapIndex].SetActive(true);
    }
}
