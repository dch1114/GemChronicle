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
    
    // �浹�� �߻����� ���θ� ��Ÿ���� ����
 




    public void OpenUI()
    {
        UIManager.instance.potalTxt.text = destination;
        // �浹�� �߻��ϸ� ���¸� true�� ����
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
