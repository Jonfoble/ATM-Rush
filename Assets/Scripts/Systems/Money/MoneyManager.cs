using UnityEngine;

public class MoneyManager : Singleton<MoneyManager>
{
    public int CurrentMultiplier { set => currentMultiplier = value; }
    private int totalAmount, currentMultiplier;

	private void OnEnable()
	{
        GameManager.OnGameEnd += SaveTheAmount;
    }
	private void OnDisable()
	{
        GameManager.OnGameEnd -= SaveTheAmount;
    }

	private void Start()
    {
        totalAmount = PlayerPrefs.GetInt("TOTAL_MONEY", 0);
        CanvasController.Instance.UpdateTotalMoneyText(totalAmount);
    }

    public void Deposit(int itemLevel)// atm
    {
        totalAmount += itemLevel + 1;
    }

    private void SaveTheAmount()// level finish action
    {
        totalAmount += CollectableManager.Instance.CurrentStackValue * currentMultiplier;
        PlayerPrefs.SetInt("TOTAL_MONEY", totalAmount);
        CanvasController.Instance.UpdateTotalMoneyText(totalAmount);
    }
}