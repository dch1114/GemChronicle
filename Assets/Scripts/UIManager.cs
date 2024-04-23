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

    public enum ShowMenuType  //���⽱�� �ϱ����� enum����
    {
        Buy,
        Exit,
        Max
    }

    public enum ShowHealType
    {
        Heal,
        NotHeal
    }

    public enum OpenMenuType
    { 
        Potal,
        Shop,
        Heal
    }
    public GameObject talkBtn;
    public GameObject potraitPanel;
    public GameObject shopPanel;
    public GameObject shopChoice;
    public GameObject HealChoice;
    [Header("UI Scripts")]
    public PlayerUI playerUI;
    public SkillPagesUI skillPages;
    public AlertPanelUI alertPanelUI;
    public DiePanelUI diePanelUI;
    public InventoryUIController inventoryUI;

    [Header("Potal")]
    public GameObject potalListUI;
    public PotalListUI potalUIScript;
    public GameObject potaltalk;
    public Button[] showMenuButton;
    public Button[] potalButton;
    public Button[] showHealButton;
    public GameObject Diary;
    public Text potalTxt;
    public Sprite selectButton;
    public Sprite unSelectButton;
    [SerializeField]
    ShowMenuType currentShowMenuType;
    ShowHealType currentShowHealType;
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

    //0315 [SerializeField]�� �����ϸ� �ܺ� ��ũ��Ʈ���� �����Ҽ� ������ �ν����Ϳ��� ���� �� Ȯ���� �� �� ����  
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
        InitializeShopMenuButtons();
        InitializeHealMenuButtons();


    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.UpArrow) && currentOpenMenuType.Equals(OpenMenuType.Shop)) PopupShopMenuSelect(--currentShowMenuType);

        if (Input.GetKeyUp(KeyCode.DownArrow) && currentOpenMenuType.Equals(OpenMenuType.Shop)) PopupShopMenuSelect(++currentShowMenuType);

        if (Input.GetKeyUp(KeyCode.UpArrow) && currentOpenMenuType.Equals(OpenMenuType.Heal)) PopupHealMenuSelect(--currentShowHealType);

        if (Input.GetKeyUp(KeyCode.DownArrow) && currentOpenMenuType.Equals(OpenMenuType.Heal)) PopupHealMenuSelect(++currentShowHealType);
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
        InitializeShopMenuButton(showMenuButton[(int)ShowMenuType.Exit], ExitShop, unSelectButton);
    }
    void InitializeHealMenuButtons()
    {
        // ��ư�� Ŭ�� �����ʸ� �Ҵ��ϰ� �̹��� ��������Ʈ�� �����մϴ�.
        InitializeHealMenuButton(showHealButton[(int)ShowHealType.Heal], Heal, unSelectButton);
        InitializeHealMenuButton(showHealButton[(int)ShowHealType.NotHeal], NotHeal, unSelectButton);
    }

    void InitializeShopMenuButton(Button button, UnityAction action, Sprite sprite)
    {
        button.onClick.AddListener(action); // Ŭ�� �̺�Ʈ�� �׼��� �߰��մϴ�.
        button.image.sprite = sprite; // �̹��� ��������Ʈ�� �����մϴ�.
    }
    void InitializeHealMenuButton(Button _button, UnityAction _action, Sprite _sprite)
    {
        _button.onClick.AddListener(_action); // Ŭ�� �̺�Ʈ�� �׼��� �߰��մϴ�.
        _button.image.sprite = _sprite; // �̹��� ��������Ʈ�� �����մϴ�.
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
            Debug.Log(i+"��° ��Ż ��� : "+ pArray[i].potal.isLock);
            //�ر��� ���� �ȵǾ��ٸ�
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
        talkBtn.SetActive(false);
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

            SetPlayerInput();
            playerinput.OnEnable();
            isOpenShowPopUp = false;
        }
    }
    public void HealChoiceOnOff(bool _OnOff)
    {
        talkBtn.SetActive(false);
        if (_OnOff)
        {
            currentOpenMenuType = OpenMenuType.Heal;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            //btn.SetActive(_OnOff);
            HealChoice.SetActive(_OnOff);
            //�˾�â�� �߸� ó���� ������ ��ư�� ������ �ǰ� ��ư ����
            PopupHealMenuSelect(ShowHealType.Heal);
            isOpenShowPopUp = true;

        }
        else
        {
            //NPCInteractive.instance.isAction = false;
            PotraitPanelOnOff(false);
            //btn.SetActive(_OnOff);
            HealChoice.SetActive(_OnOff);
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

    public void PopupHealMenuSelect(ShowHealType type)
    {
        if (currentShowHealType > ShowHealType.NotHeal)
        {
            currentShowHealType = ShowHealType.NotHeal;

            return;
        }

        if (currentShowHealType < ShowHealType.Heal)
        {
            currentShowHealType = ShowHealType.Heal;

            return;
        }

        currentShowHealType = type;

        selectMenuAction = null;

        selectMenuAction = GetSelectedHealMenu(type);
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
    UnityAction GetSelectedHealMenu(ShowHealType type)
    {
        showHealButton[(int)ShowHealType.Heal].image.sprite = unSelectButton;
        showHealButton[(int)ShowHealType.NotHeal].image.sprite = unSelectButton;

        switch (type)
        {
            case ShowHealType.Heal:
                showHealButton[(int)ShowHealType.Heal].image.sprite = selectButton;

                return Heal;


            case ShowHealType.NotHeal:
                showHealButton[(int)ShowHealType.NotHeal].image.sprite = selectButton;

                return NotHeal;
            default:
                return null;
        }
    }
    public void BuyShop()
    {
        inventoryUIController.UseShop();
        shopChoiceOnOff(false);
        Debug.Log("Select Buy");
    }

    public void Heal()
    {
        HealChoiceOnOff(false);
        GameManager.Instance.player.Data.StatusData.UseGold(100);
        GameManager.Instance.player.Data.StatusData.Hp = GameManager.Instance.player.Data.StatusData.MaxHp;
    }


    public void ExitShop()
    {
        inventoryUIController.CloseShop();
        shopChoiceOnOff(false);
        Debug.Log("Select Exi");

    }

    public void NotHeal()
    {
        HealChoiceOnOff(false);
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
        if (_page <= 6)
        {
            _page++;
            if(_page == 7)
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
        DiaryTxtArray = new string[7];
        DiaryTxtArray[0] = "-1��8��-\n\n���ڲ��� �¾�̴�.\n�ղ��� ���ڴ��� ���� ��ġ�� ���ð� �հ��� ��ġ��� \n���̶�� ���ϼ̴�.\n\n������ ���� �� ���� ������ ���� �� ���� ģ�� ģ������ �ð��.\n\n�׿��� [���ΰ�]�̶�� �̸��� �������.";
        DiaryTxtArray[1] = "-2��10��-\n\n��� �������� ���Ͱ� ����Ѵٴ� ��Ұ� ����ģ��.\n\n������ �ղ��� ������ ��å�� ������ �����Ŵ�.\n��ġ�Űɱ�?";
        DiaryTxtArray[2] = "-2��25��-\n\n�ֱ� �ղ��� ������ ���� ������Ŵٴ� ���� �޾Ҵ�.\n\n����� �ٿ� �ղ��� ��𰡽ô��� �˾ƺ��߰ڴ�.";
        DiaryTxtArray[3] = "-2��27��-\n\n�ղ��� ���Ϲ� ������ ���� ���ϰ����� �峪��Ŵٴ� \n���� �޾Ҵ�.\n\n�װ��� �ƹ� �͵� �����ٵ��� ���� �ѹ� ã�ư����߰ڴ�.";
        DiaryTxtArray[4] = "-3��27��-\n\n������  ��ҹ���  ��  ����� ���� ���͵��� �����ߴ�. \n\n�׵��� ������ ��ó�� ���������� ������ ���⸸�� �����־���.\n\n �Ⱬ�ߴ�.\n\n�װ��� �������� ������� ������  �����  �����  ���� �̾߱Ⱑ �����ߴ�.";
        DiaryTxtArray[5] = "\n\n���� ������ �ִ� ���̴�.\n\n�� ������ ������.\n\n�� ��� ����� �˰� �� ���� ���� ���� �ؿ��� �� �̻� ���� �� ������.";
        DiaryTxtArray[6] = "\n\n�����ƴ�.\n\n�������� �̷� ���� ��ƾ������� �� �� ������ ��Ե� ���� ���ƾ��Ѵ�.\n\n�ð��� �� ���� �ʾҴ�.";
        Diary.SetActive(_OnOff);
        _page = 0;
        ShowDiary(_page);

        SetPlayerInput();
        if (_OnOff == true)
        {
            playerinput.OnDisable();
        }
        else
        {
            playerinput.OnEnable();
        }
    }

    private void SetPlayerInput()
    {
        if (playerinput == null)
        {
            playerinput = GameManager.Instance.player.Input;
        }
    }
}
