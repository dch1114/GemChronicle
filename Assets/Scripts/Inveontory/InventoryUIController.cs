using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class InventoryUIController : MonoBehaviour
{
    
    [SerializeField] public GameObject inventoryUI;
    [SerializeField] public GameObject shopUi;
    [SerializeField] public CheckPurchasePopup shopTradePopup;
    //[SerializeField] private GameObject skillTreeUI;
    //[SerializeField] private GameObject optionUI;

    public UI_ItemToolTip itemToopTip;
    public UI_ItemToolTip shopitemToolTip;
    // Start is called before the first frame update
    void Start()
    {
        SwitchTo(null);

        itemToopTip.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    SwitchWithKeyTo(inventoryUI);
        //}

        //if (Input.GetKeyDown(KeyCode.K))
        //{
        //    SwitchWithKeyTo(skillTreeUI);
        //}

        //if (Input.GetKeyDown(KeyCode.O))
        //{
        //    SwitchWithKeyTo(optionUI);
        //}

    }

    public void SwitchTo(GameObject _menu)
    {
        for(int i = 0; i< transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        if( _menu != null)
        {
            _menu.SetActive(true);
        }
    }

    public void SwitchWithKeyTo(GameObject _menu)
    {
        if(_menu != null && _menu.activeSelf)
        {
            _menu.SetActive(false);
            return;
        }

        SwitchTo(_menu);
    }

    public void UseShop()
    {
        shopUi.SetActive(true);
    }

    public void CloseShop()
    {
        shopitemToolTip.gameObject.SetActive(false);
        shopTradePopup.gameObject.SetActive(false);
        shopUi.SetActive(false);
    }
}
