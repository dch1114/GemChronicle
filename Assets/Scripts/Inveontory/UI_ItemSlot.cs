using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_ItemSlot : MonoBehaviour, IPointerDownHandler //IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemText;

    protected InventoryUIController ui;
    public InventoryItem item;


    protected float clickDelay = 0.3f;
    protected float lastClickTime = 0;


    private void Start()
    {
        ui = GetComponentInParent<InventoryUIController>();
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
        if (item == null || item.stackSize == 0)
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
                ui.itemToopTip.HideToolTip();
            }
            //ui.itemToopTip.HideToolTip();
        }
        else
        {
            AdjustToolTipPosition();
            ui.itemToopTip.ShowToolTip(item.datas);   //test
        }

        lastClickTime = currentTime;
        
        //if (Input.GetKey(KeyCode.LeftControl))
        //{
        //    Inventory.Instance.RemoveItem(item.data);
        //    return;
        //}
    }

    protected void AdjustToolTipPosition()
    {
        Vector2 mousePosition = Input.mousePosition;
        Debug.Log(mousePosition);

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
