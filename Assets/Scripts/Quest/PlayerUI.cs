using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{

    [SerializeField] private int goldTest;

    [SerializeField]
    private int gold;

    public int Gold
    {
        get { return gold; }
        set { gold = value; }
    }

    public PlayerStatusData playerStatusData;

    public int GoldTotales { get; set; }

    private string KEY_Gold = "MYGAME_GOLD";
    private float expActual;
    private float NewLevel;

    void Start()
    {
        //playerStatusData = GetComponent<PlayerStatusData>();
        PlayerPrefs.DeleteKey(KEY_Gold);
        ChargingGold();
    }

    public void UpdateExpPersonality(float pExpActul, float pExpRequired)
    {
        expActual = pExpActul;
        NewLevel = pExpRequired;
    }

    private void ChargingGold()
    {
        GoldTotales = PlayerPrefs.GetInt(KEY_Gold, goldTest);
    }


    public void AddGold(int quantity)
    {
        GoldTotales += quantity;
        PlayerPrefs.SetInt(KEY_Gold, GoldTotales);
        PlayerPrefs.Save();
    }

    public void RemoverMonedas(int cantidad)
    {
        if (cantidad > GoldTotales)
        {
            return;
        }

        GoldTotales -= cantidad;
        PlayerPrefs.SetInt(KEY_Gold, GoldTotales);
        PlayerPrefs.Save();
    }
}
