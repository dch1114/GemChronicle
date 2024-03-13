using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class DataManager
{
    public static readonly DataManager instance = new DataManager();

    //private Dictionary<int, ShopData> dicShopData;
    //private Dictionary<int, ItemData> dicItemData;
    private Dictionary<int, QuestData> dicQuestData;

    private DataManager() { }

    //public ShopData GetShopData(int id)
    //{
    //    return this.dicShopData[id];
    //}

    //public void Loadshopdata()
    //{
    //    TextAsset asset = Resources.Load<TextAsset>("data/shop_data");
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

    //public void LoadItemData()
    //{
    //    TextAsset asset = Resources.Load<TextAsset>("Data/item_data");
    //    string json = asset.text;
    //    Debug.Log(json);

    //    ItemData[] arr = JsonConvert.DeserializeObject<ItemData[]>(json);

    //    this.dicItemData = arr.ToDictionary(x => x.id);
    //    Debug.Log("item data loaded.");
    //    Debug.LogFormat("item data count: <color=yellow>{0}</color>", this.dicItemData.Count);
    //}

    //public ItemData GetItemDAta(int id)
    //{
    //    if (this.dicItemData.ContainsKey(id))
    //    {
    //        return this.dicItemData[id];
    //    }

    //    Debug.LogFormat("key ({0}) not found.", id);
    //    return null;
    //}

    //public RewardItemData GetRandomItemData()
    //{
    //    var randId = Random.Range(0, this.dicItemData.Count) + 100;
    //    return this.GetItemDAta(randId);
    //}

    public void LoadQuestDAta()
    {
        TextAsset asset = Resources.Load<TextAsset>("Data/Quest_data");
        string json = asset.text;
        QuestData[] arr = JsonConvert.DeserializeObject<QuestData[]>(json);

        this.dicQuestData = arr.ToDictionary(x => x.id);
        Debug.LogFormat("shop data loaded : {0}", this.dicQuestData.Count);
    }

    public QuestData GetQuestData(int id)
    {
        return dicQuestData[id];
    }
}
