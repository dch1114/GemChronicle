using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public static NPCManager instance = null;
    public NPCDatabase npcDatabase;

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

        if (DataManager.instance != null)
        {
            npcDatabase = DataManager.instance.npcDatabase;
        }
        else
        {
            Debug.LogError("DataManager.npcDatabase null");
        }


    }
    void Start()
    {
        
        
    }


    public void GetNpcData(int _npcIdTofind)
    {

        NPC foundNPC = npcDatabase.GetNPCByKey(_npcIdTofind);
        if (foundNPC != null)
        {
            Debug.Log("NPC ID: " + foundNPC.ID);
            Debug.Log("NPC role: " + foundNPC.role);
            Debug.Log("NPC name: " + foundNPC.name);
            Debug.Log("NPC place: " + foundNPC.place);
            
        }
        else
        {
            Debug.Log("NPC with ID " + _npcIdTofind + " not found.");
        }
    }

}
