using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class HideNpc : MonoBehaviour
{
    // Start is called before the first frame update
 
    void Update()
    {
        if (QuestManager.Instance != null)
        {
            if (QuestManager.Instance.hideNPC == true)
            {
                gameObject.SetActive(false);
            }
            if(QuestManager.Instance.hideNPC == false)
            {
                gameObject.SetActive (true);
            }
        }
    }
}
