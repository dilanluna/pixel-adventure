using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
	public static LevelManager Instance { get; private set; }

	public event Action OnGoalReached;
	private List<int> collectibleIds = new List<int>();

	private void Awake()
	{
		// If there is an instance, and it's not me, delete myself.
		if (Instance != null && Instance != this)
		{
			Destroy(this);
		}
		else
		{
			Instance = this;
		}
	}

	private void Start()
	{
		GameObject[] collectibles = GameObject.FindGameObjectsWithTag("Item");
		foreach (GameObject collectible in collectibles)
		{
			collectibleIds.Add(collectible.GetInstanceID());
		}
	}

	public void OnItemCollected(Item collectible)
	{
		collectibleIds.Remove(collectible.gameObject.GetInstanceID());
		collectible.Destroy();

		if (collectibleIds.Count == 0)
		{
			OnGoalReached?.Invoke();
		}
	}
}
