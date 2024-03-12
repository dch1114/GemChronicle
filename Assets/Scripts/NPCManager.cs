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

    //���������� �����ν� �����͸� ���⼭ ����ͼ� �ٷ� ����������.
    //instantiate �� ���⼭ �ع�����

   

}
