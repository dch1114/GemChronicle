using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Pool;
using UnityEngine.UI;

public class PlayerAction : MonoBehaviour
{


   
    GameObject scanObject;
    public GameObject talkBtn;
    List<IInteractive> interactiveList = new List<IInteractive>();
    private PlayerInputActions inputActions;
    bool isQuestPanelOpen = false;

    public PlayerInput playerinput;
    //대화가 종료되었는지 체크
    IInteractive target = null;

    // Update is called once per frame

    public void OnInteractive()
    {
        if (interactiveList.Count > 0)
        {
            playerinput.OnDisable();
            target.TryTalk();
        }
    }
    void Update()  // 이부분 업데이트 제외하고 버튼클릭시 작동하도록 변경
    {
        // Q 키를 눌렀을 때 퀘스트 창을 열거나 닫기
        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            if (isQuestPanelOpen)
            {
                UIManager.instance.OpenClosePanelInspectorQuests();
                isQuestPanelOpen = false;
            }
            else
            {
                UIManager.instance.OpenClosePanelInspectorQuests();
                isQuestPanelOpen = true;
            }
        }


    }

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

    /// Istrigger가 켜져있는 콜라이더가 겹치는 곳의 npc 정보를 가져옴
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


