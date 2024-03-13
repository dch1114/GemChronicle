using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

public class PlayerAction : MonoBehaviour
{
    public float speed;


    Rigidbody2D rigid;
    float h;
    float v;

    public GameManager gameManager;
    Vector3 dirVec;
    bool isHorizonMove;
    GameObject scanObject;
    public GameObject talkBtn;
    
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        //간단한 이동식
        h = NPCInteractive.instance.isAction ? 0 : Input.GetAxisRaw("Horizontal");  //액션상태에 따라 움직이지 못하게
        v = NPCInteractive.instance.isAction ? 0 : Input.GetAxisRaw("Vertical");

        bool hDown = NPCInteractive.instance.isAction ? false : Input.GetButtonDown("Horizontal");
        bool vDown = NPCInteractive.instance.isAction ? false : Input.GetButtonDown("Vertical");
        bool hUp = NPCInteractive.instance.isAction ? false : Input.GetButtonUp("Horizontal");
        bool vUp = NPCInteractive.instance.isAction ? false : Input.GetButtonUp("Vertical");

        if (hDown || vUp)
            isHorizonMove = true;
        else if (vDown || hUp)
            isHorizonMove = false;
     


        //보는 방향 설정
        if (vDown && v == 1)
            dirVec = Vector3.up;
        //else if (vDown && v == -1)
        //    dirVec = Vector3.down;
        else if (hDown && h == -1)
            dirVec = Vector3.left;
         else if (hDown && h == 1)
            dirVec = Vector3.right;

        //scanObject
        if (Input.GetButtonDown("Jump") && scanObject != null)
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

    void FixedUpdate()
    {
        //수평이동
        Vector2 moveVec = isHorizonMove ? new Vector2(h, 0) : new Vector2(0, v);
        rigid.velocity = moveVec * speed;

       
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
                scanObject = _other.gameObject;
                UIManager.instance.shopPanelOnOff(false);
            }
        }
        else
        {
            talkBtn = null;
            scanObject = null;
        }
            
    }

    public void discrimination()
    {

    }

}


