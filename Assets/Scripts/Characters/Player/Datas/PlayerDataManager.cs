using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDatas
{
    public int level;
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

public class PlayerDataManager : MonoBehaviour
{
    public PlayerLevelDatabase playerLevelDatabase;

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

    public void SetPlayerLevel()
    {
        Player player = GameManager.Instance.player;

        if (player != null)
        {
            int level = player.Data.StatusData.Level;

            LevelDatas data = playerLevelDatabase.GetLevelDataByKey(level);
            player.Data.StatusData.LoadLevelData(data);
        }

    }
}
