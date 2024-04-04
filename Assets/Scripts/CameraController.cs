using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // 각 장소에 대한 시네머신 Virtual Camera 배열을 저장합니다.
    public CinemachineVirtualCameraBase[] cameras;

    private CinemachineVirtualCameraBase currentCamera;

    private void Start()
    {
        // 게임 시작 시 모든 카메라를 비활성화합니다.
        DeactivateAllCameras();
    }

    private void OnTriggerEnter(Collider other)
    {
        // 플레이어가 다른 장소로 이동할 때 호출되는 함수입니다.
        if (other.CompareTag("Player"))
        {
            // 새로운 장소에 맞는 Virtual Camera를 활성화합니다.
            ActivateCameraInCurrentArea();
        }
    }

    private void ActivateCameraInCurrentArea()
    {
        // 현재 활성화된 카메라를 비활성화합니다.
        if (currentCamera != null)
        {
            currentCamera.gameObject.SetActive(false);
        }

        // 새로운 장소의 Virtual Camera를 활성화합니다.
        if (cameras.Length > 0)
        {
            currentCamera = cameras[Random.Range(0, cameras.Length)]; // 임의의 카메라 선택 예시
            currentCamera.gameObject.SetActive(true);
        }
    }

    private void DeactivateAllCameras()
    {
        // 모든 카메라를 비활성화합니다.
        foreach (CinemachineVirtualCameraBase camera in cameras)
        {
            camera.gameObject.SetActive(false);
        }
    }
}