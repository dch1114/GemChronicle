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
    //��ȭ�� ����Ǿ����� üũ
   
    //0315 interactive ����Ʈ ����
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
    // �÷��̾�� ���� ����� ���͸� ã�� �޼ҵ��Դϴ�.
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

    // Istrigger�� �����ִ� �ݶ��̴��� ��ġ�� ���� npc ������ ������ <summary>
    
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


