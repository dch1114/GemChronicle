using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PotalType
{ 
    A,
    B,
    C,
    D
}
[Serializable]
public class Potal
{
    public string potalId;
    public bool isLock;
    public Vector3 potalPosition;
}
public class PotalManager : Singleton<PotalManager>
{

    List<Potal> potalList = new List<Potal>();

    public List<Potal> GetPotalList()
    { 
        return potalList;
    }
    public void AddPotal(Potal potal)
    {
        potalList.Add(potal);
    }

    public void RemovePotal(Potal potal)
    {
        potalList.Remove(potal);
    }

    public void CheckUnLockPotal(Potal potal)
    {
        
        Potal temp = null;
        
        foreach (Potal p in potalList)
        {
            if (p.potalId == potal.potalId)
            {
                temp = p;

                break;
            }
        }

        if (temp != null)
        {
            if (temp.isLock)
            {
                Debug.Log("이 포탈은 통과 불가능");
            }
            else
            {
                Debug.Log("이 포탈은 통과 가능");

               

                if (GameManager.Instance != null)
                {
                    Player player = GameManager.Instance.player; // 게임 매니저를 통해 플레이어 얻기
                    if (player != null)
                    {
                        player.transform.position = temp.potalPosition;
                    }
                }




            }

        }

    }


}
