using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerSkillDatabase
{
    public List<SkillInfoData> SkillDatas;
    public Dictionary<int, SkillInfoData> skillDic = new();

    public void Initialize()
    {
        foreach (SkillInfoData data in SkillDatas)
        {
            skillDic.Add(data.ID, data);
        }
    }

    public List<SkillInfoData> GetDataSection(int start, int end)
    {
        List<SkillInfoData> sectionDatas = new List<SkillInfoData>();
        foreach (SkillInfoData data in SkillDatas)
        {
            if (data.ID >= start && data.ID <= end)
                sectionDatas.Add(data);
        }

        return sectionDatas;
    }
}
