using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject[] cameras; // �� ����� ī�޶� �迭�� ����
    private int currentCameraIndex = 0; // ���� ��� ���� ī�޶��� �ε����� ����

    private void Start()
    {
        // ���� ���� �� ù ��° ī�޶� Ȱ��ȭ
        ActivateCurrentCamera();
    }

    // ��Ż�� ���� ���� ������ �̵��� �� ȣ��Ǵ� �Լ�
    public void ChangeMap(int newCameraIndex)
    {
        // ���� ��� ���� ī�޶� ��Ȱ��ȭ
        DeactivateCurrentCamera();

        // ���ο� �ʿ� �ش��ϴ� ī�޶� Ȱ��ȭ
        currentCameraIndex = newCameraIndex;
        ActivateCurrentCamera();
    }

    // ���� ��� ���� ī�޶� Ȱ��ȭ�ϴ� �Լ�
    private void ActivateCurrentCamera()
    {
        if (currentCameraIndex >= 0 && currentCameraIndex < cameras.Length)
        {
            cameras[currentCameraIndex].SetActive(true);
        }
        else
        {
            Debug.LogError("Invalid camera index: " + currentCameraIndex);
        }
    }

    // ���� ��� ���� ī�޶� ��Ȱ��ȭ�ϴ� �Լ�
    private void DeactivateCurrentCamera()
    {
        if (currentCameraIndex >= 0 && currentCameraIndex < cameras.Length)
        {
            cameras[currentCameraIndex].SetActive(false);
        }
        else
        {
            Debug.LogError("Invalid camera index: " + currentCameraIndex);
        }
    }
}
