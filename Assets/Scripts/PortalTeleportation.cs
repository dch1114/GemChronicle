using UnityEngine;

public class PortalTeleportation : MonoBehaviour
{
    public GameObject[] oldVirtualCameras; // 이동 전 가상 카메라 배열
    public GameObject[] newVirtualCameras; // 이동 후 가상 카메라 배열

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 이동할 때 기존 가상 카메라를 비활성화하고 새로운 장소의 가상 카메라를 활성화
            DisableOldVirtualCameras();
            EnableNewVirtualCameras();
        }
    }

    private void DisableOldVirtualCameras()
    {
        foreach (GameObject camera in oldVirtualCameras)
        {
            camera.SetActive(false);
        }
    }

    private void EnableNewVirtualCameras()
    {
        foreach (GameObject camera in newVirtualCameras)
        {
            camera.SetActive(true);
        }
    }
}