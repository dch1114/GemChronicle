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
        if (item == null)
        {
            return;
        }

        Item selectItem = item;
        tradePopup.shopTradePopup.gameObject.SetActive(true);
        tradePopup.checkPurchasePopup.gameObject.SetActive(true);
        tradePopup.checkSellPopup.gameObject.SetActive(false);
        ui.shopitemToolTip.ShowToolTip(selectItem);
        tradePopup.SetItem(selectItem);
    }
}
