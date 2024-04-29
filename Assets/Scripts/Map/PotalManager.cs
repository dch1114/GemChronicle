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
    D,
    E
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
    public bool firstEnter = true;
    public bool EnterBossZone;
    [SerializeField] NextMap[] potalArray;

    private void Awake()
    {
        InitPotal();
    }
    private void Start()
    {
        AddEvent();
       
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
                if (index == 0)
                {
                    UIManager.Instance.alertPanelUI.ShowAlert("<color=#009900>마을 동쪽</color>");
                }
                if (index == 1)
                {
                    UIManager.Instance.alertPanelUI.ShowAlert("<color=#990099>보라 마을</color>");
                }
                if (index == 2)
                {
                    UIManager.Instance.alertPanelUI.ShowAlert("<color=#996600>시간의 마을</color>");
                }
                if (index == 3)
                {
                    UIManager.Instance.alertPanelUI.ShowAlert("<color=#2F4F4F>지하 감옥</color>");
                    
                    if (firstEnter == true)
                    {
                        QuestManager.Instance.NotifyQuest(QuestType.learn, 4000, 1);
                        EnterBossZone = true;
                        firstEnter = false;
                        UIManager.Instance.BeforeBosstalk(0);
                    }
                }
            }

            if(MonsterRespawnManager.Instance != null)
            {
                MonsterRespawnManager.Instance.OnRespawn();
            }
        }
    }

    //에디터 테스트용

    public void CompleteFirstQuest() { QuestManager.Instance.NotifyQuest(QuestType.TalkNpc,2000,1); }
    public void CompleteSecondQuest() { QuestManager.Instance.NotifyQuest(QuestType.KillBoss, 500000, 1); }
    public void CompleteThirdQuest() { QuestManager.Instance.NotifyQuest(QuestType.KillBoss, 500001, 1); }
    public void CompleteFourthQuest() { QuestManager.Instance.NotifyQuest(QuestType.TalkNpc, 3000, 1); }
    public void CompleteFiveQuest() { QuestManager.Instance.NotifyQuest(QuestType.TalkNpc, 7000, 1); }
    public void CompleteSixQuest() { QuestManager.Instance.NotifyQuest(QuestType.TalkNpc, 2000, 1); }
    public void CompleteSevenQuest() { QuestManager.Instance.NotifyQuest(QuestType.KillBoss, 500000, 1); }
    public void CompleteEightQuest() { QuestManager.Instance.NotifyQuest(QuestType.KillBoss, 500000, 1); }
    public void CompleteNineQuest() { QuestManager.Instance.NotifyQuest(QuestType.KillBoss, 500000, 1); }
    public void CompleteTenQuest() { QuestManager.Instance.NotifyQuest(QuestType.KillBoss, 500003, 1); }

}