using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableManager : Singleton<CollectableManager>
{
    //THIS SECTION IS STILL NOT FINISHED!

    #region Private Variables
    [SerializeField] private Transform stackRoot;
    [SerializeField] private List<Transform> spreadPoints;
    private List<Collectables> items = new List<Collectables>();
    [SerializeField] private float itemDeltaPosZ = 1.25f;
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
        else// first item to add
        {
            item.SetTarget(stackRoot, itemDeltaPosZ);
        }

        items.Add(item);
        currentStackValue += item.Level;

        if (!animationPerforming)
        {
            animationPerforming = true;
            StartCoroutine(PerformCollectAnim());
        }
    }

    public void DestroyItem(Collectables collisionItem)
    {
        int collisionIndex = items.IndexOf(collisionItem);

        if (collisionIndex == ItemCount - 1)// Collision of the last item with an obstacle
        {
            currentStackValue -= collisionItem.Level;
            items.RemoveAt(ItemCount - 1);
            Destroy(collisionItem.gameObject);
        }
        else //collision
        {
            for (int i = collisionIndex; i < ItemCount; i++)
            {
                items[i].Throw(spreadPoints[i].position);
                currentStackValue -= items[i].Level;
            }

            items.RemoveRange(collisionIndex, ItemCount - collisionIndex);
        }
    }

    public void DepositItem(Collectables depositItem)
    {
        MoneyManager.Instance.Deposit(depositItem.Level);
        DestroyItem(depositItem);
    }

    public void UpdateStackValue()
    {
        currentStackValue++;
    }
	#endregion

	#region Coroutines
	private IEnumerator PerformCollectAnim()
    {
        for (int i = ItemCount - 1; i >= 0; i--)
        {
            if (items[i] == null)
            {
                break;
            }

            items[i].transform.DOPunchScale(Vector3.one, 0.2f, 2, 1f);

            yield return new WaitForSeconds(0.05f);
        }

        animationPerforming = false;
    }
	#endregion
}
