using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerQuestManager : MonoBehaviour
{
    public static PlayerQuestManager instance;

    private List<QuestData> playerQuests = new List<QuestData>(); // �÷��̾��� ����Ʈ ���

    private void Awake()
    {
        instance = this;
    }

    // �÷��̾�� ����Ʈ�� �����ϰ� ����Ʈ ��Ͽ� �߰�
    public void OfferQuest(QuestData questData)
    {
        playerQuests.Add(questData);
    }
}