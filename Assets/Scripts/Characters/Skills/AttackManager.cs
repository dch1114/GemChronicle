using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    public Player player;
    public List<List<int>> atkList = new List<List<int>>();

    public static AttackManager instance;

    private void Awake()
    {
        if(instance == null) instance = this;
        else Destroy(instance);
    }

    private void InitData()
    {

    }

    public List<List<int>> GetSkillData()
    {
        List<List<int>> skillList = new List<List<int>>();

        return skillList;
    }
}
