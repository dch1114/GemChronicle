using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public GameObject downBtn;
    public GameObject upBtn;
    public TextMeshProUGUI amountBuyText;
    public TextMeshProUGUI amountSellText;
    public int amount = 0;
    int maxAmount = 999;

    public void UpButtonClick()
    {
        if(amount < maxAmount)
        {
            amount++;
        }
        else
        {
            amount = maxAmount;
        }

        UpdateText();
    }

    public void DownButtonClick()
    {
        if(amount <= 0)
        {
            amount = 0;
        }
        else
        {
            amount--;
        }

        UpdateText();
    }

    public void UpdateText()
    {
        amountBuyText.text = amount.ToString();
        amountSellText.text = amount.ToString();
    }
}
