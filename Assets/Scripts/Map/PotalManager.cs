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

    //퀘스트가 완료될 때마다 실행됨. questID와 potalID가 동일하면 포탈을 해금한다(게임오브젝트 활성화)
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
    //포탈 이동
    public void MovePotal(int index)
    {
        if (GameManager.Instance != null)
        {
            Player player = GameManager.Instance.player; // 게임 매니저를 통해 플레이어 얻기
            Vector3 targetPos = Vector3.zero;
            if (player != null)
            {
                player.transform.position = potalArray[index].potal.potalPosition;
            }
        }
    }

    //에디터 테스트용
    public void AcceptAllQuest()
    {
        Debug.Log("모든 퀘스트 수락");
        QuestManager.Instance.QuestStart(2001);
        QuestManager.Instance.QuestStart(2002);
        QuestManager.Instance.QuestStart(2003);
        QuestManager.Instance.QuestStart(2004);
    }
    public void CompleteFirstQuest()
    {
        Debug.Log("첫번째 퀘스트 완료");
        QuestManager.Instance.QuestClear(2001);
    }
    public void CompleteSecondQuest()
    {
        Debug.Log("두번째 퀘스트 완료");
        QuestManager.Instance.QuestClear(2002);
    }
    public void CompleteThirdQuest()
    {
        Debug.Log("세번째 퀘스트 완료");
        QuestManager.Instance.QuestClear(2003);
    }

    


}