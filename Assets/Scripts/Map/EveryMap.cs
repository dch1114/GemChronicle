using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static NextMap;




public class EveryMap : MonoBehaviour, IInteractive
{
    public PolygonCollider2D destCol;
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

    }
}
