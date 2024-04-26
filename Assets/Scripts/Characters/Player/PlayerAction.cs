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

    public PlayerInput playerinput;
    //��ȭ�� ����Ǿ����� üũ
    IInteractive target = null;
    UIManager uiManagerInstance;
    // Update is called once per frame
    private int talkIndex = 1;
    private int talkIndex2 = 1;
    private void Start()
    {
        uiManagerInstance = UIManager.Instance;
    }
    void OnPotalMove()
    {
        if (interactiveList.Count > 0)
        {
            if(target != null)
            {
                if (target.GetType() == InteractType.Potal)
                {
                    target.Interact();
                }
            }
           
        }
    }
    void OnInteractive()
    {
        if (QuestManager.Instance.EndingBossDie == true)
        {
            uiManagerInstance.Bosstalk(talkIndex);
            talkIndex++;
        }
        else
        {

        }
        if (PotalManager.Instance.EnterBossZone == true)
        {
            uiManagerInstance.BeforeBosstalk(talkIndex2);
            talkIndex2++;
        }
        else
        {

        }


        if (interactiveList.Count > 0)
        {
            if (target != null)
            {
                if (target.GetType() == InteractType.NPC)
                {
                    playerinput.OnDisable();
                    target.Interact();//trytalk ��� interact.����������� ����, ��ȣ�ۿ��ϴ� ���� 2���� �ΰ� ó���ϴ� �ְ� �������� ����
                }
                else if (target.GetType() == InteractType.SuperPotal)
                {
                    //��Ż �˾� �޴��� Ȱ��ȭ �Ǿ� �ִٸ�
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
        else
        {
            return;
        }
    }
    void Update()  // �̺κ� ������Ʈ �����ϰ� ��ưŬ���� �۵��ϵ��� ����
    {
        // Q Ű�� ������ �� ����Ʈ â�� ���ų� �ݱ�
        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            uiManagerInstance.TogglePanelQuest();
        }

  
    }

    //IInteractive FindClosestTarget()
    //{
    //    IInteractive closestTarget = null;
    //    float closestDistance = Mathf.Infinity;

    //    foreach (IInteractive ir in interactiveList)
    //    {
    //        float distance = Vector3.Distance(transform.position, ir.GetPosition());
    //        if (distance < closestDistance)
    //        {
    //            closestDistance = distance;
    //            closestTarget = ir;
    //        }
    //    }

    //    return closestTarget;
    //}

    /// Istrigger�� �����ִ� �ݶ��̴��� ��ġ�� ���� npc ������ ������
    private void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other != null)
        {
            IInteractive irv = _other.GetComponent<IInteractive>();

            if (irv != null)
            {
                interactiveList.Add(irv);

                target = irv;
                if (target != null)
                {
                    target.Closer();
                }
            }
        }
    }

    /// �Ʒ� �κ��� ������ NPC�� ���������� ���� �ֱ� ������ NPC�� ��� ��ȭ��
    private void OnTriggerExit2D(Collider2D _other)
    {
        if (_other != null)
        {
            IInteractive irv = _other.GetComponent<IInteractive>();
            if(irv != null)
                irv.CloseUI();

            target = null;

            interactiveList.Remove(irv);
        }

    }
}