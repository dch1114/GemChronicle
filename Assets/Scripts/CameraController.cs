using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // �� ��ҿ� ���� �ó׸ӽ� Virtual Camera �迭�� �����մϴ�.
    public CinemachineVirtualCameraBase[] cameras;

    private CinemachineVirtualCameraBase currentCamera;

    private void Start()
    {
        // ���� ���� �� ��� ī�޶� ��Ȱ��ȭ�մϴ�.
        DeactivateAllCameras();
    }

    private void OnTriggerEnter(Collider other)
    {
        // �÷��̾ �ٸ� ��ҷ� �̵��� �� ȣ��Ǵ� �Լ��Դϴ�.
        if (other.CompareTag("Player"))
        {
            // ���ο� ��ҿ� �´� Virtual Camera�� Ȱ��ȭ�մϴ�.
            ActivateCameraInCurrentArea();
        }
    }

    private void ActivateCameraInCurrentArea()
    {
        // ���� Ȱ��ȭ�� ī�޶� ��Ȱ��ȭ�մϴ�.
        if (currentCamera != null)
        {
            currentCamera.gameObject.SetActive(false);
        }

        // ���ο� ����� Virtual Camera�� Ȱ��ȭ�մϴ�.
        if (cameras.Length > 0)
        {
            currentCamera = cameras[Random.Range(0, cameras.Length)]; // ������ ī�޶� ���� ����
            currentCamera.gameObject.SetActive(true);
        }
    }

    private void DeactivateAllCameras()
    {
        // ��� ī�޶� ��Ȱ��ȭ�մϴ�.
        foreach (CinemachineVirtualCameraBase camera in cameras)
        {
            camera.gameObject.SetActive(false);
        }
    }
}