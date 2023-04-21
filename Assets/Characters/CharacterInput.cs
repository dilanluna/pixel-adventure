using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterInput : MonoBehaviour
{
	[SerializeField]
	private InputActionAsset actions;
	private InputActionMap controls;
	public event Action<Vector2> OnMove;
	public event Action<bool> OnJump;

	private void Awake()
	{
		controls = actions.FindActionMap("Gameplay");
	}

	private void Update()
	{
		OnMove?.Invoke(controls.FindAction("Move").ReadValue<Vector2>());
		OnJump?.Invoke(controls.FindAction("Jump").WasPressedThisFrame());
	}

	private void OnEnable()
	{
		actions.Enable();
	}

	private void OnDisable()
	{
		actions.Disable();
	}
}
