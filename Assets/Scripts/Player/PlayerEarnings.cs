using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerEarnings : MonoBehaviour
{
    public int NumberCoinsEarned(int currentScore, int coinAmount)
    {
        return currentScore / coinAmount / 2;
    }

    public void AddWallet (ref int wallet,int currentScore, int coinAmount)
    {
        wallet += NumberCoinsEarned(currentScore, coinAmount);
    }

}
