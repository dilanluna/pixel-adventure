using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterAnimation : MonoBehaviour
{
	private Animator animator;

	private void Start()
	{
		animator = GetComponent<Animator>();
	}

	public void Run(bool isRunning)
	{
		animator.SetBool("Running", isRunning);
	}

	public void Jump(bool isJumping)
	{
		animator.SetBool("Jumping", isJumping);
	}

	public void Fall(bool isFalling)
	{
		animator.SetBool("Falling", isFalling);

	}

	public void Slide(bool isSliding)
	{
		animator.SetBool("Sliding", isSliding);
	}
}
