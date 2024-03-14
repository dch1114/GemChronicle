using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject[] menuItems;
    private int selectedItemIndex = 0;

    // ���õ� �޴� �������� ���� �����ϰ� Ŀ�� ������
    private Vector3 originalScale = Vector3.one;
    private Vector3 enlargedScale = new Vector3(1.1f, 1.1f, 1.1f);

    // ���� �Է��� ���� �� �ִ� ������ �ð�
    public float inputDelay = 0.2f;
    private float lastInputTime;

    void Start()
    {
        // ������ �� ���� �������� ����
        originalScale = menuItems[selectedItemIndex].transform.localScale;
        lastInputTime = Time.time;
    }

    void Update()
    {
        // �Է� �����̸� üũ�Ͽ� ���� �ð��� ������ ���� �Է��� ����
        if (Time.time - lastInputTime < inputDelay)
            return;

        // ���� �� ���� �Է� ����
        float verticalInput = Input.GetAxis("Vertical");

        // ����Ű�� �޴� ������ ����
        if (verticalInput > 0)
        {
            SelectPreviousItem();
            lastInputTime = Time.time; // �Է��� �����Ǹ� ������ �Է� �ð� ����
        }
        else if (verticalInput < 0)
        {
            SelectNextItem();
            lastInputTime = Time.time; // �Է��� �����Ǹ� ������ �Է� �ð� ����
        }

        // �� Ű�� �޴� ������ ����
        if (Input.GetKeyUp(KeyCode.Space)) 
        {
            ExecuteSelectedItem();
            lastInputTime = Time.time; // �Է��� �����Ǹ� ������ �Է� �ð� ����
        }
    }

    void SelectNextItem()
    {
        // ������ ���õ� �������� �������� ������� ����
        menuItems[selectedItemIndex].transform.localScale = originalScale;

        selectedItemIndex = (selectedItemIndex + 1) % menuItems.Length;
        // ���õ� �������� �������� Ŀ�� ũ��� ����
        menuItems[selectedItemIndex].transform.localScale = enlargedScale;
    }

    void SelectPreviousItem()
    {
        // ������ ���õ� �������� �������� ������� ����
        menuItems[selectedItemIndex].transform.localScale = originalScale;

        selectedItemIndex = (selectedItemIndex - 1 + menuItems.Length) % menuItems.Length;
        // ���õ� �������� �������� Ŀ�� ũ��� ����
        menuItems[selectedItemIndex].transform.localScale = enlargedScale;
    }

    void ExecuteSelectedItem()
    {
        // ���õ� �޴� ������ ����
        Debug.Log("Executing Selected Item: " + menuItems[selectedItemIndex].name);
    }
}