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
    // 충돌이 발생한지 여부를 나타내는 변수
    private bool collisionOccurred = false;

    private void OnTriggerEnter2D(Collider2D _collision)
    {
        if (_collision.transform.CompareTag("Player"))
        {
            OpenUI();
        }
    }

    private void OnTriggerExit2D(Collider2D _collision)
    {
        if (_collision.transform.CompareTag("Player"))
        {
            CloseUI();


        }
    }
 

 

    public void OpenUI()
    {
        UIManager.instance.potalTxt.text = destination;
        // 충돌이 발생하면 상태를 true로 변경
        collisionOccurred = true;
    }

    public void CloseUI()
    {
        collisionOccurred = false;
    }

    public void TryTalk()
    {
        throw new System.NotImplementedException();
    }

    public void Closer()
    {
        throw new System.NotImplementedException();
    }

    public void Interact()
    {
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

    public Vector3 GetPosition()
    {
        return transform.position;
    }
}