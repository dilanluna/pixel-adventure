using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterMovement : MonoBehaviour
{
	[Header("Movement")]
	[SerializeField] private float moveSpeed = 5f;

	[Header("Jump")]
	[SerializeField] private float jumpForce = 12f;
	[SerializeField] private float coyoteTime = 0.20f;
	[SerializeField] private LayerMask ground;
	[SerializeField] private Transform groundCheck;

	[Header("Wall Jump")]
	[SerializeField] private float wallSlidingSpeed = 2f;
	[SerializeField] private Transform wallLeftCheck;
	[SerializeField] private Transform wallRightCheck;

	public event Action<float> OnMove;
	public event Action<bool> OnJump;
	public event Action<bool> OnFall;
	public event Action<bool> OnSlide;

	private Rigidbody2D body;
	private float horizontal;
	private bool isFacingRight = true;
	private float coyoteTimeTimer = 0f;
	private Vector2 checkSize = new Vector2(.5f, .5f);

	private void Start()
	{
		body = GetComponent<Rigidbody2D>();
	}

	public void Move(float direction)
	{
		horizontal = direction;

		body.velocity = new Vector2(direction * moveSpeed, body.velocity.y);

		if (ShouldFlip(direction))
		{
			Flip();

			isFacingRight = !isFacingRight;
		}

		OnMove?.Invoke(direction);
	}

	private bool ShouldFlip(float direction)
	{
		return isFacingRight && direction < 0f || !isFacingRight && direction > 0f;
	}

	private void Flip()
	{
		Vector2 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}

	public void Jump(bool canJump)
	{
		// Check coyote time
		coyoteTimeTimer = IsGrounded() ? 0f : coyoteTimeTimer += Time.deltaTime;

		// Check wall slide
		bool isWallSliding = IsWalled() && !IsGrounded() && horizontal != 0f;

		// Perform jump
		if (coyoteTimeTimer <= coyoteTime && canJump)
		{
			coyoteTimeTimer = 0f;
			body.velocity = new Vector2(body.velocity.x, jumpForce);
		}

		// Perform wall slide
		if (isWallSliding)
		{
			body.velocity = new Vector2(body.velocity.x, Mathf.Clamp(body.velocity.y, -wallSlidingSpeed, float.MaxValue));
		}

		// Trigger events
		OnSlide?.Invoke(isWallSliding);
		OnJump?.Invoke(!IsGrounded() && body.velocity.y > 0);
		OnFall?.Invoke(!IsGrounded() && body.velocity.y < 0);
	}

	private bool IsWalled()
	{
		return Physics2D.OverlapBox(wallLeftCheck.position, checkSize, 0f, ground) || Physics2D.OverlapBox(wallRightCheck.position, checkSize, 0f, ground);
	}

	private bool IsGrounded()
	{
		return Physics2D.OverlapBox(groundCheck.position, checkSize, 0f, ground);
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireCube(groundCheck.position, checkSize);
		Gizmos.DrawWireCube(wallLeftCheck.position, checkSize);
		Gizmos.DrawWireCube(wallRightCheck.position, checkSize);
	}
}
