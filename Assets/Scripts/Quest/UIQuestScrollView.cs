using UnityEngine;

public class UIQuestScrollView : MonoBehaviour
{
    public GameObject cellviewPrefab;
    public Transform contentTrans;

    public void Init()
    {
        //test
        for(int i = 0; i < 5; i++)
        {
            Instantiate(this.cellviewPrefab, this.contentTrans);
        }
    }
}