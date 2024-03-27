using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : Singleton<SkillManager>
{
    public void SkillCombo(List<GameObject> skills)
    {
        StartCoroutine(ShootSkills(skills));
    }

    private IEnumerator ShootSkills(List<GameObject> skills)
    {
        for(int i = 0; i < skills.Count; i++)
        {
            GameObject go = skills[i];
            go.SetActive(true);

            yield return new WaitUntil(() => !go.activeSelf);
        }
    }
}
