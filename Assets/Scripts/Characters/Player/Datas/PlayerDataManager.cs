using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

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
    public List<InventoryItem> equipmentItems; // 장착중인 아이템 목록
    public List<InventoryItem> inventoryItems; // 인벤토리 아이템 목록
}

[Serializable]
public class PlayerDataManager : Singleton<PlayerDataManager>
{
    public PlayerLevelDatabase playerLevelDatabase;
    public PlayerSkillDatabase playerSkillDatabase;
    public PlayerCurrentStatus currentStatus;

    public string saveFileName = "playerData.json";

    private Player player;

    [SerializeField] private List<GameObject> players;

    //test
    private Inventory inventory;
    protected override void Awake()
    {
        base.Awake();

        SetGameManagerPlayer(0); //임시
    }

    private void Start()
    {
        LoadDatas();
        SetDatas();
        AddGems(); //임시
        inventory = GameManager.Instance.inventory; //test
    }

    public void LoadDatas()
    {
        LoadLevelDatas();
        LoadSkillDatas();
    }

    public void SetDatas()
    {
        SetPlayerSkillInfos();
        SetPlayerLevel();
    }

    public void AddGems()
    {
        player.Data.StatusData.GetGems(SkillType.Ice, 10);
        player.Data.StatusData.GetGems(SkillType.Fire, 10);
        player.Data.StatusData.GetGems(SkillType.Light, 10);
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

        player = GameManager.Instance.player;
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

            // 플레이어의 인벤토리 데이터
            currentStatus.equipmentItems = inventory.equipmentItems;
            currentStatus.inventoryItems = inventory.inventoryItems;
        }
    }

    public bool IsCurrentDataSaved()
    {
        if(player != null)
        {
            PlayerStatusData data = player.Data.StatusData;

            if (currentStatus.jobType != data.JobType) return false;
            if (currentStatus.name != data.Name) return false;
            if (currentStatus.level != data.Level) return false;
            if (currentStatus.exp != data.Exp) return false;
            if (currentStatus.hp != data.Hp) return false;
            if (currentStatus.gold != data.Gold) return false;
            if( currentStatus.gems != data.Gems) return false;
        }

        return true;
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

            // 플레이어의 인벤토리 데이터
            inventory.equipmentItems = currentStatus.equipmentItems;
            inventory.inventoryItems = currentStatus.inventoryItems;
        }
    }

    public void SavePlayerDataToJson()
    {
        LoadCurrentDatas(); //데이터 집어넣기
        string jsonData = JsonUtility.ToJson(currentStatus);
        string path = Path.Combine(Application.dataPath, saveFileName);
        File.WriteAllText(path, jsonData);

        if(UIManager.Instance != null)
        {
            UIManager.Instance.alertPanelUI.ShowAlert("저장되었습니다.");
        }
    }

    public void LoadPlayerDataToJson()
    {
        string path = Path.Combine(Application.dataPath, saveFileName);

        if(File.Exists(path))
        {
            string jsonData = File.ReadAllText(path);

            currentStatus = JsonUtility.FromJson<PlayerCurrentStatus>(jsonData);
            SetPlayerByJob();
            SaveCurrentDatas();
        } else
        {
            Debug.Log("게임 이어하기 불가");
        }
    }

    private void SetPlayerByJob()
    {
        if(currentStatus != null)
        {
            int jobType = 0;
            switch(currentStatus.jobType)
            {
                case JobType.Warrior:
                    jobType = 0; break;
                case JobType.Archer:
                    jobType = 1; break;
                case JobType.Magician:
                    jobType = 2; break;
            }

            SetGameManagerPlayer(jobType);
        }
    }

    private void SetGameManagerPlayer(int _jobType)
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (i == _jobType)
            {
                players[i].SetActive(true);
                GameManager.Instance.player = players[i].GetComponent<Player>();
            }
            else players[i].SetActive(false);
        }
    }
}
