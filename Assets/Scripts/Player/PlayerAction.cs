using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Pool;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

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
    UIManager uiManagerInstance;
    // Update is called once per frame

    private void Start()
    {
        uiManagerInstance = UIManager.Instance;
    }
    void OnPotalMove()
    {
        if (interactiveList.Count > 0)
        {
            if (target.GetType() == InteractType.Potal)
            {
                target.Interact();
            }
        }
    }
    void OnInteractive()
    {
        if (interactiveList.Count > 0)
        {
            if (target.GetType() == InteractType.NPC)
            {
                playerinput.OnDisable();
                target.Interact();//trytalk 대신 interact.가까워졌을때 시점, 상호작용하는 시점 2개를 두고 처리하는 애가 무엇인지 생각
            }
            else if (target.GetType() == InteractType.SuperPotal)
            {
                //포탈 팝업 메뉴가 활성화 되어 있다면
                if (UIManager.Instance.IsOpenPotalPopup())
                {
                    UIManager.Instance.potalUIScript.ExecuteSelectedPotalMenuAction();
                    return;
                }

                //playerinput.OnDisable();
                target.Interact();
            }
        }
    }
    void Update()  // 이부분 업데이트 제외하고 버튼클릭시 작동하도록 변경
    {
        // Q 키를 눌렀을 때 퀘스트 창을 열거나 닫기
        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            if (isQuestPanelOpen)
            {
                uiManagerInstance.OpenClosePanelInspectorQuests(); //캐싱 하기 변수에 저장해놓고 쓰기
                isQuestPanelOpen = false;
            }
            else
            {
                uiManagerInstance.OpenClosePanelInspectorQuests();
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
            IInteractive irv = _other.GetComponent<IInteractive>();

            interactiveList.Add(irv);
            
            target = FindClosestTarget();

            target.Closer();

        }

    }

    /// 아래 부분이 없으면 NPC와 떨어지더라도 가장 최근 접촉한 NPC와 계속 대화함
    private void OnTriggerExit2D(Collider2D _other)
    {
        if (_other != null)
        {
            IInteractive irv = _other.GetComponent<IInteractive>();

            irv.CloseUI();

            target = null;

            interactiveList.Remove(irv);
        }

    }
}