using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextMap : MonoBehaviour, IInteractive
{
    public PolygonCollider2D destCol;

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
    
    public Potal potal;

    public void Start()
    {
        uiManagerInstance = UIManager.Instance;
        potal.potalPosition = transform.position;
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
       

        if (GameManager.Instance != null)
        {
            Player player = GameManager.Instance.player; // ���� �Ŵ����� ���� �÷��̾� ���

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

                        GameManager.Instance.cine2d.m_BoundingShape2D = destCol;
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