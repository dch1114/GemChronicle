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
    public enum ShowMenuType  //���⽱�� �ϱ����� enum����
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
        //���� ��ư�� �������� 
        showMenuButton[(int)ShowMenuType.Buy].onClick.AddListener(BuyShop);
        showMenuButton[(int)ShowMenuType.Sell].onClick.AddListener(SellShop);
        showMenuButton[(int)ShowMenuType.Exit].onClick.AddListener(ExitShop);
        //���� 3���� ���ְ� �Լ� 3���� public ���� �ٲ㼭 �¹�ư���� �ص���
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
            //�˾�â�� �߸� ó���� ������ ��ư�� ������ �ǰ� ��ư ����
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
