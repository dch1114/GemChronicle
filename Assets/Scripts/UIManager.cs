using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System;
using TMPro;

public class UIManager : Singleton<UIManager>
{

    [Header("Bar")]
    [SerializeField] private Image expPlayer;

    [Header("Texto")]
    [SerializeField] public TextMeshProUGUI GoldTMP;
    [SerializeField] public TextMeshProUGUI expTMP;
    [SerializeField] public TextMeshProUGUI levelTMP;

    [Header("Paneles")]
    [SerializeField] private GameObject panelInspectorQuests;
    [SerializeField] private GameObject panelPersonajeQuests;

    private float expActual;
    private float NewLevel;

    public int Level { get; private set; }

    public enum ShowMenuType  //���⽱�� �ϱ����� enum����
    {
        Buy,
        Sell,
        Exit,
        Max
    }
    public enum OpenMenuType
    { 
        Potal,
        Shop
    }
    public GameObject talkBtn;
    public GameObject potraitPanel;
    public GameObject shopPanel;
    public GameObject shopChoice;

    public GameObject potalListUI;
    //public static UIManager instance = null;
    public PlayerController playerController;
    public PotalListUI potalUIScript;
    public GameObject potaltalk;
    public Button[] showMenuButton;
    public Button[] potalButton;

    public Text potalTxt;
    public Sprite selectButton;
    public Sprite unSelectButton;
    [SerializeField]
    ShowMenuType currentShowMenuType;
    PotalType currentPotalType;
    PotalType minPotalType;
    PotalType maxPotalType;

    OpenMenuType currentOpenMenuType;
    public Text talkBtnText;
    bool isOpenShowPopUp = false;
    bool isOpenPotalPopUp = false;
    private PlayerInput playerinput;
    UnityAction selectMenuAction = null;
    public GameObject soundSetting;
 
    //0315 [SerializeField]�� �����ϸ� �ܺ� ��ũ��Ʈ���� �����Ҽ� ������ �ν����Ϳ��� ���� �� Ȯ���� �� �� ����  
    [SerializeField]
    Text talkText;
    [SerializeField]
    Image npcPortraitImg;
    [SerializeField]
    Image playerPortraitImg;


    protected override void Awake()
    {
        playerinput = GameManager.Instance.player.Input;
    }
    private void Start()
    {
        InitializeShopMenuButtons();
    }
    private void Update()
    {
        UpdateUIPersonnel();

        if (Input.GetKeyUp(KeyCode.UpArrow) && currentOpenMenuType.Equals(OpenMenuType.Shop)) PopupShopMenuSelect(--currentShowMenuType);

        if (Input.GetKeyUp(KeyCode.DownArrow) && currentOpenMenuType.Equals(OpenMenuType.Shop)) PopupShopMenuSelect(++currentShowMenuType);
    }

    public void OpenSoundSet(bool _OnOff)
    {
        soundSetting.SetActive(_OnOff);
    }
    public bool IsOpenShowPopup()
    {
        return isOpenShowPopUp;
    }
    public bool IsOpenPotalPopup()
    {
        return isOpenPotalPopUp;
    }

    void InitializeShopMenuButtons()
    {
        // ��ư�� Ŭ�� �����ʸ� �Ҵ��ϰ� �̹��� ��������Ʈ�� �����մϴ�.
        InitializeShopMenuButton(showMenuButton[(int)ShowMenuType.Buy], BuyShop, unSelectButton);
        InitializeShopMenuButton(showMenuButton[(int)ShowMenuType.Sell], SellShop, unSelectButton);
        InitializeShopMenuButton(showMenuButton[(int)ShowMenuType.Exit], ExitShop, unSelectButton);
    }

    void InitializeShopMenuButton(Button button, UnityAction action, Sprite sprite)
    {
        button.onClick.AddListener(action); // Ŭ�� �̺�Ʈ�� �׼��� �߰��մϴ�.
        button.image.sprite = sprite; // �̹��� ��������Ʈ�� �����մϴ�.
    }


    //0315 ��ȸ�޼��� ����
    public void SetTalkMessage(string msg)
    {
        talkText.text = msg;
    }
    //0315 �ʻ�ȭ �̹��� ����
    public void SetNpcPortraitImage(Sprite sp)
    {
        npcPortraitImg.sprite = sp;
    }
    public void SetPlayerPortraitImage(Sprite sp)
    {
        playerPortraitImg.sprite = sp;
    }
    public void talkBtnOnOff(bool _OnOff)
    {
        talkBtn.SetActive(_OnOff);
    }

    public void PotraitPanelOnOff(bool _OnOff)
    {
        potraitPanel.SetActive(_OnOff);
    }
    public void ShowNpcPotrait(bool _OnOff)
    {
        npcPortraitImg.enabled = _OnOff;
    }
    public void ShowPlayerPotrait(bool _OnOff)
    {
        playerPortraitImg.enabled = _OnOff;
    }
    public void shopPanelOnOff(bool _OnOff)
    {
        shopPanel.SetActive(_OnOff);
    }


    ///////////////////////////////

    public void TogglePortalUI(bool isVisible)
    {
        //Cursor.lockState = isVisible ? CursorLockMode.None : CursorLockMode.Locked;
        //Cursor.visible = isVisible;
        currentOpenMenuType = OpenMenuType.Potal;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        //��Ż ���� UI Ȱ��ȭ
        potalListUI.SetActive(isVisible);

        //1.��Ż�Ŵ������� ����� �Ǿ��ִ� ��ŻŸ�Ը� Ȯ���Ͽ� ��ư���� Ȱ��ȭ�ϰ� �������� ��Ȱ��ȭ
        var pArray = PotalManager.Instance.GetPotal();

        for (int i = 0; i < pArray.Length; i++)
        {
            if (pArray[i].potal.isLock == false)
            {
                potalUIScript.ShowButton(i);
            }
            else
            {
                potalUIScript.HideButton(i);
            }
        }

        potalUIScript.ButtonSet();

        isOpenPotalPopUp = isVisible;

    }


    public void shopChoiceOnOff(bool _OnOff)
    {

        if (_OnOff)
        {
            currentOpenMenuType = OpenMenuType.Shop;
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

    UnityAction GetSelectedShopMenu(ShowMenuType type)
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

    public void OpenClosePersonalityPanelQuests()
    {
        panelPersonajeQuests.SetActive(!panelPersonajeQuests.activeSelf);
    }



    private void UpdateUIPersonnel()
    {
        expPlayer.fillAmount = Mathf.Lerp(expPlayer.fillAmount,
            expActual / NewLevel, 10f * Time.deltaTime);

        expTMP.text = $"{((expActual / NewLevel) * 100):F2}%";
        levelTMP.text = $"Level {Level}";
      
    }

    public void UpdateExpPersonality(float pExpActul, float pExpRequired, int pLevel)
    {
        expActual = pExpActul;
        NewLevel = pExpRequired;
        Level = pLevel;
    }
    
    public void PotalTalk(bool _OnOff)
    {
        potaltalk.SetActive(_OnOff);
    }

}
