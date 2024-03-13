using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField] public ItemData itemData;  // public ¹Ù²Ü¹æ¹ý
    [SerializeField] public Item itemDataTest;

    private void OnValidate()
    {
        GetComponent<SpriteRenderer>().sprite = itemData.icon;
        gameObject.name = "item object - " + itemData.name;
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.GetComponent<Player>() != null)
    //    {
    //        Inventory.Instance.AddItem(itemData);
    //        Destroy(gameObject);
    //    }
    //}
}
