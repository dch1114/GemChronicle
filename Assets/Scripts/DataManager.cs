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
        if (jsonFile != null)
        {
            string json = jsonFile.text;

            // JSON ������ �Ľ��Ͽ� NPCDatabase(����)�� �����մϴ�.
            npcDatabase = JsonUtility.FromJson<NPCDatabase>(json);
            npcDatabase.Initialize();



          
        }
    }
}

