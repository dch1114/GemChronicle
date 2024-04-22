using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System;
using TMPro;

public class UIManager : Singleton<UIManager>
{
    [Header("Paneles")]
    [SerializeField] private GameObject panelQuest;

    [SerializeField] private GameObject panelCompleteQuest;

    private float expActual;
    private float NewLevel;

    public int Level { get; private set; }

    public enum ShowMenuType  //보기쉽게 하기위해 enum선언
    {
        Buy,
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

    [Header("UI Scripts")]
    public PlayerUI playerUI;
    public SkillPagesUI skillPages;
    public AlertPanelUI alertPanelUI;

    public GameObject potalListUI;
    public PotalListUI potalUIScript;
    public GameObject potaltalk;
    public Button[] showMenuButton;
    public Button[] potalButton;
    public GameObject Diary;
    public Text potalTxt;
    public Sprite selectButton;
    public Sprite unSelectButton;
    [SerializeField]
    ShowMenuType currentShowMenuType;
    PotalType currentPotalType;
    PotalType minPotalType;
    PotalType maxPotalType;

    OpenMenuType currentOpenMenuType;
    public TextMeshProUGUI talkBtnText;
    bool isOpenShowPopUp = false;
    bool isOpenPotalPopUp = false;
    private PlayerInput playerinput;
    UnityAction selectMenuAction = null;
    public GameObject soundSetting;
    public TextMeshProUGUI DiaryTxt;
    public string[] DiaryTxtArray;
    public int _page;

    //0315 [SerializeField]를 선언하면 외부 스크립트에서 접근할수 없으나 인스펙터에서 세팅 및 확인을 할 수 있음  
    [SerializeField]
    TextMeshProUGUI talkText;
    [SerializeField]
    Image npcPortraitImg;
    [SerializeField]
    Image playerPortraitImg;

    public InventoryUIController inventoryUIController;

    protected override void Awake()
    {

   

    }
    private void Start()
    {
        playerinput = GameManager.Instance.player.Input;
        InitializeShopMenuButtons();

    }
    private void Update()
    {
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
        // 버튼에 클릭 리스너를 할당하고 이미지 스프라이트를 설정합니다.
        InitializeShopMenuButton(showMenuButton[(int)ShowMenuType.Buy], BuyShop, unSelectButton);
        InitializeShopMenuButton(showMenuButton[(int)ShowMenuType.Exit], ExitShop, unSelectButton);
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
        var pArray = PotalManager.Instance.GetPotal();

        for (int i = 0; i < pArray.Length; i++)
        {
            Debug.Log(i+"번째 포탈 잠김 : "+ pArray[i].potal.isLock);
            //해금이 아직 안되었다면
            //if (pArray[i].potal.isLock)
            //{
            //    potalUIScript.HideButton(i);
            //}
            //else
            //{
            //    potalUIScript.ShowButton(i);
            //}
            if (!pArray[i].potal.isLock)
            {
                potalUIScript.ShowButton(i);
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
        showMenuButton[(int)ShowMenuType.Exit].image.sprite = unSelectButton;

        switch (type)
        {
            case ShowMenuType.Buy:
                showMenuButton[(int)ShowMenuType.Buy].image.sprite = selectButton;

                return BuyShop;


            case ShowMenuType.Exit:
                showMenuButton[(int)ShowMenuType.Exit].image.sprite = selectButton;

                return ExitShop;
            default:
                return null;
        }
    }
    void BuyShop()
    {
        inventoryUIController.UseShop();
        shopChoiceOnOff(false);
        Debug.Log("Select Buy");
    }



    void ExitShop()
    {
        inventoryUIController.CloseShop();
        shopChoiceOnOff(false);
        Debug.Log("Select Exi");

    }
    bool bOpenQuestPanel = false;
    public void TogglePanelQuest()
    {

        if (!bOpenQuestPanel)
        {
            bOpenQuestPanel = true;
            panelQuest.SetActive(true);

        }
        else
        {
            bOpenQuestPanel = false;
            panelQuest.SetActive(false);

        }
    }

    public void OpenClosePanelCompleteQuest()
    {
        panelCompleteQuest.SetActive(!panelCompleteQuest.activeSelf);
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

    public void ShowDiary(int _page)
    {
        
        DiaryTxt.text = DiaryTxtArray[_page];
        return;
    }

    public void NextPage()
    {
        if (_page <= 5)
        {
            _page++;
            if(_page == 6)
            {
                OnOffDiary(false);
              
            }
            DiaryTxt.text = DiaryTxtArray[_page];
        }
       
        return;
    }
    public void PrevPage()
    {
        if (_page >= 1)
        {
            _page--;
            DiaryTxt.text = DiaryTxt.text = DiaryTxtArray[_page];
        }
   
        return;
    }

    public void OnOffDiary(bool _OnOff)
    {
        DiaryTxtArray = new string[56];
        DiaryTxtArray[0] = "왕자께서 태어나셨다. 왕께선 왕자님의 마력 수치를 보시고선 왕가의 수치라며 죽이라고 명하셨다. 하지만 나는 그 명을 따를수 없어 내 가장 친한 친구에게 맡겼다. 그에게 [주인공]이라는 이름을 지어줬다.";
        DiaryTxtArray[1] = "-중략-\r\n\r\n모든 지역에서 몬스터가 출몰한다는 상소가 빗발친다. 하지만 왕께선 마땅한 대책을 내놓지 않으신다. 지치신걸까?";
        DiaryTxtArray[2] = "(15일뒤)\r\n\r\n최근 왕께서 새벽에 자주 사라지신다는 보고를 받았다. 사람을 붙여 왕께서 어디가시는지 알아봐야겠다.";
        DiaryTxtArray[3] = "(2일뒤)\r\n\r\n왕께서 매일밤 오래전 폐쇄된 지하감옥에 드나드신다는 보고를 받았다. 그곳엔 아무 것도 없을텐데… 내일 한번 찾아가봐야겠다.";
        DiaryTxtArray[4] = "(한달뒤)감옥엔 상소문들 속 모습과 같은 몬스터들이 가득했다. 그들의 몸에는 상처가 가득했지만 눈에는 광기만이 남아있었다. 기괴했다. 그곳의 보고서에는 ‘어둠의 젬’을 만드는 방법에 대한 이야기가 가득했다.";
        DiaryTxtArray[5] = "왕이 만들어내고 있었던 것이다. 그 수많은 고통을. 이 모든 사실을 알게 된 나는 차마 왕의 밑에서 더 이상 일할 수 없었다. 도망쳤다. 언제까지 이런 삶을 살아야할지는 알 수 없지만 어떻게든 왕을 막아야한다. 시간이 얼마 남지 않았다.";
        Diary.SetActive(_OnOff);
        _page = 0;
        ShowDiary(_page);
    }
}
