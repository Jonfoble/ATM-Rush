using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    public ScriptableObjects CollectablesScrObj;

	private void Awake()
	{
		gameObject.GetComponent<MeshRenderer>().material.color = CollectablesScrObj.color;
		
	}
}
