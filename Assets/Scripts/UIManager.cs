using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject talkBtn;
    public GameObject talkPanel;
    public GameObject shopPanel;
    public GameObject shopChoice;
    public static UIManager instance = null;
    public GameObject btn;
    void Awake()
    {


        if (instance == null)
        {
            instance = this;
           
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

    public void shopPanelOnOff(bool _OnOff)
    {
        shopPanel.SetActive(_OnOff);
    }

    public void shopChoiceOnOff(bool _OnOff)
    {
      
        if (_OnOff)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            btn.SetActive(_OnOff);
            shopChoice.SetActive(_OnOff);
            
        }
           
            else
        {
            
            //NPCInteractive.instance.isAction = false;
            talkPanelOnOff(false);
            btn.SetActive(_OnOff);
            shopChoice.SetActive(_OnOff);
          
        }
            

    }

}
