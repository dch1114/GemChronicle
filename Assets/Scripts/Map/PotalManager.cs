using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PotalType
{
    A,
    B,
    C,
    D
}
[Serializable]
public class Potal
{
    public int potalId;
    public bool isLock = true;
    public Vector3 potalPosition;
}
public class PotalManager : Singleton<PotalManager>
{

    [SerializeField] NextMap[] potalArray;

    private void Start()
    {
        AddEvent();
        InitPotal();
    }

    void AddEvent()
    {
        QuestManager.Instance.OnQuestStartCallback += UpdatePotalActiveState;
    }

    void InitPotal()
    {
        for (int i = 0; i < potalArray.Length; i++)
        {
            potalArray[i].gameObject.SetActive(false);
        }
    }

    public NextMap[] GetPotal()
    { 
        return potalArray;
    }

    //����Ʈ�� �Ϸ�� ������ �����. questID�� potalID�� �����ϸ� ��Ż�� �ر��Ѵ�(���ӿ�����Ʈ Ȱ��ȭ)
    public void UpdatePotalActiveState(int questID)
    {
        //Debug.Log(questID);

        for (int i = 0; i < potalArray.Length; i++)
        {
            if (potalArray[i].potal.potalId == questID)
            {
                potalArray[i].gameObject.SetActive(true);
                potalArray[i].potal.isLock = false;

                break;
            }
        }

    }
    //��Ż �̵�
    public void MovePotal(int index)
    {
        if (GameManager.Instance != null)
        {
            Player player = GameManager.Instance.player; // ���� �Ŵ����� ���� �÷��̾� ���
            Vector3 targetPos = Vector3.zero;
            if (player != null)
            {
                player.transform.position = potalArray[index].potal.potalPosition;
            }
        }
    }

    //������ �׽�Ʈ��
    public void AcceptAllQuest()
    {
        Debug.Log("��� ����Ʈ ����");
        QuestManager.Instance.QuestStart(2001);
        QuestManager.Instance.QuestStart(2002);
        QuestManager.Instance.QuestStart(2003);
        QuestManager.Instance.QuestStart(2004);
    }
    public void CompleteFirstQuest()
    {
        Debug.Log("ù��° ����Ʈ �Ϸ�");
        QuestManager.Instance.QuestClear(2001);
    }
    public void CompleteSecondQuest()
    {
        Debug.Log("�ι�° ����Ʈ �Ϸ�");
        QuestManager.Instance.QuestClear(2002);
    }
    public void CompleteThirdQuest()
    {
        Debug.Log("����° ����Ʈ �Ϸ�");
        QuestManager.Instance.QuestClear(2003);
    }

    


}