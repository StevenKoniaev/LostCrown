using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CoinBarScript : MonoBehaviour
{
    public Slider slider;


    public void SetMaxCoinAmount(int coinAmount)
    {
        slider.maxValue = coinAmount;
        slider.value = 0;
    }

    public void SetCoin(int coins)
    {
        slider.value = coins;
    }
}
