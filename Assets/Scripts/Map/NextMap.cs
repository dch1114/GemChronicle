using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextMap : MonoBehaviour, IInteractive
{
    public enum NextPositionType
    {
        InitPosition,
        SomePosition,
    };
    public NextPositionType nextPositionType;
    public Transform DestinationPoint;
    public string destination;
    // �浹�� �߻����� ���θ� ��Ÿ���� ����
    private bool collisionOccurred = false;
    UIManager uiManagerInstance;


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
                if (nextPositionType == NextPositionType.InitPosition)
                {
                    player.transform.position = Vector3.zero;
                }
                else if (nextPositionType == NextPositionType.SomePosition)
                {
                    if (DestinationPoint != null)
                    {
                        player.transform.position = DestinationPoint.position;
                    }
                    else
                    {

                    }
                }
                else
                {

                }
            }
            else
            {

            }
        }

    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    InteractType IInteractive.GetType()
    {
        return InteractType.Potal;
    }
}