using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_ShopSlot : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemText;

    private Shop shop;
    private CheckPurchasePopup tradePopup;

    private InventoryUIController ui;
    public Item item;

    //private float clickDelay = 0.3f;
    //private float lastClickTime = 0;

    private void Start()
    {
        ui = GetComponentInParent<InventoryUIController>();
        shop = FindObjectOfType<Shop>();
        tradePopup = ui.shopTradePopup;
        UpdateSlot(item);
    }

    public void UpdateSlot(Item _shopitem)
    {
        item = _shopitem;

        itemImage.color = Color.white;

        if (item != null)
        {
            itemImage.sprite = item.sprite; // test
            itemImage.GetComponent<Image>().SetNativeSize();

            //if (item.Quantity > 1)
            //{
            //    itemText.text = item.Quantity.ToString();
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
        if (item == null)
        {
            return;
        }

        if ((item.ItemType != ItemType.Potion))
        {
            Item selectItem = item;
            tradePopup.checkPurchasePopup.gameObject.SetActive(true);
            tradePopup.checkPotionPurchasePopup.gameObject.SetActive(false);
            tradePopup.shopTradePopup.gameObject.SetActive(true);
            tradePopup.checkSellPopup.gameObject.SetActive(false);
            ui.shopitemToolTip.ShowToolTip(selectItem);
            tradePopup.SetItem(selectItem); 
        }
        else
        {
            if (tradePopup.amountSwitch.amount > 0)
            {
                tradePopup.amountSwitch.amount = 0;
                tradePopup.amountSwitch.UpdateText();
            }

            Item selectItem = item;
            tradePopup.checkPotionPurchasePopup.gameObject.SetActive(true);
            tradePopup.checkPurchasePopup.gameObject.SetActive(false);
            tradePopup.shopTradePopup.gameObject.SetActive(true);
            tradePopup.checkPotionSellPopup.gameObject.SetActive(false);
            ui.shopitemToolTip.ShowToolTip(selectItem);
            tradePopup.SetItem(selectItem);
        }
    }
}
