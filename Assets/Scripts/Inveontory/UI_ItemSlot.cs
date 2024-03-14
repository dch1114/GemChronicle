using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_ItemSlot : MonoBehaviour, IPointerDownHandler //IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemText;

    protected UI ui;
    public InventoryItem item;

    private UI_EquipmentSlot equipmentSlot;

    float clickDelay = 0.3f;
    float lastClickTime = 0;


    private void Start()
    {
        ui = GetComponentInParent<UI>();
        equipmentSlot = GetComponentInParent<UI_EquipmentSlot>();
    }
    public void UpdateSlot(InventoryItem _newitem)
    {
        item = _newitem;

        itemImage.color = Color.white;

        if(item != null)
        {
            itemImage.sprite = item.datas.sprite; // test

            if (item.stackSize > 1)
            {
                itemText.text = item.stackSize.ToString();
            }
            else
            {
                itemText.text = "";
            }
        }
    }

    public void CleanUpSlot()
    {
        item = null;

        itemImage.sprite = null;
        itemImage.color = Color.clear;
        itemText.text = "";
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if (item == null)
        {
            return;
        }

        float currentTime = Time.time;
        float timeSinceLastClick = currentTime - lastClickTime;

        if (timeSinceLastClick <= clickDelay)
        {
            if (item.datas.ItemType == ItemType.Equipment) // test
            {
                Inventory.instance.EquipItemTest(item.datas);
                //ui.itemtooptip.hidetooltip();
            }
            //ui.itemToopTip.HideToolTip();
        }
        else
        {
            AdjustToolTipPosition();
            //ui.itemToopTip.ShowToolTip(item.data as ItemData_Equipment);   //test
        }

        lastClickTime = currentTime;
        //if (Input.GetKey(KeyCode.LeftControl))
        //{
        //    Inventory.Instance.RemoveItem(item.data);
        //    return;
        //}

        //AdjustToolTipPosition();
        //ui.itemToopTip.ShowToolTip(item.data as ItemData_Equipment);


        //if (item.data.itemType == ItemType.Equipment)
        //{
        //    Inventory.Instance.EquipItem(item.data);
        //}

    }

    protected void AdjustToolTipPosition()
    {
        Vector2 mousePosition = Input.mousePosition;

        float xOffset = 0;
        float yOffset = 0;

        if (mousePosition.x > 600)
        {
            xOffset = -75;
        }
        else
        {
            xOffset = 75;
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

    //public void OnPointerEnter(PointerEventData eventData)
    //{
    //    if(item == null)
    //    {
    //        return;
    //    }

    //    Vector2 mousePosition = Input.mousePosition;

    //    float xOffset = 0;
    //    float yOffset = 0;

    //    if(mousePosition.x > 600)
    //    {
    //        xOffset = -75;
    //    }
    //    else
    //    {
    //        xOffset = 75;
    //    }

    //    if(mousePosition.y > 320)
    //    {
    //        yOffset = -75;
    //    }
    //    else
    //    {
    //        yOffset = 75;
    //    }

    //    ui.itemToopTip.ShowToolTip(item.data as ItemData_Equipment);
    //    ui.itemToopTip.transform.position = new Vector2(mousePosition.x + xOffset, mousePosition.y + yOffset);
    //}

    //public void OnPointerExit(PointerEventData eventData)
    //{
    //    if (item == null)
    //    {
    //        return;
    //    }

    //    ui.itemToopTip.HideToolTip();
    //}
}
