using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class ShopTradePopup : MonoBehaviour
{
    [SerializeField] ShopTradePopup tradepopup;
    [SerializeField] Button buyBtn;
    [SerializeField] Button cancelBtn;
    [SerializeField] Button closeBtn;

    Shop shop;
    InventoryUIController ui;
    Item selectItem;



    private void Start()
    {   
        ui = GetComponentInParent<InventoryUIController>();
        shop = FindObjectOfType<Shop>();
        buyBtn.onClick.AddListener(OnClickBuy);
        cancelBtn.onClick.AddListener(OnClickCancel);
    }

    public void SetItem(Item _item)
    {
        selectItem = _item;
    }

    public void OnClickBuy()
    {
        if(selectItem != null)
        {
            shop.Buy(selectItem);
            ui.itemToopTip.HideToolTip();
            tradepopup.gameObject.SetActive(false);
        }
    }

    public void OnClickCancel()
    {
        ui.itemToopTip.HideToolTip();
        tradepopup.gameObject.SetActive(false);
    }
}
