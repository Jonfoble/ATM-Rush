using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Collectables : MonoBehaviour
{
	#region Private Variables
	[SerializeField] private BoxCollider _collider;
	[SerializeField] private List<GameObject> _CollectableObjects;
	private ScriptableObjects _collectablesSO;
	private Transform _followTarget;
	private float _deltaPosZ;
	private int _level = 0;
	#endregion

	#region Public Variables
	public int Level => _level + 1;
	[System.NonSerialized] public bool isCollected = false, inStack = false, enteredAtmLine = false;
	#endregion

	private void Update()
	{
		if (_followTarget == null || !inStack) return;

		if (!enteredAtmLine)
		{
			Follow();
		}
		else
		{
			GoToLastAtm();
		}
	}

	private void Follow()
	{
		var targetPos = _followTarget.position + Vector3.forward * _deltaPosZ;
		var targetPosX = Mathf.Lerp(transform.position.x, targetPos.x, 0.08f);
		targetPos.x = targetPosX;
		transform.position = targetPos;
	}

	public void SetTarget(Transform followTarget, float deltaPosZ)
	{
		this._followTarget = followTarget;
		this._deltaPosZ = deltaPosZ;

		inStack = true;
		isCollected = true;
	}

	public void Throw(Vector3 position)
	{
		StartCoroutine(ThrowRoutine(position));
	}
	private IEnumerator ThrowRoutine(Vector3 position)
	{
		inStack = false;
		transform.DOMove(position, 0.26f).SetEase(Ease.OutBounce);
		_collider.enabled = false;

		yield return new WaitForSeconds(0.25f);

		isCollected = false;
		_collider.enabled = true;
	}
	private void Improve()
	{
		if (_level != 2)
		{
			_level++;
			gameObject.GetComponentInChildren<MeshFilter>().sharedMesh = _CollectableObjects[_level].GetComponentInChildren<MeshFilter>().sharedMesh;
			gameObject.GetComponentInChildren<MeshRenderer>().sharedMaterials = _CollectableObjects[_level].GetComponentInChildren<MeshRenderer>().sharedMaterials;
		}
	}
	private void GoToLastAtm()
	{
		transform.DOLocalMoveX(-15f, 2f);
	}
	private void OnTriggerEnter(Collider other)
	{
		if (!isCollected && !inStack && !enteredAtmLine)
		{
			if(other.CompareTag("Player") || other.CompareTag("Collectable"))
			{
				print(other.tag);
				CollectableManager.Instance.AddItem(this);
			}
		}
		if (inStack && isCollected)
		{
			if (other.CompareTag("Obstacle"))
			{
				CollectableManager.Instance.DestroyItem(this);
			}
			else if (other.CompareTag("Gate"))
			{
				Improve();
				CollectableManager.Instance.UpdateStackValue();
			}
			else if (other.CompareTag("Atm"))
			{
				if (enteredAtmLine)
				{
					Destroy(gameObject);
					return;
				}
				CollectableManager.Instance.DepositItem(this);
			}
			else if (other.CompareTag("AtmLine"))
			{
				enteredAtmLine = true;
			}
		}
	}
}
