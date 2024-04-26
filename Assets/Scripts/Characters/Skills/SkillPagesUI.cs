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

    //test
    //[Header("Sound")]
    //public AudioClip SkillPageOpenSound;
    //public AudioClip SkillPageCloseSound;

    private void Start()
    {
        
    }

    private void SetPlayer()
    {
        player = GameManager.Instance.player;
    }

    public void SetJobPage()
    {
        if(player != null)
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
        } else
        {
            SetPlayer();
            SetJobPage();
        }
    }

    public void ActiveGems()
    {
        UpdateGems();
        gems.SetActive(true);
    }

    public void OffGems()
    {
        gems.SetActive(false);
    }

    public void UpdateGems()
    {
        if(player != null)
        {
            iceGem.text = player.Data.StatusData.Gems[ElementType.Ice].ToString();
            fireGem.text = player.Data.StatusData.Gems[ElementType.Fire].ToString();
            lightGem.text = player.Data.StatusData.Gems[ElementType.Light].ToString();
        } else
        {
            SetPlayer();
            UpdateGems();
        }
    }

    //test
    public void ToggleSkillPage()
    {
        if (gems.activeSelf)
        {
            SoundManager.Instance.PlayClip(SoundManager.Instance.SkillPageCloseSound);
        }
        else
        {
            SoundManager.Instance.PlayClip(SoundManager.Instance.SkillPageOpenSound);
        }
    }

    //private void PlaySkillPageOpenSound()
    //{
    //    SoundManager.PlayClip(SkillPageOpenSound);
    //}

    //private void PlaySkillPageCloseSound()
    //{
    //    SoundManager.PlayClip(SkillPageCloseSound);
    //}
}
