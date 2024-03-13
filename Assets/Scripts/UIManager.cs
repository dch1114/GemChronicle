using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject talkBtn;
    public GameObject talkPanel;
    public static UIManager instance = null;
    void Awake()
    {


        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance != this)
                Destroy(this.gameObject);
        }

    }

    public void talkBtnOnOff(bool _OnOff)
    {
        talkBtn.SetActive(_OnOff);
    }

    public void talkPanelOnOff(bool _OnOff)
    {
        talkPanel.SetActive(_OnOff);
    }
}
