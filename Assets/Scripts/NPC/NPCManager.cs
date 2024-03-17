using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public static NPCManager instance = null;
    public TalkManager talkManager;
    public PlayerInput playerinput;
    //���Ǿ� ���ӿ�����Ʈ�� �� ������ ����Ʈ
    public List<GameObject> npcList;
    //���Ǿ��� ������ ��ġ ����Ʈ
    public List<Transform> npcPosList;

    public List<int> npcId;

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
    void Start()
    {
        //�������� ���ӿ�����Ʈ�� �����ϰ� ��ġ�� �������ش�
       
        MakeNpcGameObject();

    }

   

    void MakeNpcGameObject()
    {
        Debug.Log("1");
        for (int i = 0; i < npcList.Count; i++)
        {

         
            //����Ʈ�� ĳ���� ���� �������� ���ӿ�����Ʈ�� �����
            GameObject npcInstance = Instantiate(npcList[i]);

            //��ġ ����
            npcInstance.transform.position = npcPosList[i].position;

           
            //������ �Ŵ������� NpcId�� �ش��ϴ� ������ ��������
            NPC dbase = DataManager.instance.npcDatabase.GetNPCByKey(npcId[i]);
            //������ �����͸� ������ ���Ǿ���Ʈ�ѷ��� �����ϱ�
            NPCController npcController = npcInstance.GetComponent<NPCController>();
            npcController.Init(dbase, this); // NPCManager �ڽ��� �������� ����
           
        }
    }


}
