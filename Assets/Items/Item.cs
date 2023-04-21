using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
	private event Action<Item> OnCollected;

	private void Start()
	{
		OnCollected += LevelManager.Instance.OnItemCollected;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		Animator animator = GetComponent<Animator>();
		animator.SetTrigger("Collected");
	}

	private void OnCollectedAnimationFinished()
	{
		OnCollected?.Invoke(this);
	}

	public void Destroy()
	{
		Destroy(gameObject);
	}
}
