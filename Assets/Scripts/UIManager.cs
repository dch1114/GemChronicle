using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System;

public class UIManager : MonoBehaviour
{
    [Header("Paneles")]
    [SerializeField] private GameObject panelInspectorQuests;
    [SerializeField] private GameObject panelPersonajeQuests;
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
    public GameObject btn;
    public static UIManager instance = null;

    public static UIManager instance = null;

    public PlayerController playerController;

    public Button[] showMenuButton;

    public Sprite selectButton;
    public Sprite unSelectButton;
    [SerializeField]    
    ShowMenuType currentShowMenuType;

    bool isOpenShowPopUp = false;
    public PlayerInput playerinput;
    Action selectMenuAction = null;

    //0315 [SerializeField]를 선언하면 외부 스크립트에서 접근할수 없으나 인스펙터에서 세팅 및 확인을 할 수 있음  
    [SerializeField]
    Text talkText;
    [SerializeField]
    Image portraitImg;

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

    //0315 대회메세지 세팅
    public void SetTalkMessage(string msg)
    {
        talkText.text = msg;
    }
    //0315 초상화 이미지 세팅
    public void SetPortraitImage(Sprite sp)
    {
        portraitImg.sprite = sp;
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
            NPCInteractive.instance.isAction = false;
            talkPanelOnOff(false);
            btn.SetActive(_OnOff);
            shopChoice.SetActive(_OnOff);
        }
    }

    public void OpenClosePanelInspectorQuests()
    {
        panelInspectorQuests.SetActive(!panelInspectorQuests.activeSelf);
    }

    public void AbrirCerrarPanelPersonajeQuests()
    {
        panelPersonajeQuests.SetActive(!panelPersonajeQuests.activeSelf);
    }
}
