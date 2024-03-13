using System.Collections;
using System.Collections.Generic;
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

            // JSON 파일을 파싱하여 ItemDatabase에 저장합니다.
            itemDatabase = JsonUtility.FromJson<ItemDatabase>(json);
            itemDatabase.Initialize();




            // 예제로 특정 아이템에 접근하는 방법
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
