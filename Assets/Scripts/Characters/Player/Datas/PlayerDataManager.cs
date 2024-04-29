using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
public class PlayerQuestData
{
    public int questId;
    public int questCount;
    public int subQuestId;
    public int subQuestCount;
}



[Serializable]
public class PlayerDataManager : Singleton<PlayerDataManager>
{
    public PlayerLevelDatabase playerLevelDatabase;
    public PlayerSkillDatabase playerSkillDatabase;
    public PlayerCurrentStatus currentStatus;
    public PlayerCurrentItems currentItems;
    public PlayerQuestData playerQuestData;
    public string saveFileName = "playerData.json";
    public string saveItemFileName = "playerItemData.json";
    public string saveQuestFileName = "playerQuestData.json";


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
            if (GameManager.Instance.isNew)
            {
                CreateNewPlayer();
            }
            else
            {

                LoadPlayerDataFromJson();

            }

        }

        if (UIManager.Instance != null)
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

    void SetNewQuestData()
    {
        QuestManager.Instance.SetAllQuestUI(2000);
        playerQuestData.subQuestId = 0;
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

        SetNewQuestData();
    }

    private void SetNewAttackSkillStates()
    {
        switch (currentStatus.jobType)
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
            UnityEngine.Debug.Log("Level JSON NULL");
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
            UnityEngine.Debug.Log("Skill JSON NULL");
        }
    }

    private void SetPlayerSkillInfos()
    {
        int start = 0;
        int end = 0;

        player = GameManager.Instance.player;
        if (player != null)
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

    private Dictionary<ElementType, int> ChangeGemsListToDic()
    {
        Dictionary<ElementType, int> dic = new Dictionary<ElementType, int>();

        dic.Add(ElementType.Ice, currentStatus.gems[0]);
        dic.Add(ElementType.Fire, currentStatus.gems[1]);
        dic.Add(ElementType.Light, currentStatus.gems[2]);

        return dic;
    }

    private void ChangeGemsDicToList(Dictionary<ElementType, int> gems)
    {
        currentStatus.gems.Clear();
        currentStatus.gems.Add(gems[ElementType.Ice]);
        currentStatus.gems.Add(gems[ElementType.Fire]);
        currentStatus.gems.Add(gems[ElementType.Light]);
    }

    private List<int> ChangeDoubleListToList(List<List<int>> _list)
    {
        List<int> temp = new List<int>();
        for (int i = 0; i < _list.Count; i++)
        {
            for (int j = 0; j < _list[i].Count; j++)
            {
                temp.Add(_list[i][j]);
            }
        }

        return temp;
    }

    private bool IsDooubleListAndCurrentSame(List<List<int>> _list)
    {
        for (int i = 0; i < _list.Count; i++)
        {
            for (int j = 0; j < _list[i].Count; j++)
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
        for (int i = 0; i < _list.Count; i++)
        {
            if (i / 3 == 0)
            {
                temp0.Add(_list[i]);
            }
            else if (i / 3 == 1)
            {
                temp1.Add(_list[i]);
            }
            else if (i / 3 == 2)
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
        if (player != null)
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

    void LoadCurrentQuestData()
    {
        var qMainData = QuestManager.Instance.currentProgressMainQuestData;
        var qSubData = QuestManager.Instance.currentProgressSubQuestData;
        var qOnGoingDic = QuestManager.Instance.GetOnGoingQuest();
        var subQuest = qOnGoingDic.ContainsKey(qSubData.ID) ? qOnGoingDic[qSubData.ID] : null;
        var mainQuest = qOnGoingDic.ContainsKey(qMainData.ID) ? qOnGoingDic[qMainData.ID] : null;






        playerQuestData.questId = qMainData.ID;
        playerQuestData.subQuestId = qSubData.ID;


        if (mainQuest != null)
        {
            playerQuestData.questCount = qOnGoingDic[qMainData.ID].CurrentCount;
        }
        else
        {
            playerQuestData.questCount = 0;
        }

        if (subQuest != null)
        {
            playerQuestData.subQuestCount = qOnGoingDic[qSubData.ID].CurrentCount;
        }
        else
        {
            playerQuestData.subQuestCount = 0;
        }
        


    }

    void SetQuestData()
    {
        //메인 퀘스트 세팅
        QuestManager.Instance.SetAllQuestUI(playerQuestData.questId);
        if (PlayerPrefs.GetInt("2002doing") == 1)
        {
            QuestManager.Instance.SubscribeQuest(2002);
        }
        QuestManager.Instance.QuestUpdate(playerQuestData.questId, playerQuestData.questCount);

        for (int i = 2000; i < playerQuestData.questId + 1; i++)
        {
            PotalManager.Instance.UpdatePotalActiveState(i);
        }

        //서브 퀘스트 세팅
        if (playerQuestData.subQuestId != 0)
        {
            QuestManager.Instance.SubscribeQuest(playerQuestData.subQuestId);
            QuestManager.Instance.QuestUpdate(playerQuestData.subQuestId, playerQuestData.subQuestCount);
        }

    }

    public void SavePlayerDataToJson()
    {
        LoadCurrentDatas(); //������ ����ֱ�
        LoadCurrentQuestData();

        string jsonData = JsonUtility.ToJson(currentStatus);
        string path = Path.Combine(Application.dataPath, saveFileName);


        string itemJsonData = JsonUtility.ToJson(currentItems);
        string itemPath = Path.Combine(Application.dataPath, saveItemFileName);



        string questJsonData = JsonUtility.ToJson(playerQuestData);
        string questPath = Path.Combine(Application.dataPath, saveQuestFileName);

        File.WriteAllText(path, jsonData);
        File.WriteAllText(itemPath, itemJsonData);
        File.WriteAllText(questPath, questJsonData);

        if (UIManager.Instance != null)
        {
            UIManager.Instance.alertPanelUI.ShowAlert("저장되었습니다.");
        }


    }

    public void LoadPlayerDataFromJson()
    {
        string path = Path.Combine(Application.dataPath, saveFileName);
        string itemPath = Path.Combine(Application.dataPath, saveItemFileName);
        string questPath = Path.Combine(Application.dataPath, saveQuestFileName);
        if (File.Exists(path))
        {
            string jsonData = File.ReadAllText(path);
            string itemJsonData = File.ReadAllText(itemPath);
            string questJsonData = File.ReadAllText(questPath);

            currentStatus = JsonUtility.FromJson<PlayerCurrentStatus>(jsonData);
            currentItems = JsonUtility.FromJson<PlayerCurrentItems>(itemJsonData);
            playerQuestData = JsonUtility.FromJson<PlayerQuestData>(questJsonData);

            SetDatas();
            SetQuestData();
        }
        else
        {
            UnityEngine.Debug.Log("파일이 없습니다.");
        }
    }

    private void SetPlayerByJob()
    {
        if (currentStatus != null)
        {
            int jobType = 0;
            switch (currentStatus.jobType)
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
