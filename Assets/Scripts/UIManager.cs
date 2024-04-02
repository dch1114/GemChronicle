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

    public enum ShowMenuType  //보기쉽게 하기위해 enum선언
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
    public GameObject potaltalk;
    public Button[] showMenuButton;
    public Button[] potalButton;

    public Text potalTxt;
    public Sprite selectButton;
    public Sprite unSelectButton;
    [SerializeField]
    ShowMenuType currentShowMenuType;
    PotalType currentPotalType;
    OpenMenuType currentOpenMenuType;
    public Text talkBtnText;
    bool isOpenShowPopUp = false;
    bool isOpenPotalPopUp = false;
    public PlayerInput playerinput;
    UnityAction selectMenuAction = null;
    public GameObject soundSetting;
 
    //0315 [SerializeField]를 선언하면 외부 스크립트에서 접근할수 없으나 인스펙터에서 세팅 및 확인을 할 수 있음  
    [SerializeField]
    Text talkText;
    [SerializeField]
    Image npcPortraitImg;
    [SerializeField]
    Image playerPortraitImg;


    private Dictionary<PotalType, UnityAction> potalMenuAction = new Dictionary<PotalType, UnityAction>();


    protected override void Awake()
    {
        
    }

    private void Update()
    {
        UpdateUIPersonnel();

        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            if (currentOpenMenuType.Equals(OpenMenuType.Potal))
            {
                SelectPotalMenu(--currentPotalType);
            }
            else if (currentOpenMenuType.Equals(OpenMenuType.Shop))
            {
                PopupShopMenuSelect(--currentShowMenuType);
            }
           
        }

        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            if (currentOpenMenuType.Equals(OpenMenuType.Potal))
            {
                SelectPotalMenu(++currentPotalType);
            }
            else if (currentOpenMenuType.Equals(OpenMenuType.Shop))
            {
                PopupShopMenuSelect(++currentShowMenuType);
            }
        }
    }

    //void Awake()
    //{
    //    if (instance == null)
    //    {
    //        instance = this;
    //    }
    //    else
    //    {
    //        if (instance != this)
    //            Destroy(this.gameObject);
    //    }
    //}
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
    private void Start()
    {
        SetPotalDictionary();

        InitializeShopMenuButtons();
    }
    void SetPotalDictionary()
    {
        List<Potal> pList = PotalManager.Instance.GetPotalList();

        for (int i = 0; i < pList.Count && i < Enum.GetValues(typeof(PotalType)).Length; i++)
        {
            int index = i;
            potalMenuAction[(PotalType)index] = () => PotalManager.Instance.CheckUnLockPotal(pList[index]);
        }
    }

    void InitializeShopMenuButtons()
    {
        // 버튼에 클릭 리스너를 할당하고 이미지 스프라이트를 설정합니다.
        InitializeShopMenuButton(showMenuButton[(int)ShowMenuType.Buy], BuyShop, unSelectButton);
        InitializeShopMenuButton(showMenuButton[(int)ShowMenuType.Sell], SellShop, unSelectButton);
        InitializeShopMenuButton(showMenuButton[(int)ShowMenuType.Exit], ExitShop, unSelectButton);
        InitializeShopMenuButton(potalButton[(int)PotalType.A], potalMenuAction[PotalType.A], unSelectButton);
        InitializeShopMenuButton(potalButton[(int)PotalType.B], potalMenuAction[PotalType.B], unSelectButton);
        InitializeShopMenuButton(potalButton[(int)PotalType.C], potalMenuAction[PotalType.C], unSelectButton);
        InitializeShopMenuButton(potalButton[(int)PotalType.D], potalMenuAction[PotalType.D], unSelectButton);
    }

    void InitializeShopMenuButton(Button button, UnityAction action, Sprite sprite)
    {
        button.onClick.AddListener(action); // 클릭 이벤트에 액션을 추가합니다.
        button.image.sprite = sprite; // 이미지 스프라이트를 설정합니다.
    }


    //0315 대회메세지 세팅
    public void SetTalkMessage(string msg)
    {
        talkText.text = msg;
    }
    //0315 초상화 이미지 세팅
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
        //포탈 선택 UI 활성화
        potalListUI.SetActive(isVisible);


        //1.포탈매니저에서 언락이 되어있는 포탈타입만 확인하여 버튼으로 활성화하고 나머지는 비활성화

        //2.활성화 되어 있는 버튼들중 제일 앞에 있는(예:Type B,D라면 B Type)포탈 타입의 버튼이 선택되게 세팅(아래 SelectPotalMenu() 함수는 사용불가)

        //3.키보드 화살표 위, 아래로 선택시 활성화 된 버튼들 안에서 이동하기

        //4.selectMenuAction 이벤트 액션에 선택된 버튼의 메소드 등록



        //포탈 선택 UI가 활성화되면 PotalType.A 버튼이 선택된 상태로 시작
        SelectPotalMenu(isVisible ? PotalType.A : PotalType.D);

        isOpenPotalPopUp = isVisible;


    }

    private void SelectPotalMenu(PotalType type)
    {
        currentPotalType = (PotalType)Mathf.Clamp((int)type, (int)PotalType.A, (int)PotalType.D);
        //기존에 세팅되어 있는 버튼의 연결을 모두 해제
        selectMenuAction = null;
        selectMenuAction = GetSelectedPotalMenuAction(currentPotalType);

    }

    public void ExecuteSelectedPotalMenuAction()
    {
        selectMenuAction?.Invoke();
    }

    UnityAction GetSelectedPotalMenuAction(PotalType type)
    {
        foreach (var button in potalButton)
        {
            button.image.sprite = unSelectButton;
        }

        potalButton[(int)type].image.sprite = selectButton;


        // 딕셔너리에서 해당 PotalType에 대응하는 메소드를 반환합니다.
        if (potalMenuAction.TryGetValue(type, out UnityAction action))
        {
            return action;
        }

        return null; // 해당하는 PotalType에 대응하는 메소드가 없으면 null을 반환합니다.
    }
    //////////////////////////////////








    public void shopChoiceOnOff(bool _OnOff)
    {

        if (_OnOff)
        {
            currentOpenMenuType = OpenMenuType.Shop;
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
        //기존에 세팅되어 있는 버튼의 연결을 모두 해제
        selectMenuAction = null;
        //선택될 버튼에 상점타입의 버튼을 연결한다 
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
