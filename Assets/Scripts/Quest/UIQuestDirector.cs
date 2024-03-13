using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIQuestDirector : MonoBehaviour
{
    public UIQuestScrollView scrollview;

    public void Init()
    {
        this.scrollview.Init();
    }
}
