using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class DataManager : MonoBehaviour
{
    
    public NPCDatabase npcDatabase;
    public static DataManager instance = null;
    public ItemDatabase itemDatabase;

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


   
        TextAsset NPCjsonFile = Resources.Load<TextAsset>("JSON/NPCData");
        TextAsset jsonFile = Resources.Load<TextAsset>("JSON/Item_Data");
        if (jsonFile != null)
        {
            string json = jsonFile.text;

            // JSON ������ �Ľ��Ͽ� NPCDatabase(����)�� �����մϴ�.
            npcDatabase = JsonUtility.FromJson<NPCDatabase>(json);
            npcDatabase.Initialize();


            itemDatabase = JsonUtility.FromJson<ItemDatabase>(json);
            itemDatabase.Initialize();

        }
        

    

      
       
         
           
    }
}
