using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public NPCDatabase npcDatabase;
    public int NPCID;
  
    private void Start()
    {
        npcDatabase = DataManager.instance.npcDatabase;
        int npcKeyToFind = NPCID;
        NPC foundNPC = npcDatabase.GetNPCByKey(npcKeyToFind);
        Debug.Log(npcKeyToFind);
        if (foundNPC != null)
        {
            
            Debug.Log("NPC ID: " + foundNPC.ID);
            Debug.Log("NPC role: " + foundNPC.role);
            Debug.Log("NPC name: " + foundNPC.name);
            Debug.Log("NPC place: " + foundNPC.place);
        }
        else
        {
            Debug.Log("NPC with key " + npcKeyToFind + " not found.");
        }

    }

}
