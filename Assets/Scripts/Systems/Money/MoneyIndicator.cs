using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoneyIndicator : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMoney;
    private Vector3 newPos;


    private void LateUpdate()
    {
        SetCoinText();
    }

    private void SetCoinText()
    {
        textMoney.text = CollectableManager.Instance.CurrentStackValue.ToString();
    }

    private void DisableThis()
    {
        Destroy(textMoney.gameObject);
        Destroy(this.gameObject);
    }

}