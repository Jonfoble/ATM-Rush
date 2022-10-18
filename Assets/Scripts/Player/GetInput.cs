using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetInput : MonoBehaviour
{
	[HideInInspector]public GameObject player;
	[HideInInspector]public Vector3 screenPosition;
	[HideInInspector]public Vector3 worldPosition;
	public static float inputHorizontal;
	public static float horizontalOffSet;
	private void OnEnable()
	{
		GameManager.OnGameRunning += GettingMouseInput;
	}
	private void OnDisable()
	{
		GameManager.OnGameRunning -= GettingMouseInput;
	}
	public void GettingMouseInput()
	{
		screenPosition = Input.mousePosition;
		screenPosition.z = Camera.main.nearClipPlane + 1;
		worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
		if (worldPosition.x > player.transform.position.x)
		{
			inputHorizontal = 1f;
		}
		else if (worldPosition.x < player.transform.position.x)
		{
			inputHorizontal = -1f;
		}
		else
		{
			inputHorizontal = 0f;
		}
	}
}
