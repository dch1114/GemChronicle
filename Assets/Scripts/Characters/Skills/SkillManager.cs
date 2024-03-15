using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public Player player; //나중에 gameManager의 Player와 연결 필요

    public static SkillManager instance;

    private void Awake()
    {
        if(instance == null) instance = this;
        else Destroy(instance);
    }

    public List<List<int>> GetSkillData()
    {
        List<List<int>> skillList = new List<List<int>>();
        //switch(player.Data.StatusData.JobType)
        //{
        //    case JobType.Warrior:
        //        skillList = GetWarriorSkills();
        //        break;
        //    case JobType.Archer:
        //        skillList = GetArcherSkills();
        //        break;
        //    case JobType.Magician:
        //        skillList = GetMagicianSkills();
        //        break;
        //}

        return skillList;
    }

    public int[,] GetWarriorSkills()
    {
        int[,] skillList = new int[3, 3] { { 0, 1, 2 }, { 1, 1, 1 }, { 2, 1, 0 } };

        return skillList;
    }

    //public List<List<int>> GetWarriorSkills()
    //{

    //}
    public int[] GetArcherSkills()
    {
        int[] skillList = new int[3] { 0, 1, 2 };

        return skillList;
    }

    //public List<int> GetWarriorSkills()
    //{

    //}

    public int[] GetMagicianSkills()
    {
        int[] skillList = new int[3] { 0, 1, 2 };

        return skillList;
    }
}
