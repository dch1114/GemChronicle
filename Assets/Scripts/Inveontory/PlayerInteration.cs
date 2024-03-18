using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteration : MonoBehaviour
{
    
    public float interactionDistance = 2f; // 플레이어가 아이템을 획득할 수 있는 최대 거리
    public LayerMask itemLayer; // 아이템 레이어

    private void Awake()
    {

    }
    private void Update()
    {
        GetItem();
    }

    private void GetItem()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, interactionDistance, itemLayer);

            if (hit.collider != null)
            {
                ItemObject itemObject = hit.collider.GetComponent<ItemObject>();
                if (itemObject != null)
                {
                    if(!Inventory.instance.CanAddItem()) //&& itemObject.itemData.itemType == ItemType.Equipment)
                    {
                        return;
                    }

                    //Inventory.Instance.AddItem(itemObject.itemData);
                    Debug.Log("아이템 획득");
                    Destroy(itemObject.gameObject);
                }
            }
        }
    }
}
