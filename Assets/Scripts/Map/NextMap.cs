using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextMap : MonoBehaviour
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

    private void OnTriggerEnter2D(Collider2D _collision)
    {
        if (_collision.transform.CompareTag("Player"))
        {
            // �浹�� �߻��ϸ� ���¸� true�� ����
            collisionOccurred = true;
        }
    }

    private void OnTriggerExit2D(Collider2D _collision)
    {
        if (_collision.transform.CompareTag("Player"))
        {
            
            collisionOccurred = false;
        }
    }
    private void Update()
    {
        // �浹�� �߻����� ���� Ű �Է��� �˻�
        if (collisionOccurred)
        {
            UIManager.instance.potalTxt.text = destination;
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {

                PerformAction();


            }
        }
    }
    

    private void PerformAction()
    {
        // �÷��̾� ������Ʈ�� ã��
        GameObject player = GameObject.FindGameObjectWithTag("Player");

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