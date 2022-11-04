using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableManager : Singleton<CollectableManager>
{
    #region Private Variables
    [SerializeField] private Transform stackRoot;
    [SerializeField] private List<Transform> spreadPoints;
    [SerializeField] private float itemDeltaPosZ = 1.25f;
    [SerializeField] private GameObject _destroyEffect;
    private List<Collectables> items = new List<Collectables>();
    private int currentStackValue = 0;
    private bool animationPerforming = false;
	#endregion

	#region Public Variables
	public int ItemCount => items.Count;
    public int CurrentStackValue => currentStackValue;
	#endregion

	#region Functions

	public void AddItem(Collectables item)
    {
        if (items.Count > 0)
        {
            item.SetTarget(items[ItemCount - 1].transform, itemDeltaPosZ);
        }
        else// Appending the first item
        {
            item.SetTarget(stackRoot, itemDeltaPosZ);
        }

        items.Add(item);
        currentStackValue += item.Level;

        if (!animationPerforming)
        {
            animationPerforming = true;
            StartCoroutine(CollectAnim());
        }
    }

    public void DestroyDepositItem(Collectables collisionItem)
	{
        int collisionIndex = items.IndexOf(collisionItem);

        if (collisionIndex == ItemCount - 1)// Fucking collision of the last item with an atm
        {
            currentStackValue -= collisionItem.Level;
            items.RemoveAt(ItemCount - 1);
            collisionItem.transform.DOScale(0f, 0.2f);
            Destroy(collisionItem.GetComponent<BoxCollider>());
        }
    }
    public void DestroyItem(Collectables collisionItem)
    {
        int collisionIndex = items.IndexOf(collisionItem);

        if (collisionIndex == ItemCount - 1)// Fucking collision of the last item with an obstacle
        {
            currentStackValue -= collisionItem.Level;
            items.RemoveAt(ItemCount - 1);
            PerformDestroyAnimation(collisionItem.transform.position);
            Destroy(collisionItem.gameObject);
        }
        else //collision
        {
            PerformDestroyAnimation(collisionItem.transform.position);
            for (int i = collisionIndex; i < ItemCount; i++)
            {
				try 
                {
                    items[i].Throw(spreadPoints[i].position);
                    currentStackValue -= items[i].Level;
                }
                catch (ArgumentOutOfRangeException)
				{
                    print("Index Was Out Of Range");
				}
            }
            items.RemoveRange(collisionIndex, ItemCount - collisionIndex);
        }
    }

    public void DepositItem(Collectables depositItem)
    {
        MoneyManager.Instance.Deposit(depositItem.Level);
        DestroyDepositItem(depositItem);
    }

    public void UpdateStackValue()
    {
        currentStackValue++;
    }

    public void PerformDestroyAnimation(Vector3 transform)
	{
        GameObject Effect = Instantiate(_destroyEffect, transform, Quaternion.identity);
        Destroy(Effect, 2f);
	}
	#endregion

	#region Coroutines
	private IEnumerator CollectAnim()
    {
        for (int i = ItemCount - 1; i >= 0; i--)
        {
            if (items[i] == null)
            {
                break;
            }
			else
			{
                items[i].transform.DOPunchScale(Vector3.one, 0.2f, 2, 1f);
            }
            yield return new WaitForSeconds(0.05f);
        }
        animationPerforming = false;
    }
	#endregion
}
