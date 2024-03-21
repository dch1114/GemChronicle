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
    public void Awake()
    {
        
        maps.Add(DestinationPoint);


    }
    
    // 충돌이 발생한지 여부를 나타내는 변수
 




    public void OpenUI()
    {
        UIManager.instance.potalTxt.text = destination;
        // 충돌이 발생하면 상태를 true로 변경
        collisionOccurred = true;
        UIManager.instance.PotalTalk(true);
    }

    public void CloseUI()
    {
        collisionOccurred = false;
        UIManager.instance.PotalTalk(false);
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
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            if (DestinationPoint != null)
            {
                player.transform.position = DestinationPoint.position;
            }
        }
    }
    public Vector3 GetPosition()
    {
        return transform.position;
    }
}
