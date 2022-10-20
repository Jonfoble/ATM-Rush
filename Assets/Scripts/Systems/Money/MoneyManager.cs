using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : Singleton<MoneyManager>
{
    //THIS SECTION IS STILL NOT FINISHED!

    public int CurrentMultiplier { set => currentMultiplier = value; }

    private int totalAmount, currentMultiplier;


    private void Start()
    {
        GameManager.OnGameEnd += SaveTheAmount;
    }

    public void Deposit(int itemLevel)// atm
    {
        totalAmount += itemLevel + 1;
    }

    private void SaveTheAmount()// level finish action
    {
        totalAmount += CollectableManager.Instance.CurrentStackValue * currentMultiplier;
        PlayerPrefs.SetInt("TOTAL_MONEY", totalAmount);
    }

    private void OnDestroy()
    {
        GameManager.OnGameEnd -= SaveTheAmount;
    }
}