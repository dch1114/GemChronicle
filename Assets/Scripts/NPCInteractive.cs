using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface INPCInteractive
{
    void AddNPCList();
    void RemoveNPCList();

    void Talk();
    int NPCID { get; set; }

}


public class NPCInteractive : MonoBehaviour, INPCInteractive
{
    public int NPCID { get; set; }
    List<int> NPCList = new List<int>();

    
    public void AddNPCList()
    {
        NPCList.Add(NPCID);
        Debug.Log(NPCList);
    }

    public void RemoveNPCList()
    {
        NPCList.Remove(NPCID);
        Debug.Log(NPCList);
    }

    public void Talk()
    {

    }


}