using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class QuestMain : MonoBehaviour
{
    public UIQuestDirector director;

    void Start()
    {
        //DataViewManager.instance.LoadQuestData();

        this.director.Init();
    }
}
