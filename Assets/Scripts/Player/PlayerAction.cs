using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

        //������ �̵���
        h = gameManager.isAction ? 0 : Input.GetAxisRaw("Horizontal");  //�׼ǻ��¿� ���� �������� ���ϰ�
        v = gameManager.isAction ? 0 : Input.GetAxisRaw("Vertical");

        bool hDown = gameManager.isAction ? false : Input.GetButtonDown("Horizontal");
        bool vDown = gameManager.isAction ? false : Input.GetButtonDown("Vertical");
        bool hUp = gameManager.isAction ? false : Input.GetButtonUp("Horizontal");
        bool vUp = gameManager.isAction ? false : Input.GetButtonUp("Vertical");

        if (hDown || vUp)
            isHorizonMove = true;
        else if (vDown || hUp)
            isHorizonMove = false;
     


        //���� ���� ����
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
            gameManager.Action(scanObject);





    }

    void FixedUpdate()
    {
        //�����̵�
        Vector2 moveVec = isHorizonMove ? new Vector2(h, 0) : new Vector2(0, v);
        rigid.velocity = moveVec * speed;

       
    }

    /// Istrigger�� �����ִ� �ݶ��̴��� ��ġ�� ���� npc ������ ������
    private void OnTriggerEnter2D(Collider2D _other)
    {

        if (_other != null)
        {
            if (_other.gameObject.layer == LayerMask.NameToLayer("Object"))
            {
                scanObject = _other.gameObject;
                talkBtn.SetActive(true);
            }
        }
        else
        {
            scanObject = null;
            talkBtn = null;
        }
            
    }


    /// �Ʒ� �κ��� ������ NPC�� ���������� ���� �ֱ� ������ NPC�� ��� ��ȭ��
    private void OnTriggerExit2D(Collider2D _other)
    {
        if (_other != null)
        {
            if (_other.gameObject.layer == LayerMask.NameToLayer("Object"))
            {
                scanObject = null;
                talkBtn.SetActive(false);
            }
        }
        else
        {
            talkBtn = null;
            scanObject = null;
        }
            
    }


}


////���� ����+ �Ÿ�Ȯ��
//Debug.DrawRay(rigid.position, dirVec * 1.7f, new Color(0, 1, 0));

////��ȭ�ɱ�
//RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, dirVec, 1.7f, LayerMask.GetMask("Object"));

//if (rayHit.collider != null)
//{
//    scanObject = rayHit.collider.gameObject;
//}
//else
//    scanObject = null;