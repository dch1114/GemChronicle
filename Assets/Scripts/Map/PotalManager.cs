using Constants;
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

    //퀘스트가 수락 할 때마다 실행됨. questID와 potalID가 동일하면 포탈을 해금한다(게임오브젝트 활성화)
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

    public void AcceptFirstQuest() { QuestManager.Instance.SubscribeQuest(2000); }
    public void AcceptSecondQuest() { QuestManager.Instance.SubscribeQuest(2001); }
    public void AcceptThirdQuest() { QuestManager.Instance.SubscribeQuest(2002); }
    public void CompleteFirstQuest() { QuestManager.Instance.NotifyQuest(QuestType.TalkNpc,2000,1); }
    public void CompleteSecondQuest() { QuestManager.Instance.NotifyQuest(QuestType.TalkNpc, 2000, 1); }
    public void CompleteThirdQuest() { QuestManager.Instance.NotifyQuest(QuestType.KillMonster, 500000, 5); }

}