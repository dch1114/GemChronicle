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

    // �浹�� �߻����� ���θ� ��Ÿ���� ����

    public void Start()
    {
        uiManagerInstance = UIManager.Instance;
    }



    public void OpenUI()
    {
        uiManagerInstance.potalTxt.text = destination;
        // �浹�� �߻��ϸ� ���¸� true�� ����
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
        GameManager gameManager = FindObjectOfType<GameManager>(); // ���� �Ŵ��� ã��

        if (gameManager != null)
        {
            GameObject player = gameManager.GetPlayer(); // ���� �Ŵ����� ���� �÷��̾� ���
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
