using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject[] cameras; // 각 장소의 카메라를 배열로 저장
    private int currentCameraIndex = 0; // 현재 사용 중인 카메라의 인덱스를 추적

    private void Start()
    {
        // 게임 시작 시 첫 번째 카메라를 활성화
        ActivateCurrentCamera();
    }

    // 포탈을 통해 다음 맵으로 이동할 때 호출되는 함수
    public void ChangeMap(int newCameraIndex)
    {
        // 현재 사용 중인 카메라를 비활성화
        DeactivateCurrentCamera();

        // 새로운 맵에 해당하는 카메라를 활성화
        currentCameraIndex = newCameraIndex;
        ActivateCurrentCamera();
    }

    // 현재 사용 중인 카메라를 활성화하는 함수
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

    // 현재 사용 중인 카메라를 비활성화하는 함수
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
