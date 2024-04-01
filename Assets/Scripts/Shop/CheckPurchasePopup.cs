using UnityEngine;
using UnityEngine.UI;

public class CheckPurchasePopup : MonoBehaviour
{
    [SerializeField] public GameObject shopTradePopup;
    [SerializeField] public GameObject checkPurchasePopup;
    [SerializeField] public GameObject checkSellPopup;
    [SerializeField] Button buyBtn;
    [SerializeField] Button cancelBtn;
    [SerializeField] Button sellBtn;
    [SerializeField] Button closeBtn;

    Shop shop;
    InventoryUIController ui;
    Item selectItem;
    InventoryItem selectInventoryItem;


    private void Start()
    {
        ui = GetComponentInParent<InventoryUIController>();
        shop = FindObjectOfType<Shop>();
        buyBtn.onClick.AddListener(OnClickBuy);
        cancelBtn.onClick.AddListener(OnClickCancel);
        closeBtn.onClick.AddListener(OnClickCancel);
        sellBtn.onClick.AddListener(OnClickSell);
    }

    public void SetItem(Item _item)
    {
        selectItem = _item;
    }

    public void SetItem(InventoryItem _item)
    {
        selectInventoryItem = _item;
    }

    public void OnClickBuy()
    {
        if (selectItem != null)
        {
            shop.Buy(selectItem);
            ui.shopitemToolTip.HideToolTip();
            checkPurchasePopup.gameObject.SetActive(false);
        }
    }

    public void OnClickSell()
    {
        if (selectInventoryItem != null)
        {

            shop.Sell(selectInventoryItem);
            ui.shopitemToolTip.HideToolTip();
            checkSellPopup.gameObject.SetActive(false);
        }
    }

    public void OnClickCancel()
    {
        ui.shopitemToolTip.HideToolTip();
        checkSellPopup.gameObject.SetActive(false);
        checkPurchasePopup.gameObject.SetActive(false);
    }
}
