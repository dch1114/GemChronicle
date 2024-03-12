using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class NPCController : MonoBehaviour
{
    

    public int NPCID;


    public void Init()
    {
       
        int NPCKeyToFind = NPCID;
        DataManager.instance.npcDatabase.GetNPCByKey(NPCKeyToFind);
    }
    
}
