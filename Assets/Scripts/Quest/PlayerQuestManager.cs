using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerQuestManager : MonoBehaviour
{
    public static PlayerQuestManager instance;

    private List<QuestData> playerQuests = new List<QuestData>(); // 플레이어의 퀘스트 목록

    private void Awake()
    {
        instance = this;
    }

    // 플레이어에게 퀘스트를 제안하고 퀘스트 목록에 추가
    public void OfferQuest(QuestData questData)
    {
        playerQuests.Add(questData);
    }
}