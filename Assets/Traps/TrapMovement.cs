using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapMovement : MonoBehaviour
{
	[SerializeField] private float moveSpeed = 1f;
	[SerializeField] private Vector3[] destinations;

	private Vector3 destination;
	private int destinationIndex = 0;

	private void Start()
	{
		destination = NextDestination();
	}

	private void Update()
	{
		Move();
	}

	private void Move()
	{
		if (transform.position != destination)
		{
			transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
		}
		else
		{
			destination = NextDestination();
		}
	}

	private Vector3 NextDestination()
	{
		if (destinations.Length == destinationIndex)
		{
			destinationIndex = 0;
		}

		Vector3 nextDestination = destinations[destinationIndex];
		destinationIndex++;
		return nextDestination;
	}
}
