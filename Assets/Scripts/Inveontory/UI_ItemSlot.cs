using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;


public class UI_ItemSlot : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemText;

    private CheckPurchasePopup checkPurChasePopup;
    protected InventoryUIController ui;
    public InventoryItem inventoryItem;

    Color emptySlotColor = new Color(255, 255, 255, 0);

    protected float clickDelay = 0.3f;
    protected float lastClickTime = 0;


    private void Start()
    {
        ui = GetComponentInParent<InventoryUIController>();
        checkPurChasePopup = ui.shopTradePopup;
        UpdateSlot(inventoryItem);
    }
    public void UpdateSlot(InventoryItem _newitem)
    {
        inventoryItem = _newitem;

        itemImage.color = Color.white;

        if (inventoryItem == null || inventoryItem.stackSize == 0)
        {
            itemImage.color = emptySlotColor;
        }

        if (inventoryItem != null)
        {
            //Debug.Log(inventoryItem.datas.sprite == null);
            itemImage.sprite = inventoryItem.datas.sprite; // test
            itemImage.GetComponent<Image>().SetNativeSize();

            if (inventoryItem.stackSize > 1)
            {
                itemText.text = inventoryItem.stackSize.ToString();
            }
            else
            {
                itemText.text = "";
            }
        }

    }

    public void CleanUpSlot()
    {
        inventoryItem = null;

        itemImage.sprite = null;
        itemImage.color = Color.clear;
        itemText.text = "";
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if (inventoryItem == null || inventoryItem.stackSize == 0)
        {
            return;
        }

        float currentTime = Time.time;
        float timeSinceLastClick = currentTime - lastClickTime;

        if (timeSinceLastClick <= clickDelay)
        {
            if (ui.shopUi.activeSelf)
            {
                checkPurChasePopup.SetItem(inventoryItem);
                checkPurChasePopup.gameObject.SetActive(true);

                if(inventoryItem.datas.ItemType != ItemType.Potion)
                {
                    checkPurChasePopup.checkSellPopup.SetActive(true);
                    checkPurChasePopup.checkPotionSellPopup.SetActive(false);
                }
                else
                {
                    checkPurChasePopup.amountSwitch.amount = 0;
                    checkPurChasePopup.amountSwitch.UpdateText();
                    checkPurChasePopup.checkPotionSellPopup.SetActive(true);
                    checkPurChasePopup.checkSellPopup.SetActive(false);
                }
                ui.shopitemToolTip.ShowToolTip(inventoryItem.datas);
            }
            else
            {
                if (inventoryItem.datas.ItemType == ItemType.Equipment)
                {
                    Inventory.Instance.EquipItem(inventoryItem);
                    ui.itemToopTip.HideToolTip();
                }
                else if(inventoryItem.datas.ItemType == ItemType.Potion)
                {
                    Inventory.Instance.UseItem(inventoryItem);
                    ui.itemToopTip.HideToolTip();
                }
            }
        }
        else
        {
            if(!ui.shopUi.activeSelf)
            {
                AdjustToolTipPosition();
                ui.itemToopTip.ShowToolTip(inventoryItem.datas);
            }
        }

        lastClickTime = currentTime;

        if (Input.GetKey(KeyCode.LeftControl))
        {
            Inventory.Instance.RemoveItem(inventoryItem);
            return;
        }
    }

    protected void AdjustToolTipPosition()
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
