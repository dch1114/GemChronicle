using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class SkillPages : MonoBehaviour
{
    public Player player;
    public List<Toggle> asdPage;

    public void Start()
    {
        player = GameManager.Instance.player;

        SetSkillBtns();
    }

    public int GetASDIndex()
    {
        for (int i = 0; i < asdPage.Count; i++)
        {
            if (asdPage[i].isOn) return i;
        }

        return 0;
    }

    public abstract void SetSkillBtns();
    public abstract void UnlockSkillBtn();
}
