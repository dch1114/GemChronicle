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
    //��ȭ�� ����Ǿ����� üũ
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
    void Update()  // �̺κ� ������Ʈ �����ϰ� ��ưŬ���� �۵��ϵ��� ����
    {
        // Q Ű�� ������ �� ����Ʈ â�� ���ų� �ݱ�
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

    /// Istrigger�� �����ִ� �ݶ��̴��� ��ġ�� ���� npc ������ ������
    private void OnTriggerEnter2D(Collider2D _other)
    {

        if (_other != null)
        {
            //�������̽��� ������Ʈ Ÿ���� �ƴϱ⶧���� �������̽��� ������ NPCController Ŭ������ ������Ʈ�� �����;� ��
            IInteractive t = _other.gameObject.GetComponent<NPCController>();

            if (t != null)
            {
                interactiveList.Add(t);
                // �÷��̾�� ���� ����� ���͸� ã�� �޼ҵ��Դϴ�.
                target = FindClosestTarget();
                target.OpenUI();
            }
        }

        Debug.Log(interactiveList.Count);

    }


    /// �Ʒ� �κ��� ������ NPC�� ���������� ���� �ֱ� ������ NPC�� ��� ��ȭ��
    private void OnTriggerExit2D(Collider2D _other)
    {
        if (_other != null)
        {
            //�������̽��� ������Ʈ Ÿ���� �ƴϱ⶧���� �������̽��� ������ NPCController Ŭ������ ������Ʈ�� �����;� ��
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


