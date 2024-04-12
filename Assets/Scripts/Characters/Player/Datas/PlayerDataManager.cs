using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelDatas
{
    public int level; //id처럼 쓰임
    public int requiredExp;
    public int atk;
    public int def;
    public int maxHp;
}

public class PlayerLevelDatabase
{
    public List<LevelDatas> levelDatas;
    public Dictionary<int, LevelDatas> levelDic = new();

    public void Initialize()
    {
        foreach (LevelDatas data in levelDatas)
        {
            levelDic.Add(data.level, data);
        }
    }

    public LevelDatas GetLevelDataByKey(int level)
    {
        if (levelDic.ContainsKey(level))
            return levelDic[level];

        return null;
    }
}

[Serializable]
public class PlayerCurrentStatus //현재 상태 저장용
{
    public string name;
    public int level;
    public int exp; //현재 경험치
    public int hp;  //현재 체력
    public int gold;
    public JobType jobType;
    public Dictionary<SkillType, int> gems;
}

public class PlayerDataManager : MonoBehaviour
{
    public PlayerLevelDatabase playerLevelDatabase;
    public PlayerCurrentStatus currentStatus;

    public string saveFileName = "playerData.json";

    private Player player;

    private void Awake()
    {
        TextAsset LevelJson = Resources.Load<TextAsset>("JSON/LevelDatas");

        if (LevelJson != null)
        {
            string json = LevelJson.text;

            playerLevelDatabase = JsonUtility.FromJson<PlayerLevelDatabase>(json);
            playerLevelDatabase.Initialize();
        }
        else
        {
            Debug.Log("Level JSON NULL");
        }
    }

    private void Start()
    {
        player = GameManager.Instance.player;
    }

    public void SetPlayerLevel()
    {
        if (player != null)
        {
            int level = player.Data.StatusData.Level;

            LevelDatas data = playerLevelDatabase.GetLevelDataByKey(level);
            player.Data.StatusData.LoadLevelData(data);
        }
    }

    public void LoadCurrentDatas()
    {
        if(player != null)
        {
            PlayerStatusData data = player.Data.StatusData;

            currentStatus.name = data.Name;
            currentStatus.level = data.Level;
            currentStatus.exp = data.Exp;
            currentStatus.hp = data.Hp;
            currentStatus.gold = data.Gold;
            currentStatus.jobType = data.JobType;
            currentStatus.gems = data.Gems;
        }
    }

    public void SaveCurrentDatas()
    {
        if(player != null)
        {
            PlayerStatusData data = player.Data.StatusData;

            data.Name = currentStatus.name;
            data.Level = currentStatus.level;
            data.Exp = currentStatus.exp;
            data.Hp = currentStatus.hp;
            data.Gold = currentStatus.gold;
            data.JobType = currentStatus.jobType;
            data.Gems = currentStatus.gems;
        }
    }

    public void SavePlayerDataToJson()
    {
        LoadCurrentDatas(); //데이터 집어넣기
        string jsonData = JsonUtility.ToJson(currentStatus);
        string path = Path.Combine(Application.dataPath, saveFileName);
        File.WriteAllText(path, jsonData);
    }

    public void LoadPlayerDataToJson()
    {
        string path = Path.Combine(Application.dataPath, saveFileName);

        if(File.Exists(path))
        {
            string jsonData = File.ReadAllText(path);

            currentStatus = JsonUtility.FromJson<PlayerCurrentStatus>(jsonData);
            SaveCurrentDatas();
        } else
        {
            Debug.Log("게임 이어하기 불가");
        }
    }
}
