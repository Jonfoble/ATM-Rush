using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
	[SerializeField] private Animator animator;

	private void Awake()
	{
		animator = GetComponent<Animator>();
	}

	private void Update()
	{
		if (!PlayerMovement.isRunning)
		{
			SetIdleAnimation();
		}
		else
		{
			SetRunAnimation();
		}
	}
	public void SetRunAnimation() 
	{
		animator.SetBool("isRunning", true);
	}
	public void SetIdleAnimation()
	{
		animator.SetBool("isRunning", false);
	}
}
