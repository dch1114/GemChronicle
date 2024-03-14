using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System;

public class UIManager : MonoBehaviour
{
    public enum ShowMenuType  //보기쉽게 하기위해 enum선언
    { 
        Buy,
        Sell,
        Exit,
        Max
    }
    

    public GameObject talkBtn;
    public GameObject potraitPanel;
    public GameObject shopPanel;
    public GameObject shopChoice;
    public static UIManager instance = null;

    public PlayerController playerController;

    public Button[] showMenuButton;

    public Sprite selectButton;
    public Sprite unSelectButton;
    [SerializeField]    
    ShowMenuType currentShowMenuType;

    bool isOpenShowPopUp = false;

    Action selectMenuAction = null;

    void Awake()
    {


        if (instance == null)
        {
            instance = this;
           
        }
        else
        {
            if (instance != this)
                Destroy(this.gameObject);
        }


    }

    public bool IsOpenShowPopup()
    {
        return isOpenShowPopUp;
    }


    private void Start()
    {
        //구매 버튼을 눌렀을때 
        showMenuButton[(int)ShowMenuType.Buy].onClick.AddListener(BuyShop);
        showMenuButton[(int)ShowMenuType.Sell].onClick.AddListener(SellShop);
        showMenuButton[(int)ShowMenuType.Exit].onClick.AddListener(ExitShop);
        //위에 3줄을 없애고 함수 3개를 public 으로 바꿔서 온버튼에서 해도됨
        showMenuButton[(int)ShowMenuType.Buy].image.sprite = unSelectButton;
        showMenuButton[(int)ShowMenuType.Sell].image.sprite = unSelectButton;
        showMenuButton[(int)ShowMenuType.Exit].image.sprite = unSelectButton;
    }


    

    public void talkBtnOnOff(bool _OnOff)
    {
        talkBtn.SetActive(_OnOff);
    }

    public void PotraitPanelOnOff(bool _OnOff)
    {
        potraitPanel.SetActive(_OnOff);
    }

    public void shopPanelOnOff(bool _OnOff)
    {
        shopPanel.SetActive(_OnOff);
    }

    public void shopChoiceOnOff(bool _OnOff)
    {
      
        if (_OnOff)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            //btn.SetActive(_OnOff);
            shopChoice.SetActive(_OnOff);
            //팝업창이 뜨면 처음에 구매탭 버튼이 선택이 되게 버튼 세팅
            PopupShopMenuSelect(ShowMenuType.Buy);
            isOpenShowPopUp = true;

        }
        else
        {

            //NPCInteractive.instance.isAction = false;
            PotraitPanelOnOff(false);
            //btn.SetActive(_OnOff);
            shopChoice.SetActive(_OnOff);
            playerController.isTalking = false;
            isOpenShowPopUp = false;

        }


    }


    public void PopupShopMenuSelect(ShowMenuType type)
    {
        if (currentShowMenuType > ShowMenuType.Exit)
        {
            currentShowMenuType = ShowMenuType.Exit;
            Debug.Log("더이상 아래로 메뉴가 존재하지 않습니다");
            return;
        }

        if (currentShowMenuType < ShowMenuType.Buy)
        {
            currentShowMenuType = ShowMenuType.Buy;
            Debug.Log("더이상 위로 메뉴가 존재하지 않습니다");
            return;
        }


        currentShowMenuType = type;
        //기존에 세팅되어 있는 버튼의 연결을 모두 해제
        selectMenuAction = null;
        //선택될 버튼에 상점타입의 버튼을 연결한다 
        selectMenuAction = GetSelectedShopMenu(type);

    }

    public void RunSelectedMenuButton()
    {
        selectMenuAction?.Invoke();
    }

    Action GetSelectedShopMenu(ShowMenuType type)
    {
        showMenuButton[(int)ShowMenuType.Buy].image.sprite = unSelectButton;
        showMenuButton[(int)ShowMenuType.Sell].image.sprite = unSelectButton;
        showMenuButton[(int)ShowMenuType.Exit].image.sprite = unSelectButton;

        switch (type)
        {
            case ShowMenuType.Buy:
                showMenuButton[(int)ShowMenuType.Buy].image.sprite = selectButton;

                return BuyShop;

            case ShowMenuType.Sell:
                showMenuButton[(int)ShowMenuType.Sell].image.sprite = selectButton;

                return SellShop;
            case ShowMenuType.Exit:
                showMenuButton[(int)ShowMenuType.Exit].image.sprite = selectButton;

                return ExitShop;
            default:
                return null;
        }
    }


    void BuyShop() 
    {

        Debug.Log("Select Buy");
    }

    void SellShop()
    {

        Debug.Log("Select Sell");

    }

    void ExitShop() 
    {
        shopChoiceOnOff(false);
        Debug.Log("Select Exi");

    }


    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            PopupShopMenuSelect(--currentShowMenuType);
        }

        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            //Debug.Log("Key Down");
            PopupShopMenuSelect(++currentShowMenuType);
            //Debug.Log("현재 선택된 메뉴는 : " + currentShowMenuType);
        }

    }





}
