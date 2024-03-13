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
            //DontDestroyOnLoad(gameObject); 
        }
        else
        {
            if (instance != this) 
                Destroy(this.gameObject); 
        }

        //TextAsset NPCjsonFile = Resources.Load<TextAsset>("JSON/NPCData");
        TextAsset ItemjsonFile = Resources.Load<TextAsset>("Json/Item_Data");
        if (ItemjsonFile != null)
        {
            string json = ItemjsonFile.text;

            //npcDatabase = JsonUtility.FromJson<NPCDatabase>(json);
            //npcDatabase.Initialize();


            itemDatabase = JsonUtility.FromJson<ItemDatabase>(json);
            itemDatabase.Initialize();

        }
           
    }
}
