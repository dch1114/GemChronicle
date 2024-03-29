using UnityEngine;
using UnityEngine.UI;

public class CheckPurchasePopup : MonoBehaviour
{
    [SerializeField] public GameObject shopTradePopup;
    [SerializeField] public CheckPurchasePopup checkPurchasePopup;
    [SerializeField] Button buyBtn;
    [SerializeField] Button cancelBtn;
    [SerializeField] Button closeBtn;
    [SerializeField] Button sellBtn;

    Shop shop;
    InventoryUIController ui;
    Item selectItem;
    InventoryItem wantSell;


    private void Start()
    {
        ui = GetComponentInParent<InventoryUIController>();
        shop = FindObjectOfType<Shop>();
        buyBtn.onClick.AddListener(OnClickBuy);
        cancelBtn.onClick.AddListener(OnClickCancel);
        sellBtn.onClick.AddListener(OnClickSell);
    }

    public void SetItem(Item _item)
    {
        selectItem = _item;
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
        if (selectItem != null)
        {

            //shop.Sell();
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
