using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    public ScriptableObjects goldSO;

	private void Awake()
	{
		gameObject.GetComponent<MeshRenderer>().material.color = goldSO.color;
		
	}
}
