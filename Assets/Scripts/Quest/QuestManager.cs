using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class QuestManager : MonoBehaviour
{
    public QuestDatabase questDatabase;
    public static QuestManager instance = null;

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

        TextAsset jsonFile = Resources.Load<TextAsset>("JSON/QuestData");
        if (jsonFile != null)
        {
            string json = jsonFile.text;

            questDatabase = JsonUtility.FromJson<QuestDatabase>(json);
            questDatabase.Initialize();
        }
    }
}

