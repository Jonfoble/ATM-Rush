using System.Collections;
using DG.Tweening;
using UnityEngine;

public class ClimbHandler : MonoBehaviour
{
	[SerializeField] private GameObject _car;
	[SerializeField] private GameObject _money;
	private float currentMoneyHeight = 0;
	private float currentPlayerHeight = 0;

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
		if (transform.position.y >= currentPlayerHeight + .3f)
		{
			SpawnM();
			currentPlayerHeight = transform.position.y;
		}
		yield return new WaitForSeconds(7f);
		GameManager.OnGameEnd?.Invoke();
		PlayerMovement.isRunning = true;
	}
	public void SpawnM()
	{
		Vector3 spawnPoint = new Vector3(-0.46f, currentMoneyHeight, 120f);
		Instantiate(_money, spawnPoint, _money.transform.rotation);
		currentMoneyHeight += .36f;
	}
	#endregion
}
