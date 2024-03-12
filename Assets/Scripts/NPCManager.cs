using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public static NPCManager instance = null;
   

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

        NPCController[] foundNPC = FindObjectsOfType<NPCController>();
        foreach(NPCController npcController in foundNPC)
        {
            npcController.Init();
        }
    }

    //동적생성을 함으로써 데이터를 여기서 갖고와서 바로 만들어버린다.
    //instantiate 를 여기서 해버린다

   

}
