using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class CheckPurchasePopup : MonoBehaviour
{
    [SerializeField] public GameObject shopTradePopup;
    [SerializeField] public CheckPurchasePopup checkPurchasePopup;
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
            ui.shopitemToolTip.HideToolTip();
            checkPurchasePopup.gameObject.SetActive(false);
        }
    }

    public void OnClickCancel()
    {
        ui.shopitemToolTip.HideToolTip();
        checkPurchasePopup.gameObject.SetActive(false);
    }
}
