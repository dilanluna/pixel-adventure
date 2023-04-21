using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
	[SerializeField] private ParticleSystem dust;
	[SerializeField] private CharacterInput input;
	[SerializeField] private CharacterMovement movement;
	[SerializeField] private CharacterAnimation animation;

	private void Start()
	{
		// Movement
		input.OnMove += (axis) => movement.Move(axis.x);
		input.OnJump += (pressed) => movement.Jump(pressed);

		// Animations
		movement.OnMove += (direction) => animation.Run(Mathf.Abs(direction) != 0);
		movement.OnJump += (isJumping) => animation.Jump(isJumping);
		movement.OnFall += (isFalling) => animation.Fall(isFalling);
		movement.OnSlide += (isSliding) => animation.Slide(isSliding);

		// Particles
		movement.OnMove += (direction) =>
		{
			if (direction != 0f)
			{
				dust.Play();
			}
		};
	}
}
