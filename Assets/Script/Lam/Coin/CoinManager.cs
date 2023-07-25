using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinManager : CusMonoBehaviour
{
    public static CoinManager instance;
    public TextMeshProUGUI CoinText;
    private int currentCoin;

    protected override void LoadComponents() 
    {
        instance = this;
        CoinText = GetComponentInChildren<TextMeshProUGUI>();
        ShowCoin();
    }

    protected override void ResetValue()
    {
        currentCoin = 0;
        PlayerPrefs.SetInt("Coin", currentCoin);
    }

    public void IncreaseCoin(int value)
    {
        currentCoin += value;
        PlayerPrefs.SetInt("Coin",currentCoin);
        ShowCoin();
    }

    private void ShowCoin()
    {
        currentCoin = PlayerPrefs.GetInt("Coin");
        CoinText.text = currentCoin.ToString();
    }

    public void ChargeCoin()
    {
        // to do
        Debug.Log("charge ooin");
    }
}
