using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LevelData
{
    public int level; //id처럼 쓰임
    public int requiredExp;
    public int atk;
    public int def;
    public int maxHp;
}

[Serializable]
public class PlayerLevelDatabase
{
    public List<LevelData> LevelDatas;
    public Dictionary<int, LevelData> levelDic = new();

    public void Initialize()
    {
        foreach (LevelData data in LevelDatas)
        {
            levelDic.Add(data.level, data);
        }
    }

    public LevelData GetLevelDataByKey(int level)
    {
        if (levelDic.ContainsKey(level))
            return levelDic[level];

        return null;
    }
}