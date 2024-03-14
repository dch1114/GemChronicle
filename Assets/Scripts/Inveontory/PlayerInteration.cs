using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteration : MonoBehaviour
{
    
    public float interactionDistance = 2f; // �÷��̾ �������� ȹ���� �� �ִ� �ִ� �Ÿ�
    public LayerMask itemLayer; // ������ ���̾�

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
                    Debug.Log("������ ȹ��");
                    Destroy(itemObject.gameObject);
                }
            }
        }
    }
}
