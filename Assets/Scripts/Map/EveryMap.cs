using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static NextMap;




public class EveryMap : MonoBehaviour, IInteractive
{
    List<Transform> maps;
    private bool collisionOccurred = false;
    public string destination;
    public Transform DestinationPoint;
    UIManager uiManagerInstance;
    public void Awake()
    {

        uiManagerInstance = UIManager.Instance;

    }

     public void OpenUI()
    {
        uiManagerInstance.potalTxt.text = "��Ż";
        // �浹�� �߻��ϸ� ���¸� true�� ����
        collisionOccurred = true;
        uiManagerInstance.PotalTalk(true);
    }

    public void CloseUI()
    {
        collisionOccurred = false;
        uiManagerInstance.PotalTalk(false);
        //��Ż ���� UI ��Ȱ��ȭ
        UIManager.Instance.TogglePortalUI(false);

    }

    public void TryTalk()
    {
        throw new System.NotImplementedException();
    }

    public void Closer()
    {
        OpenUI();
    }

    public void Interact()
    {
        //GameManager gameManager = FindObjectOfType<GameManager>(); // ���� �Ŵ��� ã��

        //if (gameManager != null)
        //{
        //    GameObject player = gameManager.GetPlayer(); // ���� �Ŵ����� ���� �÷��̾� ���
        //    if (player != null)
        //    {
        //        if (DestinationPoint != null)
        //        {
        //            player.transform.position = DestinationPoint.position;
        //        }
        //    }
        //}

        //��Ż ���� UI Ȱ��ȭ
        UIManager.Instance.TogglePortalUI(true);


    }
    public Vector3 GetPosition()
    {
        Debug.Log(transform.position);

        return transform.position;
    }

    InteractType IInteractive.GetType()
    {
        return InteractType.SuperPotal;
        //throw new System.NotImplementedException();
    }
}
