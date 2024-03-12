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

            // JSON ������ �Ľ��Ͽ� ItemDatabase�� �����մϴ�.
            npcDatabase = JsonUtility.FromJson<NPCDatabase>(json);
            npcDatabase.Initialize();




            // ������ Ư�� �����ۿ� �����ϴ� ���
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

