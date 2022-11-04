using System.Collections;
using DG.Tweening;
using UnityEngine;

public class ClimbHandler : MonoBehaviour
{
	[SerializeField]private GameObject _car;

	private void Update()
	{
		if (!PlayerMovement.isRunning)
		{
			_car.SetActive(false);
			StartCoroutine(TurnCoroutine());
		}
		else
		{
			StopAllCoroutines();
		}
	}

	#region Coroutines
	private IEnumerator TurnCoroutine()
	{
		yield return new WaitForSeconds(2f);
		Vector3 rotationToHappen = new Vector3(transform.rotation.x, transform.rotation.y + 180, transform.rotation.z);
		this.gameObject.transform.DOLocalMoveZ(128f, .5f);
		this.gameObject.transform.DORotate(rotationToHappen, 2f);
		StartCoroutine(ClimbCoroutine());
	}
	private IEnumerator ClimbCoroutine()
	{

		yield return new WaitForSeconds(.5f);
		this.transform.DOLocalMoveY(CollectableManager.Instance.CurrentStackValue, 5f);
		yield return new WaitForSeconds(7f);
		GameManager.OnGameEnd?.Invoke();
		PlayerMovement.isRunning = true;
	}
	#endregion

}
