using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public CinemachineConfiner2D cine2d;

    public Player player;
    public int saveDataID;
}
