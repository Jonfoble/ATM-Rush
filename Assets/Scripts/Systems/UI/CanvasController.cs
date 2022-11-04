using System.Collections;
using TMPro;
using UnityEngine;

public class CanvasController : Singleton<CanvasController>
{
	#region Private Variables
	[SerializeField] private GameObject panelMenu, panelInGame, panelEndGame;
    [SerializeField] private MoneyIndicator moneyIndicator;
    [SerializeField] private TextMeshProUGUI textTotalMoney;
	#endregion

	private void OnEnable()
    {
        GameManager.OnGameStart += SetInGameUi;
        GameManager.OnGameRunning += SetGameUi;
        GameManager.OnGameEnd += SetEndGameUi;
    }
    private void OnDisable()
    {
        GameManager.OnGameStart -= SetInGameUi;
        GameManager.OnGameRunning -= SetGameUi;
        GameManager.OnGameEnd -= SetEndGameUi;
    }

    private void SetInGameUi()
    {
        StartCoroutine(SetInGameUiRoutine());
    }
    private IEnumerator SetInGameUiRoutine()
    {
        panelMenu.SetActive(false);

        yield return new WaitForSeconds(1f);

        panelInGame.SetActive(true);
        moneyIndicator.enabled = true;
    }

    private void SetGameUi()
    {
        panelInGame.SetActive(false);
    }

    private void SetEndGameUi()
    {
        panelEndGame.SetActive(true);
        GameManager.isGameRunning = false;
    }

    public void UpdateTotalMoneyText(int value)
    {
        textTotalMoney.text = value.ToString();
    }

    #region UI Button's Methods
    public void ButtonStartPressed()
    {
        GameManager.OnGameStart?.Invoke();
        GameManager.isGameRunning = true;
    }
    public void ButtonNextLevelPressed()
    {
        GameManager.Instance.LoadNextLevel();
    }
    public void ButtonRestartPressed()
    {
        GameManager.Instance.RestartLevel();
    }
    #endregion

   
}