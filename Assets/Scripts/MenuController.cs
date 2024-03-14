using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject[] menuItems;
    private int selectedItemIndex = 0;

    // 선택된 메뉴 아이템의 원래 스케일과 커진 스케일
    private Vector3 originalScale = Vector3.one;
    private Vector3 enlargedScale = new Vector3(1.1f, 1.1f, 1.1f);

    // 다음 입력을 받을 수 있는 딜레이 시간
    public float inputDelay = 0.2f;
    private float lastInputTime;

    void Start()
    {
        // 시작할 때 원래 스케일을 저장
        originalScale = menuItems[selectedItemIndex].transform.localScale;
        lastInputTime = Time.time;
    }

    void Update()
    {
        // 입력 딜레이를 체크하여 일정 시간이 지나야 다음 입력을 받음
        if (Time.time - lastInputTime < inputDelay)
            return;

        // 수평 및 수직 입력 감지
        float verticalInput = Input.GetAxis("Vertical");

        // 방향키로 메뉴 아이템 선택
        if (verticalInput > 0)
        {
            SelectPreviousItem();
            lastInputTime = Time.time; // 입력이 감지되면 마지막 입력 시간 갱신
        }
        else if (verticalInput < 0)
        {
            SelectNextItem();
            lastInputTime = Time.time; // 입력이 감지되면 마지막 입력 시간 갱신
        }

        // 탭 키로 메뉴 아이템 선택
        if (Input.GetKeyUp(KeyCode.Space)) 
        {
            ExecuteSelectedItem();
            lastInputTime = Time.time; // 입력이 감지되면 마지막 입력 시간 갱신
        }
    }

    void SelectNextItem()
    {
        // 이전에 선택된 아이템의 스케일을 원래대로 변경
        menuItems[selectedItemIndex].transform.localScale = originalScale;

        selectedItemIndex = (selectedItemIndex + 1) % menuItems.Length;
        // 선택된 아이템의 스케일을 커진 크기로 변경
        menuItems[selectedItemIndex].transform.localScale = enlargedScale;
    }

    void SelectPreviousItem()
    {
        // 이전에 선택된 아이템의 스케일을 원래대로 변경
        menuItems[selectedItemIndex].transform.localScale = originalScale;

        selectedItemIndex = (selectedItemIndex - 1 + menuItems.Length) % menuItems.Length;
        // 선택된 아이템의 스케일을 커진 크기로 변경
        menuItems[selectedItemIndex].transform.localScale = enlargedScale;
    }

    void ExecuteSelectedItem()
    {
        // 선택된 메뉴 아이템 실행
        Debug.Log("Executing Selected Item: " + menuItems[selectedItemIndex].name);
    }
}