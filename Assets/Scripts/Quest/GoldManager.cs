using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldManager : Singleton<GoldManager>
{
    [SerializeField] private int goldTest;

    public int GoldTotales {  get; set; }

    private string KEY_Gold = "MYGAME_GOLD";

    private void Start()
    {
        PlayerPrefs.DeleteKey(KEY_Gold);
        ChargingGold();
    }

    private void ChargingGold()
    {
        GoldTotales = PlayerPrefs.GetInt(KEY_Gold, goldTest);
    }

    public void AddGold(int quantity)
    {
        GoldTotales += quantity;
        UIManager.instance.GoldUpdate(GoldTotales);
        PlayerPrefs.SetInt(KEY_Gold, GoldTotales);
        PlayerPrefs.Save();
    }

    public void RemoverMonedas(int cantidad)
    {
        if(cantidad > GoldTotales)
        {
            return;
        }

        GoldTotales -= cantidad;
        UIManager.instance.GoldUpdate(GoldTotales);
        PlayerPrefs.SetInt(KEY_Gold, GoldTotales);
        PlayerPrefs.Save();
    }
}