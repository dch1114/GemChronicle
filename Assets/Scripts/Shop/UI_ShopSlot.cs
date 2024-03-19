using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_ShopSlot : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemText;

    private Shop shop;

    private InventoryUIController ui;
    public Item item;

    private float clickDelay = 0.3f;
    private float lastClickTime = 0;

    private void Start()
    {
        ui = GetComponentInParent<InventoryUIController>();
        shop = FindObjectOfType<Shop>();
        UpdateSlot(item);
    }

    public void UpdateSlot(Item _shopitem)
    {
        item = _shopitem;

        itemImage.color = Color.white;

        //if (item == null || item.stackSize == 0)
        //{
        //    itemImage.color = emptySlotColor;
        //}

        if (item != null)
        {
            itemImage.sprite = item.sprite; // test
            itemImage.GetComponent<Image>().SetNativeSize();

            //if (item.stackSize > 1)
            //{
            //    itemText.text = item.stackSize.ToString();
            //}
            //else
            //{
            //    itemText.text = "";
            //}
        }
    }

    public void CleanUpSlot()
    {
        item = null;

        itemImage.sprite = null;
        itemImage.color = Color.clear;
        itemText.text = "";
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (item == null) //|| item.stackSize == 0)
        {
            return;
        }

    
        float currentTime = Time.time;
        float timeSinceLastClick = currentTime - lastClickTime;

        if (timeSinceLastClick <= clickDelay)
        {
            shop.Buy(item);
            //UpdateSlot(item);
            //ui.tradePopup.SetActive(true);

            //if (item.datas.ItemType == ItemType.Equipment) // test
            //{
            //    Inventory.instance.EquipItem(item.datas);
            //    ui.itemToopTip.HideToolTip();
            //}
            //ui.itemToopTip.HideToolTip();
        }
        else
        {
            AdjustToolTipPosition();
            ui.itemToopTip.ShowToolTip(item);   //test
        }

        lastClickTime = currentTime;
    }

    private void AdjustToolTipPosition()
    {
        Vector2 mousePosition = Input.mousePosition;

        float xOffset;
        float yOffset;

        if (mousePosition.x > 600)
        {
            xOffset = -150;
        }
        else
        {
            xOffset = 150;
        }

        if (mousePosition.y > 320)
        {
            yOffset = -75;
        }
        else
        {
            yOffset = 75;
        }

        ui.itemToopTip.transform.position = new Vector2(mousePosition.x + xOffset, mousePosition.y + yOffset);
    }

}
