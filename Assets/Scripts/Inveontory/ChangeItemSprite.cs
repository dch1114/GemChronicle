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

    // public Sprite[] sprite; ������ �Ŵ������� �������� ��������Ʈ�� ���� �־ �迭�� ������ �ʿ�� ����.

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
