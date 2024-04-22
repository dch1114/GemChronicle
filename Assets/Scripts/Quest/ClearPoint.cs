using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ClearPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D _other)

        {
            if (_other != null)
            {
                QuestManager.Instance.NotifyQuest(Constants.QuestType.learn, 4000, 1);
            }
        }
   
}
