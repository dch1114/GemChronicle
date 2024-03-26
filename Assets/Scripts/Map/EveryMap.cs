using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static NextMap;

public class EveryMap : MonoBehaviour, IInteractive
{
    List<Transform> maps;
    private bool collisionOccurred = false;
    public string destination;
    public Transform DestinationPoint;
    UIManager uiManagerInstance;
    public void Awake()
    {
        
        maps.Add(DestinationPoint);


    }

    // 충돌이 발생한지 여부를 나타내는 변수

    public void Start()
    {
        uiManagerInstance = UIManager.Instance;
    }



    public void OpenUI()
    {
        uiManagerInstance.potalTxt.text = destination;
        // 충돌이 발생하면 상태를 true로 변경
        collisionOccurred = true;
        uiManagerInstance.PotalTalk(true);
    }

    public void CloseUI()
    {
        collisionOccurred = false;
        uiManagerInstance.PotalTalk(false);
    }

    public void TryTalk()
    {
        throw new System.NotImplementedException();
    }

    public void Closer()
    {
        OpenUI();
    }

    public void Interact()
    {
        GameManager gameManager = FindObjectOfType<GameManager>(); // 게임 매니저 찾기

        if (gameManager != null)
        {
            GameObject player = gameManager.GetPlayer(); // 게임 매니저를 통해 플레이어 얻기
            if (player != null)
            {
                if (DestinationPoint != null)
                {
                    player.transform.position = DestinationPoint.position;
                }
            }
        }

    }
    public Vector3 GetPosition()
    {
        return transform.position;
    }

    InteractType IInteractive.GetType()
    {
        throw new System.NotImplementedException();
    }
}
