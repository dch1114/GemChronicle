using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterExperience : MonoBehaviour
{
    [SerializeField] private int levelMax;
    [SerializeField] private int expBase;
    [SerializeField] private int incrementalvalue;

    public int Level { get; set; }

    private float expActualTemp;
    private float ExpRequiredNextLevel;

    private void Start()
    {
        Level = 1;
        ExpRequiredNextLevel = expBase;
        UpdateBarExp();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            AddExperience(2f);
        }
    }

    public void AddExperience(float expObtained)
    {
        if (expObtained > 0f)
        {
            float NewLevelRepresentative = ExpRequiredNextLevel - expActualTemp;
            if(expObtained >= NewLevelRepresentative)
            {
                expObtained -= NewLevelRepresentative;
                UpdateLevel();
                AddExperience(expObtained);
            }
            else
            {
                expActualTemp += expObtained;
                UIManager.instance.ExpUpdate(expActualTemp, ExpRequiredNextLevel);
                if (expActualTemp == ExpRequiredNextLevel)
                {
                    UpdateLevel();
                }
            }
        }

        UpdateBarExp();
    }

    private void UpdateLevel()
    {
        if(Level < levelMax)
        {
            Level++;
            expActualTemp = 0f;
            ExpRequiredNextLevel *= incrementalvalue;
        }
    }

    private void UpdateBarExp()
    {
        UIManager.instance.UpdateExpPersonality(expActualTemp, ExpRequiredNextLevel);
    }
}
