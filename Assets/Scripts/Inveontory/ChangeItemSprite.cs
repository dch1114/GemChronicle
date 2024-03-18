using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeItemSprite : MonoBehaviour
{
    public SPUM_SpriteList spriteOBj;
    
    GameManager gameManager;
    DataManager dataManager;
    ItemDatabase itemDatabase;
    Inventory inventory;
    InventoryItem inventoryItem;
    Item targetItem;

    string path = "Sprites/item";

    // public Sprite[] sprite; 데이터 매니저에서 아이템이 스프라이트를 갖고 있어서 배열로 선언할 필요는 없다.

    private void Start()
    {
        dataManager = gameManager.dataManager;
        itemDatabase = dataManager.itemDatabase;

        //inventoryItem = inventory.
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))

        {

            //spriteOBj._weaponList[0].sprite = inventoryItem.equipmentDictionaryTest.Values.
        }

        if (Input.GetKeyDown(KeyCode.F2))

        {

            //spriteOBj._weaponList[0].sprite = sprite[1];

        }

        if (Input.GetKeyDown(KeyCode.F3))

        {

            //spriteOBj._weaponList[0].sprite = sprite[2];

        }
    }
}
