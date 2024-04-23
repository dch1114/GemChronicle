using UnityEngine;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{
    public GameObject go;

    public void Start()
    {
        if (SceneManager.GetActiveScene().name == "Ending")
        {
            EndingCredit();
        }
    }

    public void EndingCredit()
    {
        go.SetActive(true);
    }

    public void LoadStartScene()
    {
        SceneManager.LoadScene("StartScene");
    }
}
