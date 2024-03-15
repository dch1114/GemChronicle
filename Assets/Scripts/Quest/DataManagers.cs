using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.Linq;
using Newtonsoft.Json;
using static SoonsoonData;
using UnityEditor.U2D.Aseprite;

public class DataManagers
{
    public static readonly DataManagers instance = new DataManagers();

    private Dictionary<int, ItemData> dicItemData;
    private Dictionary<int, QuestData> dicQuestData;
    private Dictionary<int, RewardItemData> dicRewardItemData;

    //test
    private Dictionary<string, Dictionary<int, RawData>> dic = new Dictionary<string, Dictionary<int, RawData>>();

    private Dictionary<string, string> pathDic = new Dictionary<string, string>();

    //생성자 
    private DataManagers()
    {
        pathDic.Add(typeof(QuestData).ToString(), "quest_data");
        pathDic.Add(typeof(RewardItemData).ToString(), "reward_item_data");
    }

    public void LoadItemData()
    {
        //Resources
        //Data/item_data
        TextAsset asset = Resources.Load<TextAsset>("Data/item_data");
        string json = asset.text;
        Debug.Log(json);
        //역직렬화 
        ItemData[] arr = JsonConvert.DeserializeObject<ItemData[]>(json);
        //foreach, for 돌리면서 dic에 추가 (dic 인스턴스화 필요)

        //Linq 사용 (dic 인스턴화 필요 x)
        this.dicItemData = arr.ToDictionary(x => x.id);    //id를 키로 
        Debug.Log("item data loaded.");
        Debug.LogFormat("item data count: <color=yellow>{0}</color>", this.dicItemData.Count);

    }

    public ItemData GetItemData(int id)
    {
        if (this.dicItemData.ContainsKey(id))
        {
            return this.dicItemData[id];
        }

        Debug.LogFormat("key ({0}) not found.", id);
        return null;
    }

    public ItemData GetRandomItemData()
    {
        //랜덤 아이템 획득 
        //0 ~ (count -1)  + 100 
        var randId = Random.Range(0, this.dicItemData.Count) + 100;   //0 ~ 22 
        return this.GetItemData(randId);
    }

    public void LoadQuestData()
    {
        TextAsset asset = Resources.Load<TextAsset>("Data/quest_data");
        string json = asset.text;
        QuestData[] arr = JsonConvert.DeserializeObject<QuestData[]>(json);
        this.dicQuestData = arr.ToDictionary(x => x.id);
        Debug.LogFormat("quest data loaded : <color=yellow>{0}</color>", this.dicQuestData.Count);
    }

    public QuestData GetQuestData(int id)
    {
        return this.dicQuestData[id];
    }

    public void LoadRewardItemData()
    {
        TextAsset asset = Resources.Load<TextAsset>("Data/reward_item_data");
        string json = asset.text;
        RewardItemData[] arr = JsonConvert.DeserializeObject<RewardItemData[]>(json);
        this.dicRewardItemData = arr.ToDictionary(x => x.id);
        Debug.LogFormat("reward item data loaded : <color=yellow>{0}</color>", this.dicRewardItemData.Count);
    }

    public RewardItemData GetRewardItemData(int id)
    {
        return this.dicRewardItemData[id];
    }

    public void LoadData<T>() where T : RawData
    {
        Debug.LogFormat("LoadData: {0}", typeof(T).ToString());
        var key = typeof(T).ToString();

        var path = this.pathDic[key];

        TextAsset asset = Resources.Load<TextAsset>(string.Format("Data/{0}", path));

        string json = asset.text;
        T[] arr = JsonConvert.DeserializeObject<T[]>(json);

        var a = arr.ToDictionary(x => x.id, x => (RawData)x);

        if (!dic.ContainsKey(key))
        {
            this.dic.Add(key, a);
        }

        Debug.LogFormat("key: {0}", key);
        Debug.LogFormat("{0} loaded : <color=yellow>{1}</color>", path, this.dic[key].Count);

    }

    public Dictionary<int, T> GetDataDic<T>() where T : RawData
    {
        var key = typeof(T).ToString();

        var a = this.dic[key];

        return a.ToDictionary(x => x.Key, x => (T)x.Value);
    }

    public T GetData<T>(int id) where T : RawData
    {
        var key = typeof(T).ToString();

        var a = this.dic[key];

        return a.ToDictionary(x => x.Key, x => (T)x.Value)[id];
    }
}