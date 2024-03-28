using TMPro;
using UnityEngine;


public class UI_Status : MonoBehaviour
{
    private Player player;
    private InventoryUIController ui;

    [SerializeField] private string statusName;
    [SerializeField] private StatusType statType;
    [SerializeField] private TextMeshProUGUI statusValueText;
    [SerializeField] private TextMeshProUGUI statusNameText;

    private void Start()
    {
        player = Inventory.Instance.player;
        ui = GetComponentInParent<InventoryUIController>();
        UpdateStatValueUI();
    }

    private void OnValidate()
    {
        gameObject.name = "Stat - " + statusName;


        if (statusNameText != null)
            statusNameText.text = statusName;
    }

    public void UpdateStatValueUI()
    {
        PlayerDatas playerStats = player.GetComponent<PlayerDatas>();

        if (playerStats != null)
        {
            statusValueText.text = playerStats.StatusData.GetStatus(statType).ToString();
        }
    }
}
