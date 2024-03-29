using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextMap : MonoBehaviour, IInteractive
{
    public enum NextPositionType
    {
        InitPosition,
        SomePosition,
    };
    public NextPositionType nextPositionType;
    public Transform DestinationPoint;
    public string destination;
    // 충돌이 발생한지 여부를 나타내는 변수
    private bool collisionOccurred = false;
    UIManager uiManagerInstance;
    
    public Potal potal;

    public void Start()
    {
        uiManagerInstance = UIManager.Instance;
        potal.potalPosition = transform.position;
    }

    public void OpenUI()
    {
        uiManagerInstance.potalTxt.text = destination;
        // 충돌이 발생하면 상태를 true로 변경
        collisionOccurred = true;
        uiManagerInstance.PotalTalk(true);
    }

    public void CloseUI()
    {
        collisionOccurred = false;
        uiManagerInstance.PotalTalk(false);
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
        GameManager gameManager = FindObjectOfType<GameManager>(); // 게임 매니저 찾기

        if (gameManager != null)
        {
            GameObject Player = gameManager.GetPlayer(); // 게임 매니저를 통해 플레이어 얻기

            if (Player != null)
            {
                if (nextPositionType == NextPositionType.InitPosition)
                {
                    Player.transform.position = Vector3.zero;
                }
                else if (nextPositionType == NextPositionType.SomePosition)
                {
                    if (DestinationPoint != null)
                    {
                        Player.transform.position = DestinationPoint.position;
                    }
                    else
                    {

                    }
                }
                else
                {

                }
            }
            else
            {

            }
        }

    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    InteractType IInteractive.GetType()
    {
        return InteractType.Potal;
    }



    private void OnEnable()
    {
        Quest.EventQuestCompleted += QuestCompleted;
        PotalManager.Instance.AddPotal(potal);
    }

    private void OnDisable()
    {
        Quest.EventQuestCompleted -= QuestCompleted;
        PotalManager.Instance.RemovePotal(potal);
    }

    private void QuestCompleted(Quest questCompleted)
    {
        Debug.Log("Quest Complete");
        if (questCompleted.potalID == potal.potalId)
        {
            potal.isLock = false;
        }
    }
}