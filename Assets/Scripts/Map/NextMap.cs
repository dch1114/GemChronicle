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
    // 충돌이 발생한지 여부를 나타내는 변수
    private bool collisionOccurred = false;

    private void OnTriggerEnter2D(Collider2D _collision)
    {
        if (_collision.transform.CompareTag("Player"))
        {
            // 충돌이 발생하면 상태를 true로 변경
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
        // 충돌이 발생했을 때만 키 입력을 검사
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
        // 플레이어 오브젝트를 찾음
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