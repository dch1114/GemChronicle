using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NPCManager : Singleton<NPCManager>
{
    public TalkManager talkManager;
    //���Ǿ� ���ӿ�����Ʈ�� �� ������ ����Ʈ
    public List<GameObject> npcList;
    //���Ǿ��� ������ ��ġ ����Ʈ
    public List<Transform> npcPosList;

    public List<int> npcId;


    public void InitNPCManager()
    {
        MakeNpcGameObject();
    }
    //�������� ���ӿ�����Ʈ�� �����ϰ� ��ġ�� �������ش�
    void MakeNpcGameObject()
    {
        
        for (int i = 0; i < npcList.Count; i++)
        {
            //����Ʈ�� ĳ���� ���� �������� ���ӿ�����Ʈ�� �����
            GameObject npcInstance = Instantiate(npcList[i]);
            //��ġ ����
            npcInstance.transform.position = npcPosList[i].position;
            //������ �Ŵ������� NpcId�� �ش��ϴ� ������ ��������
            NPC dbase = DataManager.Instance.npcDatabase.GetNPCByKey(npcId[i]);
            //������ �����͸� ������ ���Ǿ���Ʈ�ѷ��� �����ϱ�
            NPCController npcController = npcInstance.GetComponent<NPCController>();
            npcController.Init(dbase, this); // NPCManager �ڽ��� �������� ����
           
        }
    }


}
