using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class QuestDataManager
{
    public static readonly QuestDataManager instance = new QuestDataManager();

    //private Dictionary<int, ShopData> dicShopData;
    private Dictionary<int, ItemData> dicItemData;
    private Dictionary<int, QuestData> dicQuestData;
    private Dictionary<int, RewardItemData> dicRewardItemData;

    //test
    private Dictionary<string, Dictionary<int, RawData>> dic = new Dictionary<string, Dictionary<int, RawData>>();

    private Dictionary<string, string> pathDic = new Dictionary<string, string>();

    private QuestDataManager()
    {
        pathDic.Add(typeof(QuestData).ToString(), "quest_data");
        pathDic.Add(typeof(RewardItemData).ToString(), "reward_item_data");
    }

    //public ShopData GetShopData(int id)
    //{
    //    return this.dicShopData[id];
    //}

    //public void Loadshopdata()
    //{
    //    TextAsset asset = Resources.Load<TextAsset>("Data/shop_data");
    //    var json = asset.text;
    //    Debug.Log(json);

    //    ShopData[] arrShopDatas = JsonConvert.DeserializeObject<short[]>(json);

    //    this.dicShopData = arrShopDatas.ToDictionary(x => x.id);

    //    Debug.LogFormat("shop data loaded : {0}", this.dicShopData.Count);
    //}

    //public List<ShopData> GetShopDatas()
    //{
    //    return this.dicShopData.Values.ToList();
    //}

    public void LoadItemData()
    {
        TextAsset asset = Resources.Load<TextAsset>("Data/item_data");
        string json = asset.text;
        Debug.Log(json);

        ItemData[] arr = JsonConvert.DeserializeObject<ItemData[]>(json);

        //this.dicItemData = arr.ToDictionary(x => x.id);
        Debug.Log("item data loaded.");
        Debug.LogFormat("item data count: <color=yellow>{0}</color>", this.dicItemData.Count);
    }

    public ItemData GetItemDAta(int id)
    {
        if (this.dicItemData.ContainsKey(id))
        {
            return this.dicItemData[id];
        }

        Debug.LogFormat("key ({0}) not found.", id);
        return null;
    }

    //public rewarditemdata getrandomitemdata()
    //{
    //    var randid = random.range(0, this.dicitemdata.count) + 100;
    //    return this.getitemdata(randid);
    //}

    public void LoadQuestData()
    {
        TextAsset asset = Resources.Load<TextAsset>("Data/Quest_data");
        string json = asset.text;
        QuestData[] arr = JsonConvert.DeserializeObject<QuestData[]>(json);

        this.dicQuestData = arr.ToDictionary(x => x.id);
        Debug.LogFormat("quest data loaded : <color=yellow>{0}</color?", this.dicQuestData.Count);
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
        Debug.LogFormat("LoadData: {0}",typeof(T).ToString());
        var Key = typeof(T).ToString();

        var path = this.pathDic[Key];

        TextAsset asset = Resources.Load<TextAsset>(string.Format("Data/{0}", path));

        string json = asset.text;
        T[] arr = JsonConvert.DeserializeObject<T[]>(json);

        var a = arr.ToDictionary(x => x.id, x => (RawData) x);

        if (!dic.ContainsKey(Key))
        {
            this.dic.Add(Key, a);
        }
        Debug.LogFormat("key: {0}", Key);
        Debug.LogFormat("{0} loaded : <color=yellow>{1}</color>", path, this.dic[Key].Count);
    }

    public Dictionary<int, T> GetDataDic<T>() where T : RawData
    {
        var Key = typeof(T).ToString();

        var a = this.dic[Key];

        return a.ToDictionary(x => x.Key, x => (T)x.Value);
    }
}
