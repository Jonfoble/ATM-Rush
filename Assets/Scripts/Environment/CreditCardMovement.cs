using DG.Tweening;
using UnityEngine;

public class CreditCardMovement : MonoBehaviour
{
	[SerializeField] private GameObject _card;
	private void Start()
	{
		MoveCreditCard();
	}

	public void MoveCreditCard()
	{
		_card.transform.DOMoveX(3f, 4f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
	}
}
