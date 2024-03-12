using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public static NPCManager instance = null;

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
            DontDestroyOnLoad(gameObject);
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

        //NPCController[] foundNPC = FindObjectsOfType<NPCController>();

        //foreach (NPCController npcController in foundNPC)
        //{
        //    npcController.Init();
        //}

    }

    //���������� �����ν� �����͸� ���⼭ ����ͼ� �ٷ� ����������.
    //instantiate �� ���⼭ �ع�����


    void MakeNpcGameObject()
    {
        
        for (int i = 0; i < npcList.Count; i++)
        {
            ///������ ��������
            //����Ʈ�� ĳ���� ���� �������� ���ӿ�����Ʈ�� �����
            GameObject npcInstance = Instantiate(npcList[i]);
            //��ġ ����
            npcInstance.transform.position = npcPosList[i].position;
            ///������ ��������
            //������ �Ŵ������� NpcId�� �ش��ϴ� ������ ��������
            NPC dbase = DataManager.instance.npcDatabase.GetNPCByKey(npcId[i]);
            //������ �����͸� ������ ���Ǿ���Ʈ�ѷ��� �����ϱ�
            npcInstance.GetComponent<NPCController>().SetNpcData(dbase);

        }
    }


}
