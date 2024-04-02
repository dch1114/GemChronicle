using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField] public Item itemDataTest;

    //private void OnValidate()
    //{
    //    GetComponent<SpriteRenderer>().sprite = itemDataTest.sprite;
    //    gameObject.name = "item object - " + itemDataTest.Name;
    //}

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.GetComponent<Player>() != null)
    //    {
    //        Inventory.Instance.AddItem(itemDataTest);
    //        Destroy(gameObject);
    //    }
    //}
}
