using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SkillPagesUI : MonoBehaviour
{
    [SerializeField] private GameObject Warrior;
    [SerializeField] private GameObject Archer;
    [SerializeField] private GameObject Magician;
    [SerializeField] private GameObject gems;

    [SerializeField] private TextMeshProUGUI iceGem;
    [SerializeField] private TextMeshProUGUI fireGem;
    [SerializeField] private TextMeshProUGUI lightGem;

    private Player player;

    private void Start()
    {
        player = GameManager.Instance.player;
    }

    public void SetJobPage()
    {
        JobType playerJobType = player.Data.StatusData.JobType;

        switch(playerJobType)
        {
            case JobType.Warrior:
                player.SkillPage = Warrior;
                break;
            case JobType.Archer:
                player.SkillPage = Archer;
                break;
            case JobType.Magician:
                player.SkillPage= Magician;
                break;
        }
    }

    public void ActiveGems()
    {
        SetGems();
        gems.SetActive(true);
    }

    public void OffGems()
    {
        gems.SetActive(false);
    }

    public void SetGems()
    {
        iceGem.text = player.Data.StatusData.Gems[SkillType.Ice].ToString();
        fireGem.text = player.Data.StatusData.Gems[SkillType.Fire].ToString();
        lightGem.text = player.Data.StatusData.Gems[SkillType.Light].ToString();
    }
}
