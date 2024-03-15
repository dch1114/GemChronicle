using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Pool;
using UnityEngine.UI;

public class PlayerAction : MonoBehaviour
{

    public GameManager gameManager;
   
    GameObject scanObject;
    public GameObject talkBtn;

    private PlayerInputActions inputActions;



    // Update is called once per frame
    void Update()
    {


        if (Keyboard.current.tabKey.wasPressedThisFrame&& scanObject != null)
        {
            

            if (scanObject.layer == LayerMask.NameToLayer("NPC"))
            {
                NPCInteractive.instance.touch(scanObject);
            }
            if (scanObject.layer == LayerMask.NameToLayer("Shop"))
            {
               
                NPCInteractive.instance.touch(scanObject);
                

            }
        }





    }


    /// Istrigger가 켜져있는 콜라이더가 겹치는 곳의 npc 정보를 가져옴
    private void OnTriggerEnter2D(Collider2D _other)
    {

        if (_other != null)
        {
            if (_other.gameObject.layer == LayerMask.NameToLayer("NPC"))
            {

                scanObject = _other.gameObject;
                UIManager.instance.talkBtnOnOff(true);

            }
            if (_other.gameObject.layer == LayerMask.NameToLayer("Shop"))
            {
                scanObject = _other.gameObject;
                UIManager.instance.shopPanelOnOff(true);
            }
        }
        else
        {
            scanObject = null;
            talkBtn = null;
            
        }
        
    }


    /// 아래 부분이 없으면 NPC와 떨어지더라도 가장 최근 접촉한 NPC와 계속 대화함
    private void OnTriggerExit2D(Collider2D _other)
    {
        if (_other != null)
        {
            if (_other.gameObject.layer == LayerMask.NameToLayer("NPC"))
            {

                scanObject = null;
                UIManager.instance.talkBtnOnOff(false);
            }
            if (_other.gameObject.layer == LayerMask.NameToLayer("Shop"))
            {
                scanObject = null;
                UIManager.instance.shopPanelOnOff(false);
            }
        }
        else
        {
            talkBtn = null;
            scanObject = null;
        }
            
    }


}


