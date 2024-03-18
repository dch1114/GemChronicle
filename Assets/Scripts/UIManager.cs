using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Paneles")]
    [SerializeField] private GameObject panelInspectorQuests;
    [SerializeField] private GameObject panelPersonajeQuests;

    [Header("Texto")]
    [SerializeField] private TextMeshProUGUI monedasTMP;

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

    //0315 [SerializeField]�� �����ϸ� �ܺ� ��ũ��Ʈ���� �����Ҽ� ������ �ν����Ϳ��� ���� �� Ȯ���� �� �� ����  
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
        //���� ��ư�� �������� 
        showMenuButton[(int)ShowMenuType.Buy].onClick.AddListener(BuyShop);
        showMenuButton[(int)ShowMenuType.Sell].onClick.AddListener(SellShop);
        showMenuButton[(int)ShowMenuType.Exit].onClick.AddListener(ExitShop);
        //���� 3���� ���ְ� �Լ� 3���� public ���� �ٲ㼭 �¹�ư���� �ص���
        showMenuButton[(int)ShowMenuType.Buy].image.sprite = unSelectButton;
        showMenuButton[(int)ShowMenuType.Sell].image.sprite = unSelectButton;
        showMenuButton[(int)ShowMenuType.Exit].image.sprite = unSelectButton;
    }

    //0315 ��ȸ�޼��� ����
    public void SetTalkMessage(string msg)
    {
        talkText.text = msg;
    }
    //0315 �ʻ�ȭ �̹��� ����
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
            //�˾�â�� �߸� ó���� ������ ��ư�� ������ �ǰ� ��ư ����
            PopupShopMenuSelect(ShowMenuType.Buy);
            isOpenShowPopUp = true;

        }
        else
        {

            //NPCInteractive.instance.isAction = false;
            PotraitPanelOnOff(false);
            //btn.SetActive(_OnOff);
            shopChoice.SetActive(_OnOff);
            playerinput.OnEnable();
            isOpenShowPopUp = false;

        }


    }
    public void PopupShopMenuSelect(ShowMenuType type)
    {
        if (currentShowMenuType > ShowMenuType.Exit)
        {
            currentShowMenuType = ShowMenuType.Exit;

            return;
        }

        if (currentShowMenuType < ShowMenuType.Buy)
        {
            currentShowMenuType = ShowMenuType.Buy;

            return;
        }


        currentShowMenuType = type;
        //������ ���õǾ� �ִ� ��ư�� ������ ��� ����
        selectMenuAction = null;
        //���õ� ��ư�� ����Ÿ���� ��ư�� �����Ѵ� 
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
    public void OpenClosePanelInspectorQuests()
    {
        panelInspectorQuests.SetActive(!panelInspectorQuests.activeSelf);
    }

    public void AbrirCerrarPanelPersonajeQuests()
    {
        panelPersonajeQuests.SetActive(!panelPersonajeQuests.activeSelf);
    }
    private void Update()
    {
        ActualizarUIPersonaje();

        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            PopupShopMenuSelect(--currentShowMenuType);
        }

        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            //Debug.Log("Key Down");
            PopupShopMenuSelect(++currentShowMenuType);
            //Debug.Log("���� ���õ� �޴��� : " + currentShowMenuType);
        }

    }

    private void ActualizarUIPersonaje()
    {
        monedasTMP.text = MonedasManager.Instance.MonedasTotales.ToString();
    }
}
