using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class DataManager : MonoBehaviour
{
    //싱글톤화
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

            // JSON 파일을 파싱하여 NPCDatabase(변수)에 저장합니다.
            npcDatabase = JsonUtility.FromJson<NPCDatabase>(json);
            npcDatabase.Initialize();



          
        }
    }
}

