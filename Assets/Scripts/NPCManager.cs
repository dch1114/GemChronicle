using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public static NPCManager instance = null;

    //엔피씨 게임오브젝트가 될 프리펩 리스트
    public List<GameObject> npcList;
    //엔피씨가 생성될 위치 리스트
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
        //프리펩을 게임오브젝트로 생성하고 위치도 지정해준다
        MakeNpcGameObject();

        //NPCController[] foundNPC = FindObjectsOfType<NPCController>();

        //foreach (NPCController npcController in foundNPC)
        //{
        //    npcController.Init();
        //}

    }

    //동적생성을 함으로써 데이터를 여기서 갖고와서 바로 만들어버린다.
    //instantiate 를 여기서 해버린다


    void MakeNpcGameObject()
    {
        
        for (int i = 0; i < npcList.Count; i++)
        {
            ///프리펩 동적생성
            //리스트에 캐싱해 놓은 프리팹을 게임오브젝트로 만들고
            GameObject npcInstance = Instantiate(npcList[i]);
            //위치 지정
            npcInstance.transform.position = npcPosList[i].position;
            ///데이터 가져오기
            //데이터 매니저에서 NpcId에 해당하는 데이터 가져오기
            NPC dbase = DataManager.instance.npcDatabase.GetNPCByKey(npcId[i]);
            //가져온 데이터를 각각의 엔피씨컨트롤러에 세팅하기
            npcInstance.GetComponent<NPCController>().SetNpcData(dbase);

        }
    }


}
