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
    //��ȭ�� ����Ǿ����� üũ
    IInteractive target = null;
    public UIManager uiManagerInstance;
    // Update is called once per frame

    private void Start()
    {
        uiManagerInstance = UIManager.instance;
    }
    void OnPotalMove()
    {
        NextMap nextMap = target as NextMap;
        if (nextMap != null)
        {
            if(interactiveList.Count > 0)
            {
                target.Interact();
            }
            
        }

    }
    void OnInteractive()
    {
        NPCController npcController = target as NPCController;
        if (npcController != null)
        {
            if (interactiveList.Count > 0)
            {

                playerinput.OnDisable();
                target.Interact(); //trytalk ��� interact . ����������� ����, ��ȣ�ۿ��ϴ� ���� 2���� �ΰ� ó���ϴ� �ְ� �������� ����
            }
        }

    }
    void Update()  // �̺κ� ������Ʈ �����ϰ� ��ưŬ���� �۵��ϵ��� ����
    {
        // Q Ű�� ������ �� ����Ʈ â�� ���ų� �ݱ�
        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            if (isQuestPanelOpen)
            {
                uiManagerInstance.OpenClosePanelInspectorQuests(); //ĳ�� �ϱ� ������ �����س��� ����
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

    /// Istrigger�� �����ִ� �ݶ��̴��� ��ġ�� ���� npc ������ ������
    private void OnTriggerEnter2D(Collider2D _other)
    {

        if (_other != null)
        {
            //�������̽��� ������Ʈ Ÿ���� �ƴϱ⶧���� �������̽��� ������ NPCController Ŭ������ ������Ʈ�� �����;� ��
            IInteractive t = _other.gameObject.GetComponent<NPCController>();
            IInteractive y = _other.gameObject.GetComponent<NextMap>();
            IInteractive x = _other.gameObject.GetComponent<EveryMap>();
            if (t != null)
            {
                interactiveList.Add(t);
                // �÷��̾�� ���� ����� ���͸� ã�� �޼ҵ��Դϴ�.
                target = FindClosestTarget();
                t.Closer(); //���� UI�� �ϴ°��� �ƴ϶�. Closer�� �Ѵ�. �ϰ� ���⼭ ���� �����ٶ�°��� �ν�.
            }
            if (y != null)
            {
                interactiveList.Add(y);
                y.Closer();
                target = FindClosestTarget();

            }
        }

    }

    /// �Ʒ� �κ��� ������ NPC�� ���������� ���� �ֱ� ������ NPC�� ��� ��ȭ��
    private void OnTriggerExit2D(Collider2D _other)
    {
        if (_other != null)
        {
            //�������̽��� ������Ʈ Ÿ���� �ƴϱ⶧���� �������̽��� ������ NPCController Ŭ������ ������Ʈ�� �����;� ��
            IInteractive t = _other.gameObject.GetComponent<NPCController>();
            IInteractive y = _other.gameObject.GetComponent<NextMap>();
            IInteractive x = _other.gameObject.GetComponent<EveryMap>();
            if (t != null)
            {
                interactiveList.Remove(t);

                if (interactiveList.Count <= 0)
                {
                    t.CloseUI();
                    target = null;
                }
            }
            else
            {
                interactiveList.Remove(y);
                y.CloseUI();
            }
        }


    }
}