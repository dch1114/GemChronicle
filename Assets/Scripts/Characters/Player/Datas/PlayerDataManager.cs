using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[Serializable]
public class PlayerCurrentStatus //���� ���� �����
{
    public string name;
    public int level;
    public int exp; //���� ����ġ
    public int hp;  //���� ü��
    public int gold;
    public JobType jobType;
    public List<int> gems;
    public Vector3 currentPos;
    public List<int> attackSkillStates;
}

[Serializable]
public class PlayerCurrentItems
{
    public List<InventoryItem> equipmentItems;
    public List<InventoryItem> inventoryItems;
}

[Serializable]
public class PlayerDataManager : Singleton<PlayerDataManager>
{
    public PlayerLevelDatabase playerLevelDatabase;
    public PlayerSkillDatabase playerSkillDatabase;
    public PlayerCurrentStatus currentStatus;
    public PlayerCurrentItems currentItems;

    public string saveFileName = "playerData.json";
    public string saveItemFileName = "playerItemData.json";

    private Player player;

    [SerializeField] private List<GameObject> players;

    //test
    [SerializeField] private Inventory inventory;
    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        LoadDatas();
        //SetGameManagerInventory();

        if (GameManager.Instance != null)
        {
            if(GameManager.Instance.isNew)
            {
                CreateNewPlayer();
            } else
            {
                LoadPlayerDataToJson();
            }

        }

        if(UIManager.Instance != null)
        {
            UIManager.Instance.playerUI.StartPlayerUI();
        }

        // 먼저 inventoryUIController의 요소를 불러와야 한다.
        UpdateInventoryUI();
    }

    public void LoadDatas()
    {
        LoadLevelDatas();
        LoadSkillDatas();
    }

    public void SetDatas()
    {
        SetPlayerByJob();

        SaveCurrentDatas();
        SetPlayerSkillInfos();
        SetPlayerLevel();
        SetPlayerDataToInventory();
    }

    public void CreateNewPlayer()
    {
        //currentStatus = new PlayerCurrentStatus();

        currentStatus.name = GameManager.Instance.playerName;
        currentStatus.jobType = GameManager.Instance.playerJob;
        currentStatus.level = 1;
        currentStatus.exp = 0;

        LevelData data = playerLevelDatabase.GetLevelDataByKey(currentStatus.level);
        currentStatus.hp = data.maxHp;
        currentStatus.gold = 3000; //�⺻ �� ���� ���ؾ�
        currentStatus.currentPos = new Vector3(0f, 46.3f, 0f);
        SetNewAttackSkillStates();

        InitGems();

        SetDatas();
    }

    private void SetNewAttackSkillStates()
    {
        switch(currentStatus.jobType)
        {
            case JobType.Archer:
                currentStatus.attackSkillStates = new List<int>() { 0, 0, 0, 1, 0, 0, 2, 0, 0 };
                break;
            default:
                currentStatus.attackSkillStates = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                break;
        }
    }

    public void InitGems()
    {
        currentStatus.gems = new List<int> { 10, 10, 10 };
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

    private Dictionary<SkillType, int> ChangeGemsListToDic()
    {
        Dictionary<SkillType, int> dic = new Dictionary<SkillType, int>();

        dic.Add(SkillType.Ice, currentStatus.gems[0]);
        dic.Add(SkillType.Fire, currentStatus.gems[1]);
        dic.Add(SkillType.Light, currentStatus.gems[2]);

        return dic;
    }

    private void ChangeGemsDicToList(Dictionary<SkillType, int> gems)
    {
        currentStatus.gems.Clear();
        currentStatus.gems.Add(gems[SkillType.Ice]);
        currentStatus.gems.Add(gems[SkillType.Fire]);
        currentStatus.gems.Add(gems[SkillType.Light]);
    }

    private List<int> ChangeDoubleListToList(List<List<int>> _list)
    {
        List<int> temp = new List<int>();
        for(int i = 0; i <  _list.Count; i++)
        {
            for(int j = 0; j < _list[i].Count; j++)
            {
                temp.Add(_list[i][j]);
            }
        }

        return temp;
    }

    private bool IsDooubleListAndCurrentSame(List<List<int>> _list)
    {
        for(int i = 0; i < _list.Count; i++)
        {
            for(int j = 0; j < _list[i].Count; j++)
            {
                if (_list[i][j] != currentStatus.attackSkillStates[i * 3 + j]) return false;
            }
        }

        return true;
    }

    private List<List<int>> ChangeListToDoubleList(List<int> _list)
    {
        List<List<int>> answer = new List<List<int>>();

        List<int> temp0 = new List<int>();
        List<int> temp1 = new List<int>();
        List<int> temp2 = new List<int>();
        for(int i = 0; i < _list.Count; i++)
        {
            if (i / 3 == 0)
            {
                temp0.Add(_list[i]);
            } else if(i/3 == 1)
            {
                temp1.Add(_list[i]);
            } else if(i / 3 == 2)
            {
                temp2.Add(_list[i]);
            }
        }

        answer.Add(temp0);
        answer.Add(temp1);
        answer.Add(temp2);

        return answer;
    }

    public void LoadCurrentDatas()
    {
        player = GameManager.Instance.player;
        if(player != null)
        {
            PlayerStatusData data = player.Data.StatusData;

            currentStatus.name = data.Name;
            currentStatus.level = data.Level;
            currentStatus.exp = data.Exp;
            currentStatus.hp = data.Hp;
            currentStatus.gold = data.Gold;
            currentStatus.jobType = data.JobType;
            ChangeGemsDicToList(data.Gems);
            currentStatus.attackSkillStates = ChangeDoubleListToList(player.Data.AttackData.AttackSkillStates);

            // �÷��̾��� �κ��丮 ������
            currentItems.equipmentItems = inventory.equipmentItems;
            currentItems.inventoryItems = inventory.inventoryItems;
            currentStatus.currentPos = player.transform.position;
        }
    }

    public bool IsCurrentDataSaved()
    {
        player = GameManager.Instance.player;
        if (player != null)
        {
            PlayerStatusData data = player.Data.StatusData;

            if (currentStatus.jobType != data.JobType) return false;
            if (currentStatus.name != data.Name) return false;
            if (currentStatus.level != data.Level) return false;
            if (currentStatus.exp != data.Exp) return false;
            if (currentStatus.hp != data.Hp) return false;
            if (currentStatus.gold != data.Gold) return false;
            if (!IsDooubleListAndCurrentSame(player.Data.AttackData.AttackSkillStates)) return false;
            if (currentStatus.currentPos != player.transform.position) return false;
        }

        return true;
    }

    public void SaveCurrentDatas()
    {
        player = GameManager.Instance.player;
        inventory = GameManager.Instance.inventory;
        if (player != null)
        {
            PlayerStatusData data = player.Data.StatusData;

            data.Name = currentStatus.name;
            data.Level = currentStatus.level;
            data.Exp = currentStatus.exp;
            data.Hp = currentStatus.hp;
            data.Gold = currentStatus.gold;
            data.JobType = currentStatus.jobType;
            data.Gems = ChangeGemsListToDic();
            player.Data.AttackData.AttackSkillStates = ChangeListToDoubleList(currentStatus.attackSkillStates);
            player.transform.position = currentStatus.currentPos;

            // �÷��̾��� �κ��丮 ������
            inventory.equipmentItems = currentItems.equipmentItems;
            inventory.inventoryItems = currentItems.inventoryItems;
            player.transform.position = currentStatus.currentPos;
        }
    }

    public void SavePlayerDataToJson()
    {
        LoadCurrentDatas(); //������ ����ֱ�
        string jsonData = JsonUtility.ToJson(currentStatus);
        string path = Path.Combine(Application.dataPath, saveFileName);
        string itemJsonData = JsonUtility.ToJson(currentItems);
        string itemPath = Path.Combine(Application.dataPath, saveItemFileName);

        File.WriteAllText(path, jsonData);
        File.WriteAllText(itemPath, itemJsonData);

        if(UIManager.Instance != null)
        {
            UIManager.Instance.alertPanelUI.ShowAlert("저장되었습니다.");
        }
    }

    public void LoadPlayerDataToJson()
    {
        string path = Path.Combine(Application.dataPath, saveFileName);
        string itemPath = Path.Combine (Application.dataPath, saveItemFileName);

        if(File.Exists(path))
        {
            string jsonData = File.ReadAllText(path);
            string itemJsonData = File.ReadAllText(itemPath);

            currentStatus = JsonUtility.FromJson<PlayerCurrentStatus>(jsonData);
            currentItems = JsonUtility.FromJson<PlayerCurrentItems>(itemJsonData);

            SetDatas();
        } else
        {
            Debug.Log("파일이 없습니다.");
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

        player = GameManager.Instance.player;
        GameManager.Instance.inventory = inventory.GetComponent<Inventory>();
        CameraController.Instance.FollowPlayer();
    }

    private void SetGameManagerInventory()
    {
        GameManager.Instance.inventory = inventory.GetComponent<Inventory>();

    }

    private void SetPlayerDataToInventory()
    {
        inventory.SetPlayerData();
    }

    private void UpdateInventoryUI()
    {
        inventory.UpdateLoadInventoryItems();
    }
}
