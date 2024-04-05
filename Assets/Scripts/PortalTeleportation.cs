using UnityEngine;

public class PortalTeleportation : MonoBehaviour
{
    public GameObject[] oldVirtualCameras; // �̵� �� ���� ī�޶� �迭
    public GameObject[] newVirtualCameras; // �̵� �� ���� ī�޶� �迭

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // �̵��� �� ���� ���� ī�޶� ��Ȱ��ȭ�ϰ� ���ο� ����� ���� ī�޶� Ȱ��ȭ
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