using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class LevelData
{
    public int level; //idó�� ����
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

[Serializable]
public class PlayerSkillDatabase
{
    public List<SkillInfoData> SkillDatas;
    public Dictionary<int, SkillInfoData> skillDic = new();

    public void Initialize()
    {
        foreach (SkillInfoData data in SkillDatas)
        {
            skillDic.Add(data.ID, data);
        }
    }

    public List<SkillInfoData> GetDataSection(int start, int end)
    {
        List<SkillInfoData> sectionDatas = new List<SkillInfoData>();
        foreach(SkillInfoData data in SkillDatas)
        {
            if(data.ID >= start && data.ID <= end)
                sectionDatas.Add(data);
        }

        return sectionDatas;
    }
}

[Serializable]
public class PlayerCurrentStatus //���� ���� �����
{
    public string name;
    public int level;
    public int exp; //���� ����ġ
    public int hp;  //���� ü��
    public int gold;
    public JobType jobType;
    public Dictionary<SkillType, int> gems;
}

[Serializable]
public class PlayerDataManager : MonoBehaviour
{
    public PlayerLevelDatabase playerLevelDatabase;
    public PlayerSkillDatabase playerSkillDatabase;
    public PlayerCurrentStatus currentStatus;

    public string saveFileName = "playerData.json";

    private Player player;

    private void Awake()
    {
        LoadLevelDatas();
        LoadSkillDatas();
    }

    private void Start()
    {
        player = GameManager.Instance.player;
        SetPlayerSkillInfos();
    }

    private void LoadLevelDatas()
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

    private void LoadSkillDatas()
    {
        TextAsset SkillJson = Resources.Load<TextAsset>("JSON/SkillDatas");

        if (SkillJson != null)
        {
            string json = SkillJson.text;

            playerSkillDatabase = JsonUtility.FromJson<PlayerSkillDatabase>(json);
            playerSkillDatabase.Initialize();
        }
        else
        {
            Debug.Log("Skill JSON NULL");
        }
    }

    private void SetPlayerSkillInfos()
    {
        int start = 0;
        int end = 0;

        if(player != null)
        {
            switch (player.Data.StatusData.JobType)
            {
                case JobType.Warrior:
                    start = 5000;
                    end = 5999;
                    break;
                case JobType.Magician:
                    start = 6000;
                    end = 6999;
                    break;
                case JobType.Archer:
                    start = 7000;
                    end = 7999;
                    break;
            }

            player.Data.AttackData.SkillInfoDatas = playerSkillDatabase.GetDataSection(start, end);
        }
    }

    public void SetPlayerLevel()
    {
        if (player != null)
        {
            int level = player.Data.StatusData.Level;

            LevelData data = playerLevelDatabase.GetLevelDataByKey(level);
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
        LoadCurrentDatas(); //������ ����ֱ�
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
            Debug.Log("���� �̾��ϱ� �Ұ�");
        }
    }
}
