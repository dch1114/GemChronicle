using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class DataManager : MonoBehaviour
{
    //�̱���ȭ
    public NPCDatabase npcDatabase;
    public static DataManager instance = null;

   
    void Awake()
    {
        if (instance == null) 
        {
            instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            if (instance != this) 
                Destroy(this.gameObject); 
        }


   
        TextAsset jsonFile = Resources.Load<TextAsset>("JSON/NPCData");
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public ItemDatabase itemDatabase;

    void Awake()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("JSON/Item_Data");
        if (jsonFile != null)
        {
            string json = jsonFile.text;

            // JSON ������ �Ľ��Ͽ� NPCDatabase(����)�� �����մϴ�.
            npcDatabase = JsonUtility.FromJson<NPCDatabase>(json);
            npcDatabase.Initialize();



          
        }
    }
}

            // JSON ������ �Ľ��Ͽ� ItemDatabase�� �����մϴ�.
            itemDatabase = JsonUtility.FromJson<ItemDatabase>(json);
            itemDatabase.Initialize();




            // ������ Ư�� �����ۿ� �����ϴ� ���
            int itemKeyToFind = 3;
            Item foundItem = itemDatabase.GetItemByKey(itemKeyToFind);
            if (foundItem != null)
            {
                Debug.Log("Item Name: " + foundItem.Name);
                Debug.Log("Item Damage: " + foundItem.Damage);
                Debug.Log("Item Armor: " + foundItem.Armor);
                Debug.Log("Item Description: " + foundItem.Description);
            }
            else
            {
                Debug.Log("Item with key " + itemKeyToFind + " not found.");
            }
        }
        else
        {
            Debug.LogError("Failed to load itemDatabase.json");
        }
    }
}
