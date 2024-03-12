using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class DataManager : MonoBehaviour
{
    public NPCDatabase npcDatabase;

    void Awake()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("JSON/ITEM_INFO");
        if (jsonFile != null)
        {
            string json = jsonFile.text;

            // JSON 파일을 파싱하여 ItemDatabase에 저장합니다.
            npcDatabase = JsonUtility.FromJson<NPCDatabase>(json);
            npcDatabase.Initialize();




            // 예제로 특정 아이템에 접근하는 방법
        //    int npcKeyToFind = 3;
        //    NPC foundNPC = npcDatabase.GetNPCByKey(npcKeyToFind);
        //    if (foundNPC != null)
        //    {
        //        //Debug.Log("Item Name: " + foundItem.Name);
        //        //Debug.Log("Item Attack: " + foundItem.Attack);
        //        //Debug.Log("Item Gold: " + foundItem.Gold);
        //        //Debug.Log("Item Description: " + foundItem.Description);
        //    }
        //    else
        //    {
        //        Debug.Log("Item with key " + npcKeyToFind + " not found.");
        //    }
        //}
        //else
        //{
        //    Debug.LogError("Failed to load itemDatabase.json");
        }
    }
}

