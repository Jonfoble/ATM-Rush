using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Collectables : MonoBehaviour
{

    //THIS SECTION IS STILL NOT FINISHED!

    #region Private Variables
	[SerializeField] private new BoxCollider _collider;
    [SerializeField] private List<MeshRenderer> _visuals;
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
        var targetPosX = Mathf.Lerp(transform.position.x, targetPos.x, 0.1f);
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
        transform.DOMove(position, 0.25f).SetEase(Ease.OutBounce);
        _collider.enabled = false;

        yield return new WaitForSeconds(0.2501f);

        isCollected = false;
        _collider.enabled = true;
    }

    private void Improve()
    {
        if (_level != 2)
        {
            _visuals[_level].enabled = false;
            _level++;
            _visuals[_level].enabled = true;
        }

    }

    private void GoToLastAtm()
    {
        transform.Translate(-transform.right * 5f * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isCollected && !inStack && !enteredAtmLine &&
            ((other.transform.parent != null && other.transform.parent.CompareTag("Player")) || other.CompareTag("Collectable")))
        {
            CollectableManager.Instance.AddItem(this);
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
