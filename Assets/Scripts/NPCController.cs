using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    
    public int NPCID;
    
    private void Start()
    {
        NPCManager.instance.GetNpcData(NPCID);
        

    }
    
}
