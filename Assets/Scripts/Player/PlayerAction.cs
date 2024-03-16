using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Pool;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class PlayerAction : MonoBehaviour
{

    public GameManager gameManager;
    private PlayerInputActions inputActions;
    public PlayerInput playerinput;
    //대화가 종료되었는지 체크
   
    //0315 interactive 리스트 선언
    List<IInteractive> interactiveList = new List<IInteractive>();

    IInteractive target = null;

    
    private void OnInteractive()
    {
       if( interactiveList.Count > 0)
        {
            playerinput.OnDisable();
            target.TryTalk();
        }
    }

    
    //void Update()
    //{

    //    if (Keyboard.current.tabKey.wasPressedThisFrame&& interactiveList.Count > 0)
    
    //    {
      
    //        playerinput.OnDisable();
            
    //        target.TryTalk();

    //    }

    //}
    // 플레이어와 가장 가까운 몬스터를 찾는 메소드입니다.
    IInteractive FindClosestTarget()
    {
        IInteractive closestTarget = null;
        float closestDistance = Mathf.Infinity;

        foreach (IInteractive ir in interactiveList)
        {
            float distance = Vector3.Distance(transform.position, ir.GetPosition());
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestTarget = ir;
            }
        }

        return closestTarget;
    }

    // Istrigger가 켜져있는 콜라이더가 겹치는 곳의 npc 정보를 가져옴 <summary>
    
    private void OnTriggerEnter2D(Collider2D _other)
    {

        if (_other != null)
        {
            //인터페이스는 컴포넌트 타입이 아니기때문에 인터페이스가 구현된 NPCController 클래스를 컴포넌트로 가져와야 함
            IInteractive t = _other.gameObject.GetComponent<NPCController>();

            if (t != null)
            {
                interactiveList.Add(t);
                // 플레이어와 가장 가까운 몬스터를 찾는 메소드입니다.
                target = FindClosestTarget();
                target.OpenUI();
            }
        }

        Debug.Log(interactiveList.Count);
   
        
    }


    /// 아래 부분이 없으면 NPC와 떨어지더라도 가장 최근 접촉한 NPC와 계속 대화함
    private void OnTriggerExit2D(Collider2D _other)
    {
        if (_other != null)
        {
            //인터페이스는 컴포넌트 타입이 아니기때문에 인터페이스가 구현된 NPCController 클래스를 컴포넌트로 가져와야 함
            IInteractive t = _other.gameObject.GetComponent<NPCController>();

            if (t != null)
            {
                interactiveList.Remove(t);

                if (interactiveList.Count <= 0)
                {
                    t.CloseUI();
                    target = null;
                }

            }
        }
        Debug.Log(interactiveList.Count);

    }

}


