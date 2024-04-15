using UnityEngine;
using UnityEngine.UI;

public class CheckPurchasePopup : MonoBehaviour
{
    [SerializeField] public GameObject shopTradePopup;
    [SerializeField] public GameObject checkPurchasePopup;
    [SerializeField] public GameObject checkSellPopup;
    [SerializeField] public GameObject checkPotionPurchasePopup;
    [SerializeField] public GameObject checkPotionSellPopup;
    [SerializeField] Button buyBtn;
    [SerializeField] Button cancelBtn;
    [SerializeField] Button sellBtn;
    [SerializeField] Button closeBtn;
    [SerializeField] Button potionBuyBtn;
    [SerializeField] Button potionCancelBtn;
    [SerializeField] Button potionSellBtn;
    [SerializeField] Button potioncloseBtn;

    Shop shop;
    InventoryUIController ui;
    Item selectItem;
    InventoryItem selectInventoryItem;

    Switch _amountSwitch;

    public Switch amountSwitch
    {
        get { return _amountSwitch; }
        set { _amountSwitch = value; }
    }

    private void Start()
    {
        ui = GetComponentInParent<InventoryUIController>();
        shop = FindObjectOfType<Shop>();
        amountSwitch = GetComponentInChildren<Switch>();
        buyBtn.onClick.AddListener(OnClickBuy);
        cancelBtn.onClick.AddListener(OnClickCancel);
        closeBtn.onClick.AddListener(OnClickCancel);
        sellBtn.onClick.AddListener(OnClickSell);
        potionBuyBtn.onClick.AddListener(OnClickPotionBuy);
        potionCancelBtn.onClick.AddListener(OnClickPotionCancel);
        potionSellBtn.onClick.AddListener(OnClickSell);
        potioncloseBtn.onClick.AddListener(OnClickPotionCancel);
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

    public void OnClickPotionBuy()
    {
        if (selectItem != null && selectItem.ItemType == ItemType.Potion)
        {
            shop.BuyPotion(selectItem, amountSwitch.amount);
            ui.shopitemToolTip.HideToolTip();
            checkPotionPurchasePopup.gameObject.SetActive(false);
        }
    }

    public void OnClickPotionSell()
    {

    }

    public void OnClickPotionCancel()
    {
        ui.shopitemToolTip.HideToolTip();
        checkPotionSellPopup.gameObject.SetActive(false);
        checkPotionPurchasePopup.gameObject.SetActive(false);
    }
}
